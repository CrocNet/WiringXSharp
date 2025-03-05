using System;
using WiringXSharp;

public class Program
{
    public static void Main(string[] args)
    {
        // Check if both platform name and GPIO pin number are provided
        if (args.Length < 2)
        {
            DisplaySupportedPlatforms();
            Console.WriteLine("Please provide both the platform name and the GPIO pin number.");
            return;
        }

        // Get platform name from first argument
        string platformName = args[0];

        // Validate platform name
        var supportedPlatforms = WiringXSharp.WiringX.GetSupportedPlatforms();
        if (!Array.Exists(supportedPlatforms, platform => platform == platformName))
        {
            Console.WriteLine($"Platform '{platformName}' is not supported.");
            DisplaySupportedPlatforms();
            return;
        }

        // Parse GPIO pin number from second argument
        if (!int.TryParse(args[1], out int gpio))
        {
            Console.WriteLine("Invalid pin number. Please provide a valid integer.");
            return;
        }

        // Initialize WiringXSharp with the provided platform
        WiringXSharp.WiringX.Setup(platformName);

        try
        {
            // Create a GPIO pin object for the specified pin
            var pin = new GPIOPin(gpio);
            pin.SetMode(PinMode.Output);

            // Infinite loop to toggle the pin
            while (true)
            {
                Console.WriteLine($"GPIO (wiringX) {gpio}: High");
                pin.Write(DigitalValue.High);
                System.Threading.Thread.Sleep(1000); // 1 second delay

                Console.WriteLine($"GPIO (wiringX) {gpio}: Low");
                pin.Write(DigitalValue.Low);
                System.Threading.Thread.Sleep(1000); // 1 second delay
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to display supported platforms
    private static void DisplaySupportedPlatforms()
    {
        Console.WriteLine("Supported platforms:");
        var supportedPlatforms = WiringXSharp.WiringX.GetSupportedPlatforms();
        foreach (var platform in supportedPlatforms)
        {
            Console.WriteLine(platform);
        }
    }
}