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
