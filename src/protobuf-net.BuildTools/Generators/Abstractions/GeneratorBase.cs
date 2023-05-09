﻿#nullable enable
using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ProtoBuf.BuildTools.Internal;
using ProtoBuf.Reflection;

namespace ProtoBuf.Generators.Abstractions
{
    [Generator]
    public abstract class GeneratorBase : ISourceGenerator, ILoggingAnalyzer
    {
        private event Action<string>? Logger;
        event Action<string>? ILoggingAnalyzer.Log
        {
            add => Logger += value;
            remove => Logger -= value;
        }

        protected Version? ProtobufVersion;
        protected Version? ProtobufGrpcVersion;
        protected Version? WcfVersion;
        
        public virtual void Initialize(GeneratorInitializationContext context) { }
        public abstract void Execute(GeneratorExecutionContext context);
        
        protected void Log(string message) => Logger?.Invoke(message);

        protected void Startup(GeneratorExecutionContext context)
        {
            Log("Execute with debug log enabled");

            ProtobufVersion = context.Compilation.GetProtobufNetVersion();
            ProtobufGrpcVersion = context.Compilation.GetReferenceVersion("protobuf-net.Grpc");
            WcfVersion = context.Compilation.GetReferenceVersion("System.ServiceModel.Primitives");

            Log($"Referencing protobuf-net {ShowVersion(ProtobufVersion)}, protobuf-net.Grpc {ShowVersion(ProtobufGrpcVersion)}, WCF {ShowVersion(WcfVersion)}");

            string ShowVersion(Version? version)
                => version is null ? "(n/a)" : $"v{version}";

            if (Logger is not null)
            {
                foreach (var ran in context.Compilation.ReferencedAssemblyNames.OrderBy(x => x.Name))
                {
                    Log($"reference: {ran.Name} v{ran.Version}");
                }
            }
        }

        protected bool TryDetectCodeGenerator(
            GeneratorExecutionContext context,
            out CodeGenerator? codeGenerator, 
            out string? langVer)
        {
            switch (context.Compilation.Language)
            {
                case "C#":
                    codeGenerator = CSharpCodeGenerator.Default;
                    langVer = default;
                    if (context.ParseOptions is CSharpParseOptions cs)
                    {
                        langVer = cs.LanguageVersion switch
                        {
                            LanguageVersion.CSharp1 => "1",
                            LanguageVersion.CSharp2 => "2",
                            LanguageVersion.CSharp3 => "3",
                            LanguageVersion.CSharp4 => "4",
                            LanguageVersion.CSharp5 => "5",
                            LanguageVersion.CSharp6 => "6",
                            LanguageVersion.CSharp7 => "7",
                            LanguageVersion.CSharp7_1 => "7.1",
                            LanguageVersion.CSharp7_2 => "7.2",
                            LanguageVersion.CSharp7_3 => "7.3",
                            LanguageVersion.CSharp8 => "8",
                            LanguageVersion.CSharp9 => "9",
                            _ => null
                        };
                    }
                    break;
                //case "VB": // completely untested, and pretty sure this isn't even a "thing"
                //    generator = VBCodeGenerator.Default;
                //    langver = "14.0"; // TODO: lookup from context
                //    break;
                default:
                    Log($"Unexpected language: {context.Compilation.Language}");
                    codeGenerator = null;
                    langVer = null;
                    return false;
            }
            
            Log($"Detected {codeGenerator.Name} v{langVer}");
            return true;
        }
    }
}