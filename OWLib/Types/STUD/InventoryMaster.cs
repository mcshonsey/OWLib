﻿using System.IO;
using System.Runtime.InteropServices;

namespace OWLib.Types.STUD {
    [System.Diagnostics.DebuggerDisplay(OWLib.STUD.STUD_DEBUG_STR)]
    public class InventoryMaster : ISTUDInstance {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct InventoryMasterHeader {
            public STUDInstanceInfo instance;
            public ulong zero1;
            public ulong achievableOffset;
            public ulong zero2;
            public ulong zero2a;
            public ulong zero2b;
            public ulong zero2c;
            public ulong defaultOffset;
            public ulong zero3;
            public ulong itemOffset;
            public ulong zero4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct InventoryMasterGroup {
            public ulong zero1;
            public ulong offset;
            public ulong zero2;
            public ulong @event;
        }

        public uint Id => 0x33F56AC1;
        public string Name => "Inventory Master";

        private InventoryMasterHeader header;
        public InventoryMasterHeader Header => header;

        private OWRecord[] achievableItems;
        private OWRecord[][] defaultGroupItems;
        private OWRecord[][] itemGroupItems;
        public OWRecord[] Achievables => achievableItems;
        public OWRecord[][] Defaults => defaultGroupItems;
        public OWRecord[][] Items => itemGroupItems;

        private InventoryMasterGroup[] defaultGroups;
        private InventoryMasterGroup[] itemGroups;
        public InventoryMasterGroup[] DefaultGroups => defaultGroups;
        public InventoryMasterGroup[] ItemGroups => itemGroups;

        public void Read(Stream input, OWLib.STUD stud) {
            using (BinaryReader reader = new BinaryReader(input, System.Text.Encoding.Default, true)) {
                header = reader.Read<InventoryMasterHeader>();

                if (header.achievableOffset > 0) {
                    input.Position = (long)header.achievableOffset;
                    STUDArrayInfo ptr = reader.Read<STUDArrayInfo>();
                    achievableItems = new OWRecord[ptr.count];
                    input.Position = (long)ptr.offset;
                    for (ulong i = 0; i < ptr.count; ++i) {
                        achievableItems[i] = reader.Read<OWRecord>();
                    }
                } else {
                    achievableItems = new OWRecord[0];
                }

                if (header.defaultOffset > 0) {
                    input.Position = (long)header.defaultOffset;
                    STUDArrayInfo ptr = reader.Read<STUDArrayInfo>();
                    defaultGroups = new InventoryMasterGroup[ptr.count];
                    defaultGroupItems = new OWRecord[ptr.count][];
                    input.Position = (long)ptr.offset;
                    for (ulong i = 0; i < ptr.count; ++i) {
                        defaultGroups[i] = reader.Read<InventoryMasterGroup>();
                    }

                    for (ulong i = 0; i < ptr.count; ++i) {
                        input.Position = (long)defaultGroups[i].offset;
                        STUDArrayInfo ptr2 = reader.Read<STUDArrayInfo>();
                        defaultGroupItems[i] = new OWRecord[ptr2.count];
                        input.Position = (long)ptr2.offset;
                        for (ulong j = 0; j < ptr2.count; ++j) {
                            defaultGroupItems[i][j] = reader.Read<OWRecord>();
                        }
                    }
                } else {
                    defaultGroupItems = new OWRecord[0][];
                    defaultGroups = new InventoryMasterGroup[0];
                }

                if (header.itemOffset > 0) {
                    input.Position = (long)header.itemOffset;
                    STUDArrayInfo ptr = reader.Read<STUDArrayInfo>();
                    itemGroups = new InventoryMasterGroup[ptr.count];
                    itemGroupItems = new OWRecord[ptr.count][];
                    input.Position = (long)ptr.offset;
                    for (ulong i = 0; i < ptr.count; ++i) {
                        itemGroups[i] = reader.Read<InventoryMasterGroup>();
                    }

                    for (ulong i = 0; i < ptr.count; ++i) {
                        if (itemGroups[i].offset > 0) {
                            input.Position = (long)itemGroups[i].offset;
                            STUDArrayInfo ptr2 = reader.Read<STUDArrayInfo>();
                            itemGroupItems[i] = new OWRecord[ptr2.count];
                            input.Position = (long)ptr2.offset;
                            for (ulong j = 0; j < ptr2.count; ++j) {
                                itemGroupItems[i][j] = reader.Read<OWRecord>();
                            }
                        } else {
                            itemGroupItems[i] = new OWRecord[0];
                        }
                    }
                } else {
                    itemGroupItems = new OWRecord[0][];
                    itemGroups = new InventoryMasterGroup[0];
                }
            }
        }
    }
}
