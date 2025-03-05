# .NET C# wiringX Wrapper.

wiringX is a C library, modular approach to several GPIO interfaces.  
https://github.com/wiringX/wiringX

* **WiringXSharp** is a DLL wrapper for your DotNet projects.
* **WiringXDemo** is a command line demo, which flip-flops the output if a given GPIO pin.

  Suitable for AOT compilation.  Dockerfile included for arm64 compilation.

#### Tested
| Hardware | GPIO | I2C | PWM | SerialPort | SPI |  
|----------|------|-----|-----|------------|-----|  
| DuoS     | ✓    |     |     |            |     |  
| Duo256   |      |     |     |            |     |

#### Example
```csharp
    using WiringXSharp;
    
    WiringX.Setup("milkv_duos");
    var pin = new GPIOPin(0);
    pin.SetMode(PinMode.Output);
    
    while (true)  
    {
       pin.Write(DigitalValue.High);
       Thread.Sleep(1000);
       pin.Write(DigitalValue.Low);
       Thread.Sleep(1000);
    }
