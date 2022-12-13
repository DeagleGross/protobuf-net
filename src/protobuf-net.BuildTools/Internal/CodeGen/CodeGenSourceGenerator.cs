using Microsoft.CodeAnalysis;
using System;

namespace ProtoBuf.Internal.CodeGen
{
    /// <summary>
    /// Parses the source for any compatible syntax nodes.
    /// 
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class CodeGenSourceGenerator : ISourceGenerator
    {
        /// <summary>
        /// Provide log feedback.
        /// </summary>
        public event Action<DiagnosticSeverity, string>? Log;

        /// <summary>
        /// Indicate version in generated code.
        /// </summary>
        public bool ReportVersion { get; set; } = true;

        /// <summary>
        /// The name of the file to generate.
        /// </summary>
        public string DefaultOutputFileName { get; set; } = "Dapper.generated.cs";

        void ISourceGenerator.Initialize(GeneratorInitializationContext context)
            => context.RegisterForSyntaxNotifications(static () => new CodeGenSyntaxReceiver());

        void ISourceGenerator.Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not CodeGenSyntaxReceiver syntaxReceiver || syntaxReceiver.IsEmpty)
                return;

            foreach (var syntaxNode in syntaxReceiver)
            {
                try
                {
                    var symbol = context.Compilation.GetSemanticModel(syntaxNode.SyntaxTree).GetDeclaredSymbol(syntaxNode);


                }
                catch (Exception ex)
                {

                }
            }


        }
    }
}
