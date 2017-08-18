﻿using System;

namespace STULib {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class STUFieldAttribute : Attribute {
        public bool ReferenceArray = false;
        public bool ReferenceValue = false;
        public object Verify = null;
        public long Padding = 0;

        public object Default = null;

        public uint Checksum = 0;

        public uint[] IgnoreVersion = null;
        public uint[] STUVersionOnly = null;

        public STUFieldAttribute() {}

        public STUFieldAttribute(uint Checksum) {
            this.Checksum = Checksum;
        }
    }
}