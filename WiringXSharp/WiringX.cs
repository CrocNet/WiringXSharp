using System;
using System.Runtime.InteropServices;
using WiringXSharp;


namespace WiringXSharp;

    /// <summary>
    /// Defines pin modes for GPIO pins.
    /// </summary>
    public enum PinMode
    {
        Not_Set = 0,
        Input = 2,
        Output = 4,
        Interrupt = 8
    }

    /// <summary>
    /// Defines digital values for GPIO pins.
    /// </summary>
    public enum DigitalValue
    {
        Low = 0,
        High = 1
    }

    public enum Polarity
    {
        Normal = 0,
        Inversed = 1,
    }

    /// <summary>
    /// Defines interrupt trigger modes for GPIO pins.
    /// </summary>
    public enum InterruptMode
    {
        Unknown = 0,
        Rising = 2,
        Falling = 4,
        Both = 8,
        None = 16
    }


    public enum Function {
        Unknown = 0,
        Digital = 2,
        Analog = 4,
        I2C = 16,
        Interrup = 32
    };


    [StructLayout(LayoutKind.Sequential)]
    public struct SerialOptions
    {
        public uint baud;
        public uint databits;
        public uint parity;
        public uint stopbits;
        public uint flowcontrol;
    }


    /// <summary>
    /// Delegate for interrupt handler functions.
    /// </summary>
    public delegate void InterruptHandler();

    
    /// <summary>
    /// Static class for initializing the wiringX library.
    /// </summary>
    public static class WiringX
    {
        /// <summary>
        /// Initializes the wiringX library for the specified platform.
        /// </summary>
        /// <param name="platform">The platform string (e.g., "milkv_duo").</param>
        /// <exception cref="Exception">Thrown if initialization fails.</exception>
        public static void Setup(string platform)
        {
            int result = PInvoke.wiringXSetup(platform, IntPtr.Zero);
            if (result == -1)
            {
                PInvoke.wiringXGC();
                throw new Exception("Failed to initialize wiringX library");
            }
        }

        /// <summary>
        /// Cleans up resources allocated by the wiringX library.
        /// </summary>
        public static void Cleanup()
        {
            PInvoke.wiringXGC();
        }

        public static void DelayMicroseconds(uint microseconds)
        {
            PInvoke.delayMicroseconds(microseconds);
        }

        public static string GetPlatform()
        {
            IntPtr platformPtr = PInvoke.wiringXPlatform();
            return platformPtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(platformPtr) : null;
        }


        public static string[] GetSupportedPlatforms()
        {
            string[] platforms;
            IntPtr platformsPtr = IntPtr.Zero;
            int count = PInvoke.wiringXSupportedPlatforms(ref platformsPtr);

            if (count > 0 && platformsPtr != IntPtr.Zero)
            {
                platforms = new string[count];
                for (int i = 0; i < count; i++)
                {
                    IntPtr strPtr = Marshal.ReadIntPtr(platformsPtr, i * IntPtr.Size);
                    platforms[i] = Marshal.PtrToStringAnsi(strPtr);
                }
            }
            else
            {
                throw new Exception("Failed to read SupportedPlatforms");
            }
            return platforms;
        }

    }
    
