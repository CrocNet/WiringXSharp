using System.Runtime.InteropServices;
using WiringXSharp;

namespace WiringXSharp;

public class PWM
{

    public static void SetPWMPeriod(int channel, long period)
    {
        PInvoke.wiringXPWMSetPeriod(channel, period);
    }

    public static void SetPWMDuty(int channel, long duty)
    {
        PInvoke.wiringXPWMSetDuty(channel, duty);
    }

    public static void SetPWMPolarity(int channel, Polarity polarity)
    {
        PInvoke.wiringXPWMSetPolarity(channel, (int)polarity);
    }

    public static void EnablePWM(int channel, bool enable)
    {
        PInvoke.wiringXPWMEnable(channel, enable ? 1 : 0);
    }
}