using HarmonyLib;
using SimAirport.Modding.Data.NewInternals;
using System;
using System.Collections.Generic;
using System.Reflection;
using TerrainTools;
using UnityEngine;

namespace SimAirport.Modding.Data.ImplementationDetails {
    class ZonePatches {

        public class ZoneToolConfig_Modifications : ZoneToolConfig {
            // expected Additions/Changes to ZoneToolConfig:

            public string I18nNameKey = "UI.zones.zone.name"; // fill with default key for "Zone"
            public string I18nDescKey = "UI.zones.zone.desc"; // fill with default key
            public string ZoneSpriteKey = "ZoneDiagonal_???"; // fill with default sprite key
            public string ToolSpriteKey = "ZoneDiagonal_???"; // fill with default sprite key

            public string localizedNameKey { // may need some refinement for zone menu to show prefix "[MOD]"
                get {
                    if( zone_type == Zone.ZoneType.Modded ) {
                        return I18nNameKey;
                    } else {
                        return $"UI.zones.{zone_type.ToString()}.name";
                    }
                }
            }

            public string localizedDescription {
                get {
                    if( zone_type == Zone.ZoneType.Modded ) {
                        return i18n.Get(I18nDescKey);
                    } else {
                        return i18n.Get($"UI.zones.{zone_type.ToString()}.description");
                    }
                }
            }
            public Sprite zoneSprite {
                get {
                    if( zone_type == Zone.ZoneType.Modded ) {
                        return SpriteManager.Get(ZoneSpriteKey);
                    } else {
                        return SpriteManager.Get("ZoneDiagonal_" + spriteSuffix);
                    }
                }
            }
        }

        public class ZoneTool_Modifications : TerrainTools.ZoneTool {
            public string spriteName {
                get {
                    if( zone_config.zone_type == Zone.ZoneType.None ) {
                        return "ZoneRemoveIcon";
                    } else if( zone_config.zone_type == Zone.ZoneType.Modded ) {
                        return zone_config.ToolSpriteKey;
                    } else {
                        return "ZoneDiagonal_" + zone_config.spriteSuffix;
                    }
                }
            }
        }



        /***
         * Helper to access ZoneTool.ZTools
         * 
         * The "one" zone tool storage dict.
         */
        public static Dictionary<string, ZoneTool> GetZTools() {
            // Get ZoneTool.ZTools
            var ZTools_raw = typeof(ZoneTool).GetField("ZTools", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).GetValue(null);

            return ZTools_raw as Dictionary<string, ZoneTool>;
        }


        /***************************************************
         * 
         *            Zone Type List & Lookup
         * 
         ***************************************************/


        /***
         * Extend lookup also into modded zones
         */
        [HarmonyPatch(typeof(ZoneToolConfig))]
        [HarmonyPatch(nameof(ZoneToolConfig.GetZoneToolConfigByType))]
        static class ZoneToolConfig_GetZoneToolConfigByType {

            static void Postfix(ref ZoneToolConfig __result, Zone.ZoneType zt) {
                if( __result == null ) {

                    if( ZoneManagerInternals.IsZoneTypeModding(zt) ) {
                        var uid = ZoneManagerInternals.ModdedZoneTypeToUid(zt);

                        var ZTools = GetZTools();

                        __result = ZTools[uid].zone_config;
                    }
                }
            }
        }

        /***
        * Delegate the custom zone creation for modded zone zools
        */
        [HarmonyPatch(typeof(Zone))]
        [HarmonyPatch(nameof(Zone.Create))]
        [HarmonyPatch(new Type[] { typeof(Zone.ZoneType), typeof(int) })]
        static class Patch_Zone_Create_ZoneType {

            static bool Prefix(Zone.ZoneType type, int predetermined_guid, ref Zone __result) {
                // if it's a modded type, delegate to Handler

                if( ZoneManagerInternals.IsZoneTypeModding(type) ) {
                    var uid = ZoneManagerInternals.ModdedZoneTypeToUid(type);

                    __result = ZoneManagerInternals.CreateZone(uid, predetermined_guid);

                    return false;
                }

                return true;
            }
        }


        /***********************************************
         * 
         * The lower stuff is transforming the zone type
         * into the zone uid and vice versa.
         * 
         * It's mainly for piping the type through the
         * ZoneType Enum.
         * 
         ***********************************************/


        /***
         * Translate zone_type (int) => zone name
         * 
         * Savegames require unique persistent zone names, but the name is derived
         * from the used Zone_Type Enum. Custom zones do use an integer internally,
         * which is not nessesarily persistent over multiple loads.
         * 
         * This was the best solution I came up with, postfixing the Enum.ToString method:
         * 
         * For the ZoneType Enum, it looksup the zone tool name.
         */
        [HarmonyPatch(typeof(Enum), "ToString", new Type[] { })]
        static class Patch_Enum_ToString {
            static void Postfix(Enum __instance, ref string __result) {
                if( __instance.GetType().Equals(typeof(Zone.ZoneType)) ) {
                    if( int.TryParse(__result, out int asInt) ) {
                        __result = ZoneManagerInternals.ModdedZoneTypeToUid((Zone.ZoneType) asInt);
                    }
                }
            }
        }


        /***
         * Reverse translation zone name => zone_type (int)
         * 
         * Reverse mapping of the above mapping.
         * 
         * The string is loaded from savegame and is translated to the current zone_type.
         * For this, the path_string is prepared to only contain a number to be parsed successfully.
         */
        [HarmonyPatch(typeof(Zone))]
        [HarmonyPatch(nameof(Zone.Create))]
        [HarmonyPatch(new Type[] { typeof(string), typeof(int) })]
        static class Patch_Zone_Create_string {

            static void Prefix(ref string path_string, int predetermined_guid) {
                // normal path_string format: ZONE/LOOKUP42
                // modify path_string, so path_string.Replace("ZONE/", "") => 42

                var s = path_string.Replace("ZONE/", "");

                var type = ZoneManagerInternals.ModdedUidToZoneType(s);

                if( type != null ) {
                    path_string = ((int) type).ToString();
                }
            }
        }


    }
}
