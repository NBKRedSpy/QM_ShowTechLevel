using HarmonyLib;
using MGSC;
using ShowTechLevel_Bootstrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShowTechLevel
{
    public class Plugin : BootstrapMod
    {
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, "config.json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);


        public static State State;

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {
            hookEvents.BeforeBootstrap += BeforeBootstrap;
        }

        public void BeforeBootstrap(IModContext context)
        {
            State = context.State;

            new Harmony("NBK_RedSpy_" + ModAssemblyName).PatchAll();
        }

    }
}
