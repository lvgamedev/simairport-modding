# Custom Options

Here is a guide onto using `SimAirport.Modding.Settings`, letting the game handle saving and rendering of the data.

## Setup

Begin by importing `SimAirport.Modding.Settings` and adding this to your `OnSettingsLoaded()`

## Usage

Here's a basic setting which adds a toggle option in the game.

```cs
namespace name.MyMod
{
  public class Mod : BaseMod {
    // ... your vars or funcs

    // This is the settings manager that will handle saving, loading, and rendering of the settings.
    // The game will handle setting this, so you do not need to worry about it.
    public Settings.SettingManager SettingManager { get; set; }

    public override void OnSettingsLoaded() {
      // Create a checkbox with a key "myKey_checkbox" which can be used to access later!
      SettingManager.AddDefault("myKey_checkbox", new CheckboxSetting {
        Name = "Check me!",
        Value = false, // this is the default value
      });

      // Now we can access it! It will either be false or whatever it got saved
      var success = SettingManager.TryGetBool("myKey_checkbox", out var statement);
      // `success` var is either TRUE or FALSE depending if it failed fetching the saved value.
      // the `statement` var returns the saved or default value of the setting

      // You can do a more direct one using this
      var directToThePoint = SettingManager.GetOrDefault("myKey_checkbox", false);
      // `directToThePoint` var is direct to the point

      // this is to remove the setting values. During development it is good to clear the options
      // as some values gets saved, such as: 
      // Name (All), Min (SliderSetting), Max (SliderSetting), Stepping (SliderSetting),
      // ValueFormat (SliderSetting)
      SettingManager.RemoveSetting("myKey_checkbox");
    }

    // ... your vars or funcs
  }
}
```

> **Reminder! It's recommended to have a `RemoveSetting()` on all of your keys during development as it saves some value that you might change during development.**

You can find out other setting types in the `SimAirport.Modding.Settings`, all of them are prefixed with `Setting` (eg: `StringSetting`).
