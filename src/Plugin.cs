﻿using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_GraphicsResolutionToTop
{
    public static class Plugin
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, ModAssemblyName + ".json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);
        public static ModConfig Config { get; private set; }


        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Config = ModConfig.LoadConfig(ConfigPath);

            new Harmony("_" + ModAssemblyName).PatchAll();
        }


    }
}
