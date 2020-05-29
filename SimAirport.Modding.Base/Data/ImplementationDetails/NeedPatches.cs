using Needs;
using Newtonsoft.Json;
using System;

namespace SimAirport.Modding.Data.NewInternals {
    class NeedPatches {


        /// <summary>
        /// Interface which modded needs _must_ implemenent.
        /// 
        /// It provides hooks for the ingame logic to call custom behaviour.
        /// 
        /// May be integrated directly into Needs.Need class?
        /// </summary>
        public class Need_Modifications : Need {

            /// <summary>
            /// Translation key for the need's name.
            /// </summary>
            [JsonIgnore]
            public string I18nKeyName { get; }

            [JsonIgnore]
            public string localizedName {
                get {
                    if( string.IsNullOrEmpty(I18nKeyName) ) {
                        return i18n.Get(string.Format("UI.strings.needs.{0}", GetType().ToString().Replace("Needs.", "")));
                    } else {
                        return i18n.Get(I18nKeyName);
                    }
                }
            }


            /// <summary>
            /// Called for each new agent (like pax, executives) to configure the need for
            /// each individually.
            /// </summary>
            public virtual void Configure() { }
        }



        /***
            * Attach hook to NeedManager.AddAllNeeds
            * 
            * AddAllNeeds adds all vanilla needs to the manager for a new agent.
            * Hook into this to attach all (currently) known custom needs.
            * 
            * Note: New needs will only affect new spawnt agents.
            */
        [HarmonyPatch(typeof(NeedManager))]
        [HarmonyPatch("AddAllNeeds")]
        static class NeedManager_AddAllNeeds {
            //private void AddAllNeeds()

            static void Postfix(global::Needs.NeedManager __instance) {
                foreach( var needType in NeedManagerInternals.customNeedTypes ) {
                    // NeedManager.Add<> is private and looking it up is more complex than just reimplement add.
                    // This block is almost identical to NeedManager.Add<T>

                    Need need = (Need)Activator.CreateInstance(needType);
                    need.SetAgent(__instance.agent);
                    __instance.needs.Add(need);
                    __instance.needsByType[need.GetType()] = need;
                }
            }
        }

        /***
            * Attach hook to NeedManager.Configure
            * 
            * Configure each custom need for the current agent, e. g. not active for managers.
            * As each need could be different, this does happen for each (custom) need.
            * 
            * With above integration of Configure() into Need class, we can simply call Configure() for every need.
            * But this depends if checking for modded need is heavier than running an empty virtual method...
            */
        [HarmonyPatch(typeof(global::Needs.NeedManager))]
        [HarmonyPatch(nameof(global::Needs.NeedManager.Configure))]
        static class NeedManager_Configure {
            //public void Configure()

            static void Postfix(global::Needs.NeedManager __instance) {
                foreach( var need in __instance.needs ) {
                    need.Configure();
                }
            }
        }
    }
}
