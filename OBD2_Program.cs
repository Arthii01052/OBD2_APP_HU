using System;

class OBD2_Program : SerialPortConfiguration
{
    /// <summary>
    /// OBD2 program.
    /// </summary>
    static void Main()
    {
        try
        {
            Console.WriteLine("OBD2 kiolvasó program indul.");
            string odb2Code = PromptMethods.PromptForOBD2Code();

            Console.WriteLine("");
            Console.WriteLine($"Kód kiválasztva: [{odb2Code}].");
            Console.WriteLine("");

            serialPort.Open();
            Console.WriteLine("Sikeres kapcsolat az OBD2 adapterrel!");
            Console.WriteLine("");

            string response = OBD2Communication.SendOBD2Command(odb2Code, serialPort);

            InternalCommandHandlers.ProcessOBD2Command(odb2Code, response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hiba történt: {ex.Message}");
        }
        finally
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                Console.WriteLine("");
                Console.WriteLine("Kapcsolat bontva az OBD2 adapterrel.");
            }
        }
    }
}