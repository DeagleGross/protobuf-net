using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;
using ProtoBuf.Internal.CodeGen;
using Xunit;
using Xunit.Abstractions;
using VerifyCS = BuildToolsUnitTests.AOT.E2E.CSharpSourceGeneratorVerifier<ProtoBuf.Internal.CodeGen.CodeGenSourceGenerator>;

namespace BuildToolsUnitTests.AOT.E2E
{
    public class RoslynGeneratorTests : GeneratorTestBase
    {
        //public static IEnumerable<object[]> GetFiles() =>
        //    from path in Directory.GetFiles("jk", "*.cs", SearchOption.AllDirectories)
        //    where path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
        //    select new object[] { path };

        //[Theory, MemberData(nameof(GetFiles))]
        //public async Task Run(string initialCodeFilePath, string expectedCodeFilePath)
        //{
        //    var initialCode = await File.ReadAllTextAsync(initialCodeFilePath);
        //    var expectedCode = await File.ReadAllTextAsync(expectedCodeFilePath);

        //    await new VerifyCS.Test
        //    {
        //        TestState =
        //        {
        //            Sources = { initialCode },
        //            GeneratedSources =
        //            {
        //                (typeof(CodeGenSourceGenerator), "", SourceText.From(expectedCode, Encoding.UTF8, SourceHashAlgorithm.Sha256))
        //            }
        //        }
        //    }.RunAsync();
        //}

        public RoslynGeneratorTests(ITestOutputHelper log) : base(log) { }

        public static IEnumerable<object[]> GetFiles() =>
            from path in Directory.GetFiles("AOT/E2E/Schemas/", "*.cs", SearchOption.AllDirectories)
            where path.EndsWith(".input.cs", StringComparison.OrdinalIgnoreCase)
            select new object[] { path };

        [Theory, MemberData(nameof(GetFiles))]
        public void Run(string path)
        {
            var intputPath = File.ReadAllText(path);
            var outputPath = Regex.Replace(path, @"\.input\.cs$", ".output.cs", RegexOptions.IgnoreCase);
            var expected = File.Exists(outputPath) ? File.ReadAllText(outputPath) : string.Empty;
            
            var sb = new StringBuilder();
            var result = Execute<CodeGenSourceGenerator>(intputPath, sb, fileName: path, initializer: g =>
            {
                g.DefaultOutputFileName = Path.GetFileName(outputPath);
                g.ReportVersion = false;
                g.Log += (severity, message) => Log($"{severity}: {message}");
            });
            Assert.Single(result.Result.GeneratedTrees);
            var generated = Assert.Single(Assert.Single(result.Result.Results).GeneratedSources);

            string? code = generated.SourceText?.ToString();
#if DEBUG
            Log(code);
#endif
            sb.AppendLine().AppendLine(code);

            var actual = sb.ToString();
            try // automatically overwrite test output, for git tracking
            {
                if (GetOriginCodeLocation() is string originFile
                    && Path.GetDirectoryName(originFile) is string originFolder)
                {
                    outputPath = Path.Combine(originFolder, outputPath);
                    File.WriteAllText(outputPath, actual);

                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            Assert.Equal(0, result.ErrorCount);
            Assert.Equal(expected.Trim(), actual.Trim(), ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }

        static string? GetOriginCodeLocation([CallerFilePath] string? path = null) => path;
    }
}
