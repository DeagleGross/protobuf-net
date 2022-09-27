// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: Basic.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace BasicPackage
{

    [global::ProtoBuf.ProtoContract(Name = @"basic_message")]
    public partial class BasicMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"basic_field")]
        [global::System.ComponentModel.DefaultValue()]
        public string BasicField { get; set; }

        internal static void Serialize(BasicMessage value, ref global::ProtoBuf.Nano.Writer writer)
        {
            if (value.BasicField is { Length: > 0} s)
            {
                writer.WriteVarint(10); // field 1, string
                writer.WriteWithLengthPrefix(s);
            }
        }

        internal static ulong Measure(BasicMessage value)
        {
            ulong len = 0;
            if (value.BasicField is { Length: > 0} s)
            {
                len += 1 + global::ProtoBuf.Nano.Writer.MeasureWithLengthPrefix(s);
            }
            return len;
        }

        internal static BasicMessage Merge(BasicMessage value, ref global::ProtoBuf.Nano.Reader reader)
        {
            if (value is null) value = new();
            uint tag;
            while ((tag = reader.ReadTag()) != 0)
            {
                switch (tag)
                {
                    case 10: // field 1, string
                        value.BasicField = reader.ReadString();
                        break;
                    default:
                        if ((tag & 7) == 4) // end-group
                        {
                            reader.PushGroup(tag);
                            goto ExitLoop;
                        }
                        switch (tag >> 3)
                        {
                            case 1:
                                reader.ThrowUnhandledWireType(tag);
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

}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion
