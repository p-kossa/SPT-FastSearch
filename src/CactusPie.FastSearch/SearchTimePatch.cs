using System;
using System.Reflection;
using Aki.Reflection.Patching;

namespace CactusPie.FastSearch
{
    public class SearchTimePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            MethodInfo method = typeof(GClass2676).GetMethod("GetNextDiscoveryTime", BindingFlags.Static | BindingFlags.Public);
            return method;
        }

        [PatchPostfix]
        public static void PatchPostFix(float speed, bool instant, ref DateTime __result)
        {
            if (instant)
            {
                return;
            }

            float searchTimeMultiplier = FastSearchPlugin.SearchTimeMultiplier.Value;

            if (searchTimeMultiplier == 0)
            {
                __result = GClass1190.Now;
                return;
            }
            
            __result = GClass1190.Now.AddTicks((long)((__result - GClass1190.Now).Ticks * searchTimeMultiplier));
        }
    }
}