using System;
using System.IO.Ports;

/// <summary>
/// Az OBD2 kommunikációját megvalósító osztály.
/// </summary>
public static class OBD2Communication
{
    /// <summary>
    /// OBD2 parancs küldése a COM portra.
    /// </summary>
    /// <param name="command">OBD2 parancs.</param>
    /// <param name="serialPort">COM port.</param>
    /// <returns>Válasz.</returns>
    /// <exception cref="ArgumentNullException">A megadott COM port nem létezik vagy nem nyitott a kapcsolata.</exception>
    /// <exception cref="TimeoutException">A COM port időtullépést kapott.</exception>
    public static string SendOBD2Command(string command, SerialPort serialPort)
    {
        if (serialPort == null || !serialPort.IsOpen)
            throw new ArgumentNullException("A megadott COM port nem létezik vagy nem nyitott a kapcsolata!");
        
        try
        {
            string fullCommand = command + "\r\n";
            Console.WriteLine($"[DEV] Küldés a soros portra:  {command}");
            serialPort.Write(fullCommand);

            string response = serialPort.ReadLine()?.Trim();

            return response ?? "NO RESPONSE";
        }
        catch (TimeoutException)
        {
            throw new TimeoutException("Az OBD2 nem válaszol.");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
