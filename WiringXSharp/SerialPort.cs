using WiringXSharp;

namespace WiringXSharp;


    /// <summary>
    /// Represents a serial port and provides methods for communication.
    /// Implements IDisposable to ensure proper cleanup.
    /// </summary>
    public class SerialPort : IDisposable
    {
        private int fd;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the WiringXSerialPort class for the specified device and baud rate.
        /// </summary>
        /// <param name="device">The serial device path (e.g., "/dev/ttyS0").</param>
        /// <param name="options">The baud rate etc.</param>
        /// <exception cref="Exception">Thrown if opening the port fails.</exception>
        public SerialPort(string device, SerialOptions options)
        {
            fd = PInvoke.wiringXSerialOpen(device, ref options);
            if (fd < 0)
                throw new Exception($"Failed to open serial port {device}");
        }

        /// <summary>
        /// Writes a byte to the serial port.
        /// </summary>
        /// <param name="c">The byte to write.</param>
        public void WriteByte(byte c)
        {
            CheckDisposed();
            PInvoke.wiringXSerialPutChar(fd, c);
        }

        /// <summary>
        /// Reads a byte from the serial port.
        /// </summary>
        /// <returns>The byte read.</returns>
        /// <exception cref="Exception">Thrown if reading fails.</exception>
        public byte ReadByte()
        {
            CheckDisposed();
            int val = PInvoke.wiringXSerialGetChar(fd);
            if (val < 0)
                throw new Exception("Failed to read from serial port");
            return (byte)val;
        }

        /// <summary>
        /// Gets the number of bytes available to read from the serial port.
        /// </summary>
        /// <returns>The number of bytes available.</returns>
        public int DataAvailable()
        {
            CheckDisposed();
            return PInvoke.wiringXSerialDataAvail(fd);
        }

        /// <summary>
        /// Flushes the serial port buffers.
        /// </summary>
        public void Flush()
        {
            CheckDisposed();
            PInvoke.wiringXSerialFlush(fd);
        }

        /// <summary>
        /// Disposes the serial port, closing it if open.
        /// </summary>
        public void Dispose()
        {
            if (!disposed && fd >= 0)
            {
                PInvoke.wiringXSerialClose(fd);
                fd = -1;
                disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(SerialPort));
        }
    }
