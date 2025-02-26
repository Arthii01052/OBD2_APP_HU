using System;

/// <summary>
/// OBD2_Readerrel kapcsolatos belsős parancsok osztálya.
/// </summary>
public static class InternalCommandHandlers
{
    /// <summary>
    /// Az OBD2 parancs feldolgozása.
    /// </summary>
    /// <param name="odb2Code">OBD2 kód.</param>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <exception cref="ArgumentNullException">Hiányzó OBD2 kód.</exception>
    /// <exception cref="Exception">Hiányzó válasz a COM portról.</exception>
    public static void ProcessOBD2Command(string odb2Code, string response)
    {
        if (string.IsNullOrWhiteSpace(odb2Code))
            throw new ArgumentNullException("[HIBA] Nem lett OBD2 kód megadva!");

        if (string.IsNullOrEmpty(response))
            throw new Exception("[HIBA] Nincs válasz az OBD2 rendszertől.");            

        Console.WriteLine($"[DEV] Válasz a soros portról: {response}");

        switch (response.Substring(0, 2))
        {
            case "41":
                HandlePIDCommands(odb2Code, response);
                break;
            case "43":
                HandleErrorCodeReadCommand(response);
                break;
            default:
                Console.WriteLine("[HIBA] Nem megfelelő válaszformátum.");
                break;
        }
    }

    /// <summary>
    /// Hibakódokat kezelő metódus.
    /// </summary>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    public static void HandleErrorCodeReadCommand(string response)
    {
        string[] dtcCodes = ParseMethods.ParseDTCResponse(response);
        Console.WriteLine("");
        Console.WriteLine("Hibakódok:");
        foreach (var code in dtcCodes)
        {
            Console.WriteLine(code);
        }
    }

    /// <summary>
    /// Egyéb PID parancsokat kezelő metódus.
    /// </summary>
    /// <param name="odb2Code">OBD2 kód.</param>
    /// <param name="response">Visszakapott válasz a COM portról.</param>
    /// <exception cref="Exception">Hibás OBD2 parancs.</exception>
    public static void HandlePIDCommands(string odb2Code, string response)
    {
        switch (odb2Code)
        {
            case "0100":
                {
                    string pidData = ParseMethods.ParsePIDResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"PID válasz: [{pidData}].");
                    break;
                }

            case "010C":
                {
                    string rpmData = ParseMethods.ParseRPMResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"Motor fordulatszám: [{rpmData} RPM].");
                    break;
                }

            case "010F":
                {
                    string coolantTemp = ParseMethods.ParseCoolantTempResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"Hűtőfolyadék hőmérséklete: [{coolantTemp} C°].");
                    break;
                }

            case "0111":
                {
                    string fuelPressure = ParseMethods.ParseFuelPressureResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"Üzemanyagnyomás: [{fuelPressure} PSI].");
                    break;
                }

            case "0105":
                {
                    string airTemp = ParseMethods.ParseAirTempResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"Levegő hőmérséklete: [{airTemp} C°].");
                    break;
                }

            case "012F":
                {
                    string fuelStatus = ParseMethods.ParseFuelStatusResponse(response);
                    Console.WriteLine("");
                    Console.WriteLine($"Üzemanyag-ellátás státusza: [{fuelStatus}].");
                    break;
                }

            default:
                Console.WriteLine("");
                throw new Exception("Hiba az OBD2 kód megadásánál!");
        }
    }
}
