using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Text;
using ProtoBuf.Internal.CodeGen;
using Xunit;

using VerifyCS = BuildToolsUnitTests.AOT.E2E.CSharpSourceGeneratorVerifier<ProtoBuf.Internal.CodeGen.CodeGenSourceGenerator>;

namespace BuildToolsUnitTests.AOT.E2E
{
    public class RoslynGeneratorTests
    {
        public static IEnumerable<object[]> GetFiles() =>
            from path in Directory.GetFiles("jk", "*.cs", SearchOption.AllDirectories)
            where path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
            select new object[] { path };

        [Theory, MemberData(nameof(GetFiles))]
        public async Task Run(string initialCodeFilePath, string expectedCodeFilePath)
        {
            var initialCode = await File.ReadAllTextAsync(initialCodeFilePath);
            var expectedCode = await File.ReadAllTextAsync(expectedCodeFilePath);

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { initialCode },
                    GeneratedSources =
                    {
                        (typeof(CodeGenSourceGenerator), "", SourceText.From(expectedCode, Encoding.UTF8, SourceHashAlgorithm.Sha256))
                    }
                }
            }.RunAsync();
        }


    }
}
