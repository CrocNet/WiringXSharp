using System.Runtime.InteropServices;
using WiringXSharp;

namespace WiringXSharp;

public static class PInvoke
{
    const string libraryName = "libwiringx.so";

#region Base

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSetup(string platform, IntPtr reserved);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void wiringXGC();

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXValidGPIO(int pin);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wiringXPlatform();

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSelectableFd(int fd);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSupportedPlatforms(ref IntPtr platforms);

#endregion

#region GPIO Functions
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int pinMode(int pin, int mode);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void digitalWrite(int pin, int value);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int digitalRead(int pin);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXISR(int pin, int mode, InterruptHandler func);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void delayMicroseconds(uint us);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl),Obsolete]
        public static extern int waitForInterrupt(int pin, int timeout);

#endregion

#region I2C Functions
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CSetup(string device, int baudrate);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CRead(int fd);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CWrite(int fd, int data);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CWriteReg8(int fd, int reg, int data);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CReadReg8(int fd, int reg);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXI2CWriteReg16(int fd, int reg, int data);
#endregion

#region SPI Functions
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSPISetup(int channel, int speed);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSPIDataRW(int channel, byte[] data, int len);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSPIGetFd(int channel);
#endregion

#region Serial Functions
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSerialOpen(string device, ref SerialOptions options);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void wiringXSerialClose(int fd);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void wiringXSerialPutChar(int fd, byte c);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSerialGetChar(int fd);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXSerialDataAvail(int fd);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void wiringXSerialFlush(int fd);
 #endregion

 #region PWM
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXPWMSetPeriod(int channel, long period);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXPWMSetDuty(int channel, long duty);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXPWMSetPolarity(int channel, int polarity);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int wiringXPWMEnable(int channel, int enable);
 #endregion




}