// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: TypeRefs.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace TypeRefs
{

    [global::ProtoBuf.ProtoContract(Name = @"msg_a")]
    public partial class MsgA : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"a")]
        public global::TypeRefs.MsgA A { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"b")]
        public global::TypeRefs.MsgB B { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"c")]
        public global::TypeRefs.MsgB.MsgC C { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"d")]
        public global::TypeRefs.MsgD D { get; set; }

    }

    [global::ProtoBuf.ProtoContract(Name = @"msg_b")]
    public partial class MsgB : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"a")]
        public global::TypeRefs.MsgA A { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"b")]
        public global::TypeRefs.MsgB B { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"c")]
        public global::TypeRefs.MsgB.MsgC C { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"d")]
        public global::TypeRefs.MsgD D { get; set; }

        [global::ProtoBuf.ProtoContract(Name = @"msg_c")]
        public partial class MsgC : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"a")]
            public global::TypeRefs.MsgA A { get; set; }

            [global::ProtoBuf.ProtoMember(2, Name = @"b")]
            public global::TypeRefs.MsgB B { get; set; }

            [global::ProtoBuf.ProtoMember(3, Name = @"c")]
            public global::TypeRefs.MsgB.MsgC C { get; set; }

            [global::ProtoBuf.ProtoMember(4, Name = @"d")]
            public global::TypeRefs.MsgD D { get; set; }

        }

    }

    [global::ProtoBuf.ProtoContract(Name = @"msg_d")]
    public partial class MsgD : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"a")]
        public global::TypeRefs.MsgA A { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"b")]
        public global::TypeRefs.MsgB B { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"c")]
        public global::TypeRefs.MsgB.MsgC C { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"d")]
        public global::TypeRefs.MsgD D { get; set; }

    }

}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE0079, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion
