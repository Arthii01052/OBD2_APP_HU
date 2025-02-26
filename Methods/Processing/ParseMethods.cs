using System;
using System.Linq;

/// <summary>
/// Parse-olással kapcsolatos metódusok osztálya.
/// </summary>
public static class ParseMethods
{
    /// <summary>
    /// Motorhibakód kiolvasással kapcsolatos metódus.
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Visszaadja az OBD2 motorhibakódokat.</returns>
    public static string[] ParseDTCResponse(string response)
    {
        string[] parts = response.Split(' ');

        if (parts.Length < 2)
        {
            return [];
        }

        int dtcCount = Convert.ToInt32(parts[1], 16); // A hibakódok száma
        string[] dtcCodes = new string[dtcCount];

        for (int i = 0; i < dtcCount; i++)
        {
            if (2 + (i * 2) + 1 < parts.Length)
            {
                string highByte = parts[2 + (i * 2)];
                string lowByte = parts[2 + (i * 2) + 1];

                //Console.WriteLine($"highByte: {highByte}, lowByte: {lowByte}"); // Log a hibák pontosabb diagnosztizálásához

                string dtcCode = ConvertMethods.DecodeDTC(highByte, lowByte); // Hibakód dekódolás

                string name = DTCDescriptions.GetDTCDescription(dtcCode);

                if (name != null)
                {
                    dtcCode = $"| {dtcCode} | - [{name}]";
                }
                else
                {
                    dtcCode = $"| {dtcCode} |";
                }

                dtcCodes[i] = dtcCode;
            }
        }

        return dtcCodes;
    }

    /// <summary>
    /// Visszaadja az alap PID adatokat.
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Alap PID adatok.</returns>
    public static string ParsePIDResponse(string response)
    {
        string[] parts = response.Split(' ');
        string pidType = parts[0];

        string pidData = string.Join(" ", parts.Skip(2));

        return pidData;
    }

    /// <summary>
    /// Visszaadja a gépjármű fordulatszámát.
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Fordulatszám.</returns>
    public static string ParseRPMResponse(string response)
    {
        string[] parts = response.Split(' ');

        string rpmHexHigh = parts[2];
        string rpmHexLow = parts[3];

        int rpmValue = Convert.ToInt32(rpmHexHigh + rpmHexLow, 16);

        int rpm = rpmValue / 4;

        return rpm.ToString();
    }

    /// <summary>
    /// Visszaadja a gépjármű hűtőfolyadék hőmérsékletét. 
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Hűtő hőmérséklete.</returns>
    public static string ParseCoolantTempResponse(string response)
    {
        string[] parts = response.Split(' ');
        string coolantTempHex = parts[2];
        int coolantTemp = Convert.ToInt32(coolantTempHex, 16);
        return coolantTemp.ToString();
    }

    /// <summary>
    /// Visszaadja a gépjármű üzemanyag nyomását. 
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Üzemanyag nyomás.</returns>
    public static string ParseFuelPressureResponse(string response)
    {
        string[] parts = response.Split(' ');
        string fuelPressureHex = parts[2];
        int fuelPressure = Convert.ToInt32(fuelPressureHex, 16);
        return fuelPressure.ToString();
    }

    /// <summary>
    /// Visszaadja a gépjármű levegő hőmérsékletét.
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Levegő hőmérséklete.</returns>
    public static string ParseAirTempResponse(string response)
    {
        string[] parts = response.Split(' ');
        string airTempHex = parts[2];
        int airTemp = Convert.ToInt32(airTempHex, 16);
        return airTemp.ToString();
    }

    /// <summary>
    /// Visszaadja a gépjármű üzemanyag üzemmódját (Normál / Hiba). 
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <returns>Üzemmód típusa.</returns>
    public static string ParseFuelStatusResponse(string response)
    {
        string[] parts = response.Split(' ');
        string fuelStatusHex = parts[2];
        int fuelStatus = Convert.ToInt32(fuelStatusHex, 16);
        return fuelStatus == 1 ? "Normál üzemmód" : "Hiba";
    }
}
