﻿// File auto generated by STUHashTool

using System.Diagnostics.CodeAnalysis;
using static STULib.Types.Generic.Common;

namespace STULib.Types {
    [STU(0x7C3457DC)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class STUHero : STUInstance {
        [STUField(0x0EDCE350)]
        public STUGUID Name;

        [STUField(0x26D71549)]
        public STUGUID StatescriptHeroComponent1;

        [STUField(0x2C54AEAF)]
        public STUGUID VirtualReference1;

        [STUField(0x322C521A)]
        public STUGUID StatescriptHeroComponent2;

        [STUField(0x3446F580, "m_description")]
        public STUGUID Description;

        [STUField(0x38BFB46C)]
        public STUGUID AssetEncryptionKey;

        [STUField(0x44D13CC2)]
        public uint HeroID;

        [STUField(0x485AA39C)]
        public STUGUID LootboxUnlocks;

        [STUField(0x62746D34)]
        public byte UnknownBool1;

        [STUField(0x646B9249)]
        public byte UnknownBool2;

        [STUField(0x77FED604)]
        public STUGUID[] Abilities;

        [STUField(0x7D88A63A)]
        public uint Unknown12;

        [STUField(0x8125713E)]
        public STUGUID StatescriptHeroComponent3;

        [STUField(0x84625AA3)]
        public WeaponSkin[] WeaponSkins;

        [STUField(0x950BBA06)]
        public STUGUID StatescriptHeroComponent4;

        [STUField(0xA341183E)]
        public UnknownNested1[] Unknown16;

        [STUField(0xAC91BECC)]
        public STUGUID StatescriptHeroComponent5;

        [STUField(0xB7A1D145)]
        public int UnknownID;  // weird, sometimes works as hero type, but not always

        [STUField(0xC2FE396F)]
        public Statescript[] WeaponComponents1;

        [STUField(0xD696F2F6)]
        public STUGUID ImageResource1;

        [STUField(0xD90B256D)]
        public STUGUID ImageResource2;

        [STUField(0xDAD2E3A2)]
        public STUGUID ImageResource3;

        [STUField(0xE04197AF)]
        public STUGUID[] GUIDx0C6_Reference;

        [STUField(0xE25DDDA1)]
        public STUVec4 UnknownVec4;

        [STUField(0xEA6FF023)]
        public STUGUID ImageResource4;

        [STUField(0xF2D8DE15)]
        public STUGUID[] VirtualReference2;

        [STUField(0xFC833C02)]
        public StatisticGroup[] Statistics;

        [STUField(0xFCD2B649)]
        public Statescript[] WeaponComponents2;

        [STUField(0xFF3C2071)]
        public STUGUID[] Achievements;  // not all of them...

        [STUField(0x418F797D)]
        public UnknownNested2[] Unknown30;

        [STUField(0xD12CB4EA)]
        public STUGUID[] Unknown31;

        [STUField(0xAF4EC410)]
        public uint Unknown32;

        [STUField(0xBB16810A)]
        public uint Unknown33;

        [STUField(0xE1258EC1)]
        public UnknownNested3[] Unknown34;

        public class WeaponSkin {
            [STUField(0x0029461B)]
            public STUGUID Metadata;

            [STUField(0x5E9665E3)]
            public STUGUID SkinOverride;

            [STUField(0x7B95C2A2)]
            public byte WeaponIndex;

            [STUField(0x88F5CF3E)]
            public STUGUID[] WeaponOverrides;
        }

        public class UnknownNested1 {
            [STUField(0x118D9D9F)]
            public StatisticGroup[] Statistics;  // todo: check nested array

            [STUField(0xEB4F2408)]
            public STUGUID GUIDx0C5_Reference;  //todo: check if STUGUID
        }

        public class Statescript {
            [STUField(0x0827AB28)]
            public STUVec3 UnknownVec3_a;

            [STUField(0x18789D20)]
            public STUVec3 UnknownVec3_b;

            [STUField(0x38F997AB)]
            public STUGUID Component;

            [STUField(0xAF9D3A0C)]
            public STUVec4 UnknownVec4;

            [STUField(0xFF72C038)]
            public STUVec3 UnknownVec3_c;
        }

        public class StatisticGroup {
            [STUField(0x07EC21F2)]
            public STUGUID Statistic1;

            [STUField(0xB5B91051)]
            public STUGUID Statistic2;

            [STUField(0xBC4326FF)]
            public STUGUID Statistic3;
        }

        public class UnknownNested2 {
            [STUField(0x9A97C666)]
            public uint Unknown1;

            [STUField(0xB44A42A0)]
            public float Unknown2;
        }

        public class UnknownNested3 {
            [STUField(0x925E7392)]
            public uint Unknown1;

            [STUField(0x96D9482C)]
            public STUGUID[] StateScriptCompontents;
        }
    }
}
    