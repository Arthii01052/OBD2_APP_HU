using System;

/// <summary>
/// Felhasznál beavatkozását kezelő osztály.
/// </summary>
public static class PromptMethods
{
    /// <summary>
    /// OBD2 kód bekérése a user-től.
    /// </summary>
    /// <returns>Visszaadja az OBD2 kódot.</returns>
    public static string PromptForOBD2Code()
    {
        OBD2Codes.PrintUsableDTCCodes();

        var odb2Code = Console.ReadLine().ToUpper();

        var obd2Codes = OBD2Codes.GetDTCCodes();

        while (!obd2Codes.Contains(odb2Code))
        {
            Console.WriteLine("Hibás kód, írja be újra!");
            Console.WriteLine("");
            odb2Code = Console.ReadLine().ToUpper();
        }

        return odb2Code;
    }
}

