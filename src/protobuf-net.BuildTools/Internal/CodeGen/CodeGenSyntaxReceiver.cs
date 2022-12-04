using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace ProtoBuf.Internal.CodeGen
{
    sealed class CodeGenSyntaxReceiver : ISyntaxReceiver
    {
        private readonly List<ClassDeclarationSyntax> _classDeclarations = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration)
            {
                _classDeclarations.Add(classDeclaration);
            }

            if (syntaxNode is StructDeclarationSyntax structDeclaration)
            { 
                // TODO do we need structs here?
            }
        }

        public bool IsEmpty => _classDeclarations.Count == 0;

        public List<ClassDeclarationSyntax>.Enumerator GetEnumerator()
            => _classDeclarations.GetEnumerator();
    }
}
