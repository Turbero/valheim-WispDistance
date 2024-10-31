using BepInEx;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace WispDistance
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class WispDistance : BaseUnityPlugin
    {
        public const string GUID = "Turbero.WispDistance";
        public const string NAME = "Wisp Distance";
        public const string VERSION = "1.0.1";

        private readonly Harmony harmony = new Harmony(GUID);

        void Awake()
        {
            harmony.PatchAll();
        }

        void onDestroy()
        {
            harmony.UnpatchSelf();
        }
    }

    [HarmonyPatch]
    public class MistlandsPatch
    {
        public static float demisterRange;

        static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Demister), "OnEnable");
        }

        static void Postfix(ref Demister __instance)
        {
            if (Player.m_localPlayer == null) return;
            var demisterSE = Player.m_localPlayer.GetSEMan().GetStatusEffects().FirstOrDefault(effect => effect.name == "Demister");
            if (demisterSE != null)
                demisterSE.m_name = $"$item_demister" + $": {__instance.m_forceField.endRange} m.";
        }
    }
}