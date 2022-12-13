﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BuildToolsUnitTests.AOT.E2E
{
    public abstract partial class GeneratorTestBase
    {
        private readonly ITestOutputHelper? _log;
        protected GeneratorTestBase(ITestOutputHelper? log)
            => _log = log;

        // input from https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md#unit-testing-of-generators

        protected (Compilation? Compilation, GeneratorDriverRunResult Result, ImmutableArray<Diagnostic> Diagnostics, int ErrorCount) Execute<T>(string source,
            StringBuilder? diagnosticsTo = null,
            [CallerMemberName] string? name = null,
            string? fileName = null,
            Action<T>? initializer = null
            ) where T : class, ISourceGenerator, new()
        {
            void Output(string message)
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    _log?.WriteLine(message);
                    diagnosticsTo?.Append("// ").AppendLine(message.Replace('\\', '/')); // need to normalize paths
                }
            }
            // Create the 'input' compilation that the generator will act on
            if (string.IsNullOrWhiteSpace(name)) name = "compilation";
            if (string.IsNullOrWhiteSpace(fileName)) fileName = "input.cs";
            Compilation inputCompilation = CreateCompilation(source, name!, fileName!);

            // directly create an instance of the generator
            // (Note: in the compiler this is loaded from an assembly, and created via reflection at runtime)
            T generator = new();
            initializer?.Invoke(generator);

            ShowDiagnostics("Input code", inputCompilation, diagnosticsTo, "CS8795", "CS1701", "CS1702");

            // Create the driver that will control the generation, passing in our generator
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            // Run the generation pass
            // (Note: the generator driver itself is immutable, and all calls return an updated version of the driver that you should use for subsequent calls)
            driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);
            var runResult = driver.GetRunResult();
            foreach (var result in runResult.Results)
            {
                if (result.Exception is not null) throw result.Exception;
            }

            var dn = Normalize(diagnostics, Array.Empty<string>());
            if (dn.Any())
            {
                Output($"Generator produced {dn.Count} diagnostics:");
                foreach (var d in dn)
                {
                    Output(d);
                }
            }

            var errorCount = ShowDiagnostics("Output code", outputCompilation, diagnosticsTo, "CS1701", "CS1702");
            return (outputCompilation, runResult, diagnostics, errorCount);
        }

        int ShowDiagnostics(string caption, Compilation compilation, StringBuilder? diagnosticsTo, params string[] ignore)
        {
            if (_log is null && diagnosticsTo is null) return 0; // nothing useful to do!
            void Output(string message)
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    _log?.WriteLine(message);
                    diagnosticsTo?.Append("// ").AppendLine(message.Replace('\\', '/')); // need to normalize paths
                }
            }
            int errorCountTotal = 0;
            foreach (var tree in compilation.SyntaxTrees)
            {
                var rawDiags = compilation.GetSemanticModel(tree).GetDiagnostics();
                var diagnostics = Normalize(rawDiags, ignore);
                errorCountTotal += rawDiags.Count(x => x.Severity == DiagnosticSeverity.Error);

                if (diagnostics.Any())
                {
                    Output($"{caption} has {diagnostics.Count} diagnostics from '{tree.FilePath}':");
                    foreach (var d in diagnostics)
                    {
                        Output(d);
                    }
                }
            }
            return errorCountTotal;
        }

        static List<string> Normalize(ImmutableArray<Diagnostic> diagnostics, string[] ignore) => (
            from d in diagnostics
            where !ignore.Contains(d.Id)
            let loc = d.Location
            let msg = d.ToString()
            orderby loc.SourceTree?.FilePath, loc.SourceSpan.Start, d.Id, msg
            select msg).ToList();

        static readonly CSharpParseOptions ParseOptionsLatestLangVer = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Latest);

        protected static CSharpCompilation CreateCompilation(string source, string name, string fileName)
           => CSharpCompilation.Create(name,
               syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, ParseOptionsLatestLangVer).WithFilePath(fileName) },
               references: new[] {
                   MetadataReference.CreateFromFile(typeof(Binder).Assembly.Location),
#if !NET48
                   MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
                   MetadataReference.CreateFromFile(Assembly.Load("System.Data").Location),
                   MetadataReference.CreateFromFile(Assembly.Load("netstandard").Location),
                   MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location),
#endif
                   MetadataReference.CreateFromFile(typeof(DbConnection).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(ValueTask<int>).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(Component).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(ImmutableList<int>).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(ImmutableArray<int>).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(IAsyncEnumerable<int>).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(Span<int>).Assembly.Location),
                   MetadataReference.CreateFromFile(typeof(IgnoreDataMemberAttribute).Assembly.Location),
               },
               options: new CSharpCompilationOptions(OutputKind.ConsoleApplication, allowUnsafe: true));

        protected string? Log(string? message)
        {
            if (!string.IsNullOrEmpty(message)) _log?.WriteLine(message);
            return message;
        }
    }
}
