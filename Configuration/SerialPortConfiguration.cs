using System.IO.Ports;
using System.Text;

/// <summary>
/// A COM port konfigurációjával kapcsolatos port beállítások.
/// </summary>
class SerialPortConfiguration
{
    protected static readonly int baudRate = 9600;
    protected static readonly string portName = "COM4";

    /// <summary>
    /// Testreszabott port objektum.
    /// </summary>
    protected static readonly SerialPort serialPort = new(portName, baudRate)
    {
        ReadTimeout = 5000,
        WriteTimeout = 1,
        Encoding = Encoding.ASCII
    };
}