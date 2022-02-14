using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	public class LabelSetting : SettingBase<string> {
		/// <inheritdoc />
		public override string Name { get; set; }

		/// <summary>
		/// **DO NOT USE!** Label setting does not have a value, only a Name!
		/// </summary>
		[Obsolete("Label setting does not have a value, only a Name!")]
		public override string Value { get; set; }

		/// <summary>
		/// **DO NOT USE!** Label settings value never changes!
		/// </summary>
		[Obsolete("Label settings value never changes!")]
		public override Action<string> OnValueChanged { get; set; }
	}
}