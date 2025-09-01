﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace SOS2VEEPatch
{
    internal class HarmonyUtils
    {
        //判断该地图是否为SOS2的太空地图。
        public static bool IsSOS2SpaceMap(Map map)
        {
            var traverse = Traverse.Create(map);
            var isSpaceMethod = traverse.Method("IsSpace");
            if (isSpaceMethod.MethodExists() && (bool)isSpaceMethod.GetValue())
            {
                return true;
            }

            else if (map.Biome.defName.Contains("OuterSpace"))
            {
                return true;
            }

            return false;
        }

        public static bool IsRimNauts2SpaceMap(Map map)
        {
            return map.Biome.defName.StartsWith("RimNauts2_");
        }

        public static bool IsOdessySpaceMap(Map map)
        {
            return map.Biome == BiomeDefOf.Space || map.Biome == BiomeDefOf.Orbit;
        }

        public static bool IsSpaceMap(Map map)
        {
            return IsSOS2SpaceMap(map) || IsRimNauts2SpaceMap(map) || IsOdessySpaceMap(map);
        }
    }
}
