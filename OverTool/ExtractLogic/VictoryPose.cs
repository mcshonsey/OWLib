﻿using System.Collections.Generic;
using System.IO;
using CASCExplorer;
using OWLib;
using OWLib.Types;

namespace OverTool.ExtractLogic {
    public class VictoryPose {
        public static void Extract(ulong key, STUD stud, string output, string heroName, string name, string itemGroup, Dictionary<ushort, List<ulong>> track, Dictionary<ulong, Record> map, CASCHandler handler, List<char> furtherOpts) {
            string dest = string.Format("{0}{1}{2}{1}{3}{1}{5}{1}{4}{1}", output, Path.DirectorySeparatorChar, Util.Strip(Util.SanitizePath(heroName)), Util.SanitizePath(stud.Instances[0].Name), Util.SanitizePath(name), Util.SanitizePath(itemGroup));

            if (!Directory.Exists(dest)) {
                Directory.CreateDirectory(dest);
            }

            Dictionary<ulong, ulong> animList = new Dictionary<ulong, ulong>();
            HashSet<ulong> models = new HashSet<ulong>();
            Dictionary<ulong, List<ImageLayer>> layers = new Dictionary<ulong, List<ImageLayer>>();

            Skin.FindAnimations(key, animList, new Dictionary<ulong, ulong>(), new HashSet<ulong>(), map, handler, models, layers, key);
            Skin.Save(null, dest, heroName, name, new Dictionary<ulong, ulong>(), new HashSet<ulong>(), models, layers, animList, new List<char> { }, track, map, handler, 0, true);
        }
    }
}
