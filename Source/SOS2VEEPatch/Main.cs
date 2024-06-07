using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace SOS2VEEPatch
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main() //our constructor
        {
            Log.Message("SOS2VEEPatch loaded"); 
        }
    }
}
