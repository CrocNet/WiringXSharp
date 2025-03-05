using WiringXSharp;

namespace WiringXSharp;

/// <summary>
/// Represents an I2C device and provides methods for communication.
/// </summary>
public class I2CDevice
{
    private readonly int fd;

    /// <summary>
    /// Initializes a new instance of the WiringXI2CDevice class for the specified device ID.
    /// </summary>
    public I2CDevice(string devId, int baud)
    {
        fd = PInvoke.wiringXI2CSetup(devId,baud);
        if (fd < 0)
            throw new Exception($"Failed to setup I2C device with ID {devId}");
    }

    /// <summary>
    /// Reads a byte from the I2C device.
    /// </summary>
    /// <returns>The byte read.</returns>
    public int Read()
    {
        return PInvoke.wiringXI2CRead(fd);
    }

    /// <summary>
    /// Writes a byte to the I2C device.
    /// </summary>
    /// <param name="data">The byte to write.</param>
    public void Write(int data)
    {
        PInvoke.wiringXI2CWrite(fd, data);
    }

    /// <summary>
    /// Writes a byte to a specific register on the I2C device.
    /// </summary>
    /// <param name="reg">The register address.</param>
    /// <param name="data">The byte to write.</param>
    public void WriteReg8(int reg, int data)
    {
        PInvoke.wiringXI2CWriteReg8(fd, reg, data);
    }

    /// <summary>
    /// Reads a byte from a specific register on the I2C device.
    /// </summary>
    /// <param name="reg">The register address.</param>
    /// <returns>The byte read.</returns>
    public int ReadReg8(int reg)
    {
        return PInvoke.wiringXI2CReadReg8(fd, reg);
    }
}


