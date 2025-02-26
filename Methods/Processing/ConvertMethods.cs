using System;

/// <summary>
/// Konvertálással kapcsolatos metódusok osztálya.
/// </summary>
public static class ConvertMethods
{
    /// <summary>
    /// Hibakód dekódolás.
    /// </summary>
    /// <param name="highByte">Magas bájt.</param>
    /// <param name="lowByte">Alacsony bájt.</param>
    /// <returns>Hibakód.</returns>
    public static string DecodeDTC(string highByte, string lowByte)
    {
        int highByteValue = highByte.ConvertHexVal();
        int lowByteValue = lowByte.ConvertHexVal();

        string category = "";
        int categoryBits = highByteValue & 0xC0;

        switch (categoryBits)
        {
            case 0x00:
                category = "P";
                break;
            case 0x40:
                category = "C";
                break;
            case 0x80:
                category = "B";
                break;
            case 0xC0:
                category = "U";
                break;
            default:
                category = "?";
                break;
        }

        string dtcCode = $"{category}{highByteValue & 0x3F:X2}{lowByteValue:X2}";

        return dtcCode;
    }

    /// <summary>
    /// Konvertálja a Hex karakter értékét integerré.
    /// </summary>
    /// <param name="hex">Hex karakter.</param>
    /// <returns>Konvertált érték.</returns>
    private static int ConvertHexVal(this char hex) => hex - (hex < 58 ? 48 : (hex < 97 ? 55 : 87));

    /// <summary>
    /// Konvertálja a Hex szöveg értékét integerré.
    /// </summary>
    /// <param name="hex">Hex szöveg.</param>
    /// <returns>Konvertált érték.</returns>
    private static int ConvertHexVal(this string hex)
    {
        if ((hex.Length % 2) == 1)
            throw new ArgumentException("The binary key cannot have an odd number of digits");

        return hex.Aggregate(0, (current, c) => (current << 4) + (ConvertHexVal(c)));
    }
}