using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace NoPostProcessing
{
	[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
	public class NoPostProccesingPlugin : BaseUnityPlugin
	{
		public const string pluginGuid = "tybikkryt.nopostproccesing";
		public const string pluginName = "NoPostProcessing";
		public const string pluginVersion = "1.0.0.0";

		public static ManualLogSource Log;

		public static GameObject ppm;
		public static GameObject ppo;

		private void Awake()
		{
			Logger.LogMessage("Patching...");
			Log = Logger;
			Harmony harmony = new(pluginGuid);
			harmony.PatchAll();
			Logger.LogMessage("Patching complete");
		}

		[HarmonyPatch(typeof(PostProcessVolume), "Update")]
		public class PostProcessVolumePatch
		{
			static void Postfix()
			{
				ppm = GameObject.Find("Post Processing Main");
				ppo = GameObject.Find("Post Processing Overlay");
				ppm.SetActive(false);
				ppo.SetActive(false);
			}

		}
	}
}
