﻿ using System;

 namespace ProtoBuf
{
    /// <summary>
    /// Specifies the type of discriminated union implementation used.
    /// See: <see cref="DiscriminatedUnion32"/>, <see cref="DiscriminatedUnion32Object"/>, etc. 
    /// </summary>
    public enum DiscriminatedUnionType
    {
        Object,
        
        Standard32,
        Object32,
        
        Standard64,
        Object64,
        
        Standard128,
        Object128
    }
    
    /// <summary>
    /// Helpers for <see cref="DiscriminatedUnionType"/>
    /// </summary>
    public static class DiscriminatedUnionTypeExtensions
    {
        /// <summary>
        /// Returns string name of corresponding DiscriminatedUnion for <see cref="DiscriminatedUnionType"/>
        /// </summary>
        public static string GetTypeName(this DiscriminatedUnionType type) => type switch
        {
            DiscriminatedUnionType.Object => nameof(DiscriminatedUnionObject),
            DiscriminatedUnionType.Standard32 => nameof(DiscriminatedUnion32),
            DiscriminatedUnionType.Object32 => nameof(DiscriminatedUnion32Object),
            DiscriminatedUnionType.Standard64 => nameof(DiscriminatedUnion64),
            DiscriminatedUnionType.Object64 => nameof(DiscriminatedUnion64Object),
            DiscriminatedUnionType.Standard128 => nameof(DiscriminatedUnion128),
            DiscriminatedUnionType.Object128 => nameof(DiscriminatedUnion128Object),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}