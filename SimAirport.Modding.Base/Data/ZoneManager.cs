using System;
using TerrainTools;

namespace SimAirport.Modding.Data {
    public class ZoneManager {
        public static ZoneManager Instance { get; private set; }

        /// <summary>
        /// Creates singleton of this manager.
        /// </summary>
        /// <param name="internalRegisterZone">ZoneManagerInternal.InternalRegisterZone</param>
        /// <param name="internalUnregisterZone">ZoneManagerInternal.InternalUnregisterZone</param>
        public ZoneManager(Func<Type, string, ZoneToolConfig, ZoneTool> internalRegisterZone, Action<ZoneTool> internalUnregisterZone) {
            if( Instance != null )
                throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

            Instance = this;

            InternalRegisterZone = internalRegisterZone;
            InternalUnregisterZone = internalUnregisterZone;
        }

        /// <summary>
        /// Registers a new zone.
        /// </summary>
        /// <typeparam name="T">Zone Derivative with zone's logic</typeparam>
        /// <param name="uniqueId">globally unique zone identifier</param>
        /// <param name="config"></param>
        /// <returns>Zone Tool instance representing the new build tool</returns>
        public ZoneTool RegisterZone<T>(string uniqueId, ZoneToolConfig config) where T : Zone {
            return InternalRegisterZone(typeof(T), uniqueId, config);
        }
        private readonly Func<Type, string, ZoneToolConfig, ZoneTool> InternalRegisterZone;

        /// <summary>
        /// Remove a zone tool.
        /// </summary>
        /// <param name="which">The zone tool to be unregistered from the game.</param>
        public void UnregisterZone(ZoneTool which) {
            InternalUnregisterZone(which);
        }
        private readonly Action<ZoneTool> InternalUnregisterZone;
    }
}
