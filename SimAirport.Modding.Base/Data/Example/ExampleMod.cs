using SimAirport.Modding.Base;
using SimAirport.Modding.Data.ImplementationDetails.Example;
using SimAirport.Modding.Data.NewInternals;
using SimAirport.Modding.Settings;
using TerrainTools;
using UnityEngine;

namespace SimAirport.Modding.Data.Example {

    public class ExampleMod : BaseMod {

        public override string InternalName => "Example.Mod.Internal.Name";
        public override string Name => "example.mod.name"; // i18n not working: i18n.Get("example.mod.name");
        public override string Description => "example.mod.desc"; // i18n not working: i18n.Get("example.mod.desc");
        public override string Author => "BR146";
        public override SettingManager SettingManager { get; set; }

        public override void OnTick() { }

        public ExampleMod() : base() {
            // This should happen internally
            new NeedManager(NeedManagerInternals.InternalRegisterNeed, NeedManagerInternals.InternalUnregisterNeed);
            new ZoneManager(ZoneManagerInternals.InternalRegisterZone, ZoneManagerInternals.InternalUnregisterZone);
        }


        private ZoneTool exampleZoneTool;

        public override void OnLoad(global::GameState state) {

            exampleZoneTool = ZoneManager.Instance.RegisterZone<ExampleZone>("example.zone.guid", new ZoneToolConfig() {
                ZoneSpriteKey = "example.zone.sprite", // set key for sprite used for zone overlay 
                ToolSpriteKey = "example.tool.sprite", // set key for sprite used as icon in zone menu
                I18nNameKey   = "example.zone.name",   // i18n for zone name (overlay & zone menu)
                I18nDescKey   = "example.zone.desc",   // i18n for zone desc in zone menu

                requiresEnclosedSpace = true,
                requiredMinSize = new Vector2(2, 3),
                maximum_of_zone_type = -1
            });

            NeedManager.Instance.RegisterNeed<ExampleNeed>();
        }

        public override void OnDisabled() {
            // Not exactly sure what will happen on save/load

            ZoneManager.Instance.UnregisterZone(exampleZoneTool);

            NeedManager.Instance.UnregisterNeed<ExampleNeed>();
        }

    }

}
