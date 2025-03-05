using WiringXSharp;

namespace WiringXSharp;

    /// <summary>
    /// Represents a GPIO pin and provides methods for controlling it.
    /// </summary>
    public class GPIOPin
    {
        public int PinNumber { get; private set; }
        private InterruptHandler? _handler;

        /// <summary>
        /// Initializes a new instance of the WiringXPin class for the specified pin.
        /// </summary>
        /// <param name="pin">The pin number.</param>
        /// <exception cref="ArgumentException">Thrown if the pin is invalid for the platform.</exception>
        public GPIOPin(int pin)
        {
            if (PInvoke.wiringXValidGPIO(pin) != 0)
                throw new ArgumentException($"Invalid GPIO pin: {pin}");
            PinNumber = pin;
        }

        /// <summary>
        /// Sets the mode of the pin (input or output).
        /// </summary>
        /// <param name="mode">The pin mode.</param>
        public void SetMode(PinMode mode)
        {
            if (PInvoke.pinMode(PinNumber, (int)mode)!= 0)
                throw new ArgumentException($"Invalid pin mode: {PinNumber} : {mode}");
        }

        /// <summary>
        /// Writes a digital value to the pin.
        /// </summary>
        /// <param name="value">The value to write (Low or High).</param>
        public void Write(DigitalValue value)
        {
            PInvoke.digitalWrite(PinNumber, (int)value);
        }

        /// <summary>
        /// Reads the digital value from the pin.
        /// </summary>
        /// <returns>The current value (Low or High).</returns>
        public DigitalValue Read()
        {
            return (DigitalValue)PInvoke.digitalRead(PinNumber);
        }


        /// <summary>
        /// Configures an interrupt handler for the pin.
        /// </summary>
        /// <param name="mode">The interrupt trigger mode.</param>
        /// <param name="handlerAction">The action to execute when the interrupt occurs.</param>
        /// <exception cref="Exception">Thrown if setting the interrupt fails.</exception>
        public void SetInterrupt(InterruptMode mode, Action handlerAction)
        {
            _handler = () => handlerAction();
            int result = PInvoke.wiringXISR(PinNumber, (int)mode, _handler);
            if (result < 0)
                throw new Exception($"Failed to set interrupt on pin {PinNumber}");
        }
    }
