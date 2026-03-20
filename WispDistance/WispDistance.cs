using BepInEx;
using HarmonyLib;
using System.Linq;

namespace WispDistance
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInIncompatibility("Turbero.BiomeConqueror")]
    public class WispDistance : BaseUnityPlugin
    {
        public const string GUID = "Turbero.WispDistance";
        public const string NAME = "Wisp Distance";
        public const string VERSION = "1.0.2";

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

    [HarmonyPatch(typeof(Demister), "OnEnable")]
    public class DemisterPatch
    {
        static void Postfix(ref Demister __instance)
        {
            if (Player.m_localPlayer == null) return;
            var demisterSE = Player.m_localPlayer.GetSEMan().GetStatusEffects().FirstOrDefault(effect => effect.name == "Demister");
            if (demisterSE != null)
                demisterSE.m_name = "$item_demister" + $": {__instance.m_forceField.endRange} m.";
        }
    }
}