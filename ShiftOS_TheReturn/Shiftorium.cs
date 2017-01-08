﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ShiftOS.Engine.SaveSystem;

namespace ShiftOS.Engine
{
    public static class Shiftorium
    {
        /// <summary>
        /// Whether or not shiftorium output should be written to the console.
        /// </summary>
        public static bool Silent = false;

        public static void InvokeUpgradeInstalled()
        {
            Installed?.Invoke();
        }


        public static bool Buy(string id, int cost)
        {
            if(SaveSystem.CurrentSave.Codepoints >= cost)
            {
                SaveSystem.CurrentSave.Upgrades[id] = true;
                TerminalBackend.InvokeCommand("sos.save");
                SaveSystem.TransferCodepointsToVoid(cost);
                Installed?.Invoke();
                Desktop.ResetPanelButtons();
                Desktop.PopulateAppLauncher();
                return true;
            }
            else
            {
                if(!Silent)
                    Console.WriteLine($"{{SHIFTORIUM_NOTENOUGHCP}}: {cost} > {SaveSystem.CurrentSave.Codepoints}");
                return false;
            }
        }

        public static bool UpgradeAttributesUnlocked(Type type)
        {
            foreach(var attr in type.GetCustomAttributes(true))
            {
                if(attr is RequiresUpgradeAttribute)
                {
                    var rAttr = attr as RequiresUpgradeAttribute;
                    return rAttr.Installed;
                }
            }

            return true;
        }

        public static bool UpgradeAttributesUnlocked(MethodInfo type)
        {
            foreach (var attr in type.GetCustomAttributes(true))
            {
                if (attr is RequiresUpgradeAttribute)
                {
                    var rAttr = attr as RequiresUpgradeAttribute;
                    return rAttr.Installed;
                }
            }

            return true;
        }

        public static bool UpgradeAttributesUnlocked(PropertyInfo type)
        {
            foreach (var attr in type.GetCustomAttributes(true))
            {
                if (attr is RequiresUpgradeAttribute)
                {
                    var rAttr = attr as RequiresUpgradeAttribute;
                    return rAttr.Installed;
                }
            }

            return true;
        }

        public static bool UpgradeAttributesUnlocked(FieldInfo type)
        {
            foreach (var attr in type.GetCustomAttributes(true))
            {
                if (attr is RequiresUpgradeAttribute)
                {
                    var rAttr = attr as RequiresUpgradeAttribute;
                    return rAttr.Installed;
                }
            }

            return true;
        }


        public static void Init()
        {
            try
            {
                var dict = GetDefaults();
                foreach (var itm in dict)
                {
                    if (!SaveSystem.CurrentSave.Upgrades.ContainsKey(itm.ID))
                    {
                        SaveSystem.CurrentSave.Upgrades.Add(itm.ID, false);
                    }
                }
            }
            catch
            {
            }
        }

        public static int GetCPValue(string id)
        {
            foreach(var upg in GetDefaults())
            {
                if (upg.ID == id)
                    return upg.Cost;
            }
            return 0;
        }

        public static ShiftoriumUpgrade[] GetAvailable()
        {
            List<ShiftoriumUpgrade> available = new List<ShiftoriumUpgrade>();
            foreach(var defaultupg in GetDefaults())
            {
                if (!UpgradeInstalled(defaultupg.ID) && DependenciesInstalled(defaultupg))
                    available.Add(defaultupg);
            }
            return available.ToArray();
        }

        public static bool DependenciesInstalled(ShiftoriumUpgrade upg)
        {
            if (string.IsNullOrEmpty(upg.Dependencies))
            {
                return true;//root upgrade, no parents
            }
            else if (upg.Dependencies.Contains(";"))
            {
                string[] dependencies = upg.Dependencies.Split(';');
                foreach(var dependency in dependencies)
                {
                    if (!UpgradeInstalled(dependency))
                        return false;
                }
                return true;
            } 
            else
            {
                return UpgradeInstalled(upg.Dependencies);
            }
        }

        public static event EmptyEventHandler Installed;

        public static bool UpgradeInstalled(string id)
        {
            if (SaveSystem.CurrentSave != null)
            {
                if (SaveSystem.CurrentSave.Upgrades == null)
                    Init();
            }
            try
            {
                return SaveSystem.CurrentSave.Upgrades[id];
            }
            catch (Exception ex)
            {
                if(LogOrphanedUpgrades == true)
                    Console.WriteLine($"WHOA, Developers! Upgrade ID '{id}' is unaccounted for in the Shiftorium.txt resource!");
                return false;
            }
        }

        //LEAVE THIS AS FALSE. The game will set it when the save is loaded.
        public static bool LogOrphanedUpgrades = false;

        private static IShiftoriumProvider _provider = null;

        public static void RegisterProvider(IShiftoriumProvider p)
        {
            _provider = p;
        }

        //Bless the newer NEWER engine.
        public static List<ShiftoriumUpgrade> GetDefaults()
        {
            try
            {
                return _provider.GetDefaults();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't get the upgrade definition list from the provider.");
                Console.WriteLine("This might be able to help:");
                Console.WriteLine(ex);
                return JsonConvert.DeserializeObject<List<ShiftoriumUpgrade>>(Properties.Resources.Shiftorium);
            }
        }
    }

    public interface IShiftoriumProvider
    {
        List<ShiftoriumUpgrade> GetDefaults();
    }

    public class ShiftoriumUpgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string ID { get { return (this.Id != null ? this.Id : (Name.ToLower().Replace(" ", "_"))); } }
        public string Id { get; }

        public string Dependencies { get; set; }
    }
}
