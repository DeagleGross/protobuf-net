// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: Enums.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace BasicPackage
{

    [global::ProtoBuf.ProtoContract()]
    public partial class SearchRequest : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(2, Name = @"corpus")]
        public global::BasicPackage.Corpus Corpus { get; set; }

        internal static void Serialize(SearchRequest value, ref global::ProtoBuf.Nano.Writer writer)
        {
            if (value.Corpus is global::BasicPackage.Corpus obj2)
            {
                writer.WriteVarint(18); // field 2, string
                writer.WriteVarintUInt64(global::BasicPackage.Corpus.Measure(obj2);
                global::BasicPackage.Corpus.Write(obj2, ref writer);
            }
        }

        internal static ulong Measure(SearchRequest value)
        {
            ulong len = 0;
            if (value.Corpus is global::BasicPackage.Corpus obj2)
            {
                len += 1 + global::BasicPackage.Corpus.Measure(obj2);
            }
            return len;
        }

        internal static SearchRequest Merge(SearchRequest value, ref global::ProtoBuf.Nano.Reader reader)
        {
            ulong oldEnd;
            if (value is null) value = new();
            uint tag;
            while ((tag = reader.ReadTag()) != 0)
            {
                switch (tag)
                {
                    case 18: // field 2, string
                        oldEnd = reader.ConstrainByLengthPrefix();
                        value.Corpus = global::BasicPackage.Corpus.Merge(value.Corpus, ref reader);
                        reader.Unconstrain(oldEnd);
                        break;
                    case 19: // field 2, group
                        value.Corpus = global::BasicPackage.Corpus.Merge(value.Corpus, ref reader);
                        reader.PopGroup(2);
                        break;
                    default:
                        if ((tag & 7) == 4) // end-group
                        {
                            reader.PushGroup(tag);
                            goto ExitLoop;
                        }
                        switch (tag >> 3)
                        {
                            case 2:
                                reader.UnhandledTag(tag); // throws
                                break;
                        }
                        reader.Skip(tag);
                        break;
                }
            }
        ExitLoop:
            return value;
        }

    }

    [global::ProtoBuf.ProtoContract()]
    public enum Corpus
    {
        [global::ProtoBuf.ProtoEnum(Name = @"CORPUS_UNSPECIFIED")]
        CorpusUnspecified = 0,
        [global::ProtoBuf.ProtoEnum(Name = @"CORPUS_UNIVERSAL")]
        CorpusUniversal = 1,
        [global::ProtoBuf.ProtoEnum(Name = @"CORPUS_WEB")]
        CorpusWeb = 2,
    }

}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion
