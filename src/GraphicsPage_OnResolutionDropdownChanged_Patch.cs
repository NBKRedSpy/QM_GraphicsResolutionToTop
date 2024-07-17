using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_GraphicsResolutionToTop
{
    [HarmonyPatch(typeof(GraphicsPage), nameof(GraphicsPage.OnResolutionDropdownChanged))]
    internal static class GraphicsPage_OnResolutionDropdownChanged_Patch
    {
        static void Postfix(int arg0)
        {
            if(GraphicsPage_InitResolutionsDropdown_Patch.NewResolutionOrder != null)
            {
                List<Resolution> newResolutionOrder = GraphicsPage_InitResolutionsDropdown_Patch.NewResolutionOrder;
                SingletonMonoBehaviour<GameSettings>.Instance.Resolution = newResolutionOrder[arg0];
            }
            

        }
    }
}
