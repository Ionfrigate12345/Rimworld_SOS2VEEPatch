using System;
using System.Linq;
using HarmonyLib;
using RimWorld;
using SaveOurShip2;
using VEE.RegularEvents;
using Verse;

namespace SOS2VEEPatch
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        private static Harmony _harmonyInstance;
        public static Harmony HarmonyInstance { get { return _harmonyInstance; } }

        static HarmonyPatches()
        {
            _harmonyInstance = new Harmony("com.ionfrigate12345.sos2veepatch");
            _harmonyInstance.PatchAll();
        }

        [HarmonyPatch(typeof(VEE.PurpleEvents.PsychicRain))]
        [HarmonyPatch("ForcedWeather")]
        public static class PsychicRain_ForcedWeather_Patch
        {
            public static bool Prefix(VEE.PurpleEvents.PsychicRain __instance)
            {
                var affectedSOS2Maps = __instance.AffectedMaps.Where(map => HarmonyUtils.IsSOS2OrRimNauts2SpaceMap(map)).ToList();
                if (affectedSOS2Maps.Count > 0)
                {
                    //Terminate psychic rain if it happens on any SOS2 space map.
                    __instance.suppressEndMessage = true;
                    __instance.End();
                    Log.Warning("[SOS2VEEPatch] VEE Psychic Rain terminated on SOS2 or Rimnauts2 space maps.");
                    return false;
                }
                return true; 
            }
        }

        [HarmonyPatch(typeof(VEE.PurpleEvents.PsychicRain))]
        [HarmonyPatch("GameConditionTick")]
        public static class PsychicRain_GameConditionTick_Patch
        {
            public static bool Prefix(VEE.PurpleEvents.PsychicRain __instance)
            {
                if (GenTicks.TicksAbs % 15 == 5) //Run less than once-per-tick for not causing performance issue.
                {
                    var affectedSOS2Maps = __instance.AffectedMaps.Where(map => HarmonyUtils.IsSOS2OrRimNauts2SpaceMap(map)).ToList();
                    if (affectedSOS2Maps.Count > 0)
                    {
                        //Terminate psychic rain if it happens on any SOS2 space map.
                        __instance.suppressEndMessage = true;
                        __instance.End();
                        Log.Warning("[SOS2VEEPatch] VEE Psychic Rain terminated on SOS2 or Rimnauts2 space maps.");
                        return false;
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(CaravanAnimalWI))]
        [HarmonyPatch("CanFireNowSub")]
        public static class CaravanAnimalWI_CanFireNowSub_Patch
        {
            public static bool Prefix(ref bool __result, IncidentParms parms)
            {
                __result = false;
                Log.Warning("[SOS2VEEPatch] VEE Caravan Animal Wander-In event banned for SOS2 balance (This event may give you archon animals).");
                return false;
            }
        }

        [HarmonyPatch(typeof(CaravanAnimalWI))]
        [HarmonyPatch("TryExecuteWorker")]
        public static class CaravanAnimalWI_TryExecuteWorker_Patch
        {
            public static bool Prefix(ref bool __result, IncidentParms parms)
            {
                __result = false;
                Log.Warning("[SOS2VEEPatch] VEE Caravan Animal Wander-In event banned for SOS2 balance (This event may give you archon animals).");
                return false;
            }
        }
    }
}