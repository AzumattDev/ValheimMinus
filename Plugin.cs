using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using Mono.Cecil;

namespace ValheimMinus;

[HarmonyPatch]
public class ValheimMinusPatcher
{
    internal const string ModName = "ValheimMinus";
    internal const string ModVersion = "1.0.0";
    internal const string Author = "Azumatt";
    private const string ModGUID = Author + "." + ModName;


    public static readonly ManualLogSource ValheimMinusLogger =
        Logger.CreateLogSource(ModName);

    public static IEnumerable<string> TargetDLLs { get; } = Array.Empty<string>();

    public static void Patch(AssemblyDefinition assembly)
    {
    }

    [UsedImplicitly]
    public static void Initialize()
    {
        IEnumerable<string> fileList = Directory
            .EnumerateFiles(Paths.BepInExRootPath, "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".cfg") || s.EndsWith(".dll"));
        foreach (string s in fileList)
        {
            string? filename = Path.GetFileName(s);
            if (filename is not ("valheim_plus.cfg" or "ValheimPlus.dll")) continue;
            ValheimMinusLogger.LogDebug(filename + " found, attempting to delete.");
            try
            {
                File.Delete(s);
            }
            catch (Exception exception)
            {
                ValheimMinusLogger.LogError(filename +
                                            $" found, but the attempt to delete the file failed with the following error: {Environment.NewLine}{exception}");
            }
        }
    }
}