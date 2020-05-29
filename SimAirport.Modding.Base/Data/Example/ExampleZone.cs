using System.Text;

namespace SimAirport.Modding.Data.ImplementationDetails.Example {
    public class ExampleZone : Zone {
        protected ExampleZone(ZoneType type) : base(type) {
        }

        private void ACheck(out bool isOnLevel) {
            isOnLevel = Level == 2; // only functional on Level 2
        }

        public override bool MeetsRequirements(bool cacheBuster = false) {
            if( ShouldCheckRequirements(cacheBuster) ) {
                base.MeetsRequirements(cacheBuster);

                ACheck(out bool isOnLevel);

                cachedMet &= isOnLevel;
            }

            return cachedMet;
        }

        public override StringBuilder GetFormattedErrors() {
            base.GetFormattedErrors();

            ACheck(out bool isOnLevel);

            Util.passfailAppend(isOnLevel, i18n.Get("example.zone.requires.level2"), null, formatted_errors);

            return formatted_errors;
        }
    }
}
