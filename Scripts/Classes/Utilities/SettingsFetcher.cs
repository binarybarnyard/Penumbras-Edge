using Godot;

namespace gamejam15.Scripts.Classes.Utilities
{
    public static class SettingsFetcher
    {
        public static float Gravity()
        {
            return ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
        }
    }
}