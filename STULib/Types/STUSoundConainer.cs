// File auto generated by STUHashTool
using static STULib.Types.Generic.Common;

namespace STULib.Types {
    [STU(0xF746901F)]
    [STUSuppressWarning(0xF746901F, STUWarningType.MissingField, 0x5683B253)]
    // suppress the missing field 5683B253 in F746901F
    [STUSuppressWarning(0xF746901F, STUWarningType.MissingInstance, InstanceUsage.Embed, 0x5683B253, 0x4493ED2C)]
    // suppress the missing instance 4493ED2C embedded in F746901F:5683B253
    public class STUSoundConainer : STUInstance {  // todo: these names are so bad
        //[STUField(0x5683B253)]
        //public STU_4493ED2C[] m_5683B253;  // 0 to 12 // todo: I don't care about this instance right now

        [STUField(0x798027DE)]
        public STUSoundWrapper Sound1;

        [STUField(0xA84AA2B5)]
        public STUSoundWrapper Sound2;

        [STUField(0xD872E45C)]
        public STUSoundWrapper Sound3;

        [STUField(0x1485B834)]
        public STUSoundWrapper Sound4;
    }
}
