using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using TMPro;
using UnityEngine;

namespace QM_GraphicsResolutionToTop
{
    [HarmonyPatch(typeof(GraphicsPage), nameof(GraphicsPage.InitResolutionsDropdown))]
    internal static class GraphicsPage_InitResolutionsDropdown_Patch
    {
        public static ResolutionConfig Resolution { get; set; } = new ResolutionConfig() { Width = 1366, Height = 768, RefreshRate = 60 };

        /// <summary>
        /// The sorted resolutions.  Required for the resolution change event since it uses the Unity screen array.
        /// </summary>
        public static List<Resolution> NewResolutionOrder;

        public static bool Prefix(GraphicsPage __instance)
        {

            //This is the full copy of the game's code.

            Resolution[] resolutions = Screen.resolutions;

            //----------------- Start Change

            List<Resolution> sortedResolutions = new List<Resolution>();


            sortedResolutions = resolutions.OrderByDescending(x => x.width == Resolution.Width && x.height == Resolution.Height && x.refreshRate == Resolution.RefreshRate)
                .ToList();

            //Move the highest value to the top as well.
            sortedResolutions.Insert(1, sortedResolutions[sortedResolutions.Count - 1]);
            sortedResolutions.RemoveAt(sortedResolutions.Count - 1);

            NewResolutionOrder = sortedResolutions;

            resolutions = sortedResolutions.ToArray();

            //----------------- End Change

            List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>(resolutions.Length);
            int valueWithoutNotify = 0;
            Resolution[] array = resolutions;

            for (int i = 0; i < array.Length; i++)
            {
                Resolution resolution = array[i];
                if (resolution.Equals(SingletonMonoBehaviour<GameSettings>.Instance.Resolution))
                {
                    valueWithoutNotify = list.Count;
                }
                list.Add(new TMP_Dropdown.OptionData(resolution.width + "x" + resolution.height + " : " + resolution.refreshRate));
            }
            __instance._resolutionDropdown.ClearOptions();
            __instance._resolutionDropdown.AddOptions(list);
            __instance._resolutionDropdown.SetValueWithoutNotify(valueWithoutNotify);

            return false;
        }
    }
}
