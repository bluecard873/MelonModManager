using MelonLoader;
using Main = MelonModManager.Main;

#region Assembly attributes

[assembly: MelonInfo(typeof(Main), PluginInfo.Name, PluginInfo.Version, PluginInfo.Author, PluginInfo.DownloadLink)]
[assembly: MelonColor()]
[assembly: MelonGame(null, null)]

#endregion

public static class PluginInfo
{
    public const string Name = "ModManager";
    public const string Version = "1.0.0";
    public const string Author = "Flower";
    public const string DownloadLink = null;
}