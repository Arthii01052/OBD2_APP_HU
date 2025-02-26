using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// OBD2 bemeneti parancsok kezelésével kapcsolatos osztály.
/// </summary>
public static class OBD2Codes
{
    /// <summary>
    /// Elérhető parancsok könyvtára.
    /// </summary>
    private static readonly Dictionary<string, string> dtcCodes = new Dictionary<string, string>
    {
        { "0100", "Alap PID adatok" },
        { "0105", "A levegő hőmérséklete" },
        { "010C", "Motor fordulatszám (RPM)" },
        { "010F", "Hűtőfolyadék hőmérséklete" },
        { "0111", "Üzemanyagnyomás" },
        { "012F", "Üzemanyag-ellátás státusza" },
        { "03"  , "Hibakód olvasás" },

    };

    /// <summary>
    /// Visszaadja az elérhető parancsok kódját egy HashSet-ben.
    /// </summary>
    /// <returns>Elérhető parancsok.</returns>
    public static HashSet<string> GetDTCCodes()
    {
        return dtcCodes.Select(entry => entry.Key).ToHashSet();
    }

    /// <summary>
    /// Visszaadja az elérhető parancsok kódját és nevét egy HashSet-ben.
    /// </summary>
    /// <returns>Elérhető parancsok.</returns>
    private static HashSet<string> GetDTCCodesWithNames()
    {
        return dtcCodes.Select(entry => $"| {entry.Key} | [{entry.Value}]").ToHashSet(); 
    }

    /// <summary>
    /// Kilistázza a felhasználó számára az elérhető parancsokat.
    /// </summary>
    public static void PrintUsableDTCCodes()
    {
        var dtcCodeWithNames = GetDTCCodesWithNames();

        Console.WriteLine("");
        Console.WriteLine("Válasszon az alábbi kódok közül:");

        foreach (var item in dtcCodeWithNames)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("");
    }

}