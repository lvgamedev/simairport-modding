using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data.NewInternals {
    class NeedManagerInternals {
        /// <summary>
        /// Internal global list with all modded registered needs.
        /// </summary>
        internal static List<Type> customNeedTypes = new List<Type>();

        internal static void InternalRegisterNeed(Type tNeed) {
            if( !customNeedTypes.Contains(tNeed) ) {
                customNeedTypes.Add(tNeed);
            }
        }

        internal static void InternalUnregisterNeed(Type tNeed) {
            customNeedTypes.Remove(tNeed);
        }
    }
}
