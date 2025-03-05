using WiringXSharp;

namespace WiringXSharp;


/// <summary>
/// Represents an SPI device and provides methods for communication.
/// </summary>
public class SPIDevice
{
    private readonly int channel;

    /// <summary>
    /// Initializes a new instance of the WiringXSPIDevice class for the specified channel and speed.
    /// </summary>
    /// <param name="channel">The SPI channel.</param>
    /// <param name="speed">The communication speed in Hz.</param>
    /// <exception cref="Exception">Thrown if setup fails.</exception>
    public SPIDevice(int channel, int speed)
    {
        this.channel = channel;
        if (PInvoke.wiringXSPISetup(channel, speed) < 0)
            throw new Exception($"Failed to setup SPI device on channel {channel}");
    }

    /// <summary>
    /// Performs a read/write operation on the SPI device.
    /// </summary>
    /// <param name="data">The data buffer to send and receive.</param>
    /// <returns>The number of bytes transferred.</returns>
    public int DataRW(byte[] data)
    {
        return PInvoke.wiringXSPIDataRW(channel, data, data.Length);
    }
}
