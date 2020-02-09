using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	public class LabelSetting : SettingBase<string> {
		public override string Name { get; set; }

		[Obsolete("Label setting does not have a value, only a Name!")]
		public override string Value { get; set; }

		[Obsolete("Label settings value never changes!")]
		public override Action<string> OnValueChanged { get; set; }
	}
}