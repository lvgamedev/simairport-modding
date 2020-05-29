using Needs;
using UnityEngine;

namespace SimAirport.Modding.Data.Example {
    class ExampleNeed : Need {
        public string I18nKeyName => "example.need.name";

        private readonly NeedConfig agentConfig;
        protected override NeedConfig config => agentConfig;

        public override bool impactsSatisfaction => true;



        public ExampleNeed() {
            agentConfig = ScriptableObject.CreateInstance<NeedConfig>();

            agentConfig.Init = new Vector2(0.7f, 1.0f); // start score
            agentConfig.Tick = 1f / (6f * 60f * 18f);  // how fast the need is rising? % per tick @ 18 ticks per second? NOTE: score = Mathf.Clamp01(score + tick * deltaTime);
            agentConfig.Activation = 0.1f; // threshold to trigger end of need satificaton?

            agentConfig.curve = new ThreadsafeCurve();
            agentConfig.curve.SetCurve(AnimationCurve.Linear(0, 0, 1, 1));
        }

        public override void Configure() {
            if( agent.isExecutive ) { // executives don't need this need ;)
                enabled = false;
            }
        }
    }
}
