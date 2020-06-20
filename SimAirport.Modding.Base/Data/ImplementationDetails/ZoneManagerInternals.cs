using SimAirport.Modding.Data.ImplementationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using TerrainTools;
using UnityEngine;

namespace SimAirport.Modding.Data.NewInternals {
    internal static class ZoneManagerInternals {
        /*
         * The following is just for pipelining type information through Zone.ZoneType Enum
         */
        private const int zone_type_id_start = 1000;
        private static int _nextFreeZoneId = zone_type_id_start;
        private static int NextFreeZoneId {
            get {
                return _nextFreeZoneId++;
            }
        }

        private static readonly Dictionary<Zone.ZoneType, string> ZoneTypeToUniqueId = new Dictionary<Zone.ZoneType, string>();
        internal static bool IsZoneTypeModding(Zone.ZoneType type) {
            return (int) type >= zone_type_id_start;
        }

        internal static string ModdedZoneTypeToUid(Zone.ZoneType type) {
            if( !IsZoneTypeModding(type) ) {
                return type.ToString();
            } else {
                return ZoneTypeToUniqueId[type];
            }
        }
        internal static Zone.ZoneType? ModdedUidToZoneType(string uid) {
            return ZoneTypeToUniqueId.Where(e => e.Value == uid).Select(e => (Zone.ZoneType?) e.Key).FirstOrDefault();
        }

        /*
         * Pipeline Helpers End
         */

        // Map which Zone type is instanciated by which modded zone tool
        internal static Dictionary<string, Type> ToolToZoneType = new Dictionary<string, Type>();

        public static ZoneTool InternalRegisterZone(Type zoneType, string uniqueId, ZoneToolConfig config) { // where Type : Zone
            ZoneTool.LoadAll(); // force load of vanilla zones, else they will never be loaded!
            var ZTools = ZonePatches.GetZTools();

            if( !ZTools.TryGetValue(uniqueId, out ZoneTool zoneTool) ) {
                zoneTool = ScriptableObject.CreateInstance<ZoneTool>();

                config.zone_type = (Zone.ZoneType) NextFreeZoneId;  // currently missusing it to transport information to Zone.Create
                config.zone_type = Zone.ZoneType.Modded;           // this is what you have suggested

                zoneTool.zone_config = config;

                zoneTool.name = (config.name = uniqueId);
                zoneTool.uniqueID = uniqueId;
                zoneTool.i18nNameKey = config.I18nNameKey;
                zoneTool.i18nDescKey = config.I18nDescKey;

                ZoneTypeToUniqueId.Add(config.zone_type, uniqueId);

                ToolToZoneType.Add(uniqueId, zoneType);

                ZTools.Add(uniqueId, zoneTool);
            }
            return zoneTool;
        }

        public static void InternalUnregisterZone(ZoneTool zoneTool) {
            var ZTools = ZonePatches.GetZTools();

            if( zoneTool != null ) {
                string uniqueId = zoneTool.uniqueID;
                Zone.ZoneType zoneType = zoneTool.zone_type;

                ZTools.Remove(uniqueId);
                ToolToZoneType.Remove(uniqueId);
                ZoneTypeToUniqueId.Remove(zoneType);
            }
        }

        internal static Zone CreateZone(string uid, int predetermined_guid) {
            Type zonetype = ToolToZoneType[uid];
            Zone zone = (Zone)Activator.CreateInstance(zonetype, (Zone.ZoneType)zone_type_id_start );

            zone.iprefab.guid = predetermined_guid < 0 ? GUID.New() : predetermined_guid;
            Util.AddSafe(ref global::Game.current.iprefabs, zone.iprefab);

            return zone;
        }
    }
}
