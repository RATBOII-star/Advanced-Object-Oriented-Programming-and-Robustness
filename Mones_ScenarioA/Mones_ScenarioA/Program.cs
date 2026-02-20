using System;

namespace Mones_ScenarioA
{
    // Base Class
    public class Vehicle
    {
        // Encapsulation: private fields
        private string vehicleID;
        private string modelName;

        // Public properties with validation
        public string VehicleID
        {
            get { return vehicleID; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("VehicleID cannot be empty.");
                vehicleID = value;
            }
        }

        public string ModelName
        {
            get { return modelName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ModelName cannot be empty.");
                modelName = value;
            }
        }

        // Constructor
        public Vehicle(string id, string model)
        {
            VehicleID = id;
            ModelName = model;
        }

        // Polymorphism: virtual method
        public virtual double CalculateRange()
        {
            return 0; // Base class doesn't know range
        }
    }

    // Subclass: ElectricBus
    public class ElectricBus : Vehicle
    {
        private double batteryPercent;

        public double BatteryPercent
        {
            get { return batteryPercent; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Battery % must be 0-100.");
                batteryPercent = value;
            }
        }

        public ElectricBus(string id, string model, double battery) : base(id, model)
        {
            BatteryPercent = battery;
        }

        // Polymorphism: override
        public override double CalculateRange()
        {
            if (BatteryPercent < 5)
                throw new InvalidOperationException($"ElectricBus {VehicleID}: Battery too low for range calculation!");

            // Simple logic: each percent gives 2 km range
            return BatteryPercent * 2;
        }
    }

    // Subclass: GasPoweredVan
    public class GasPoweredVan : Vehicle
    {
        private double fuelLevel;

        public double FuelLevel
        {
            get { return fuelLevel; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Fuel level must be 0-100.");
                fuelLevel = value;
            }
        }

        public GasPoweredVan(string id, string model, double fuel) : base(id, model)
        {
            FuelLevel = fuel;
        }

        // Polymorphism: override
        public override double CalculateRange()
        {
            if (FuelLevel < 5)
                throw new InvalidOperationException($"GasPoweredVan {VehicleID}: Fuel too low for range calculation!");

            // Simple logic: each percent gives 3 km range
            return FuelLevel * 3;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create vehicle objects
                ElectricBus bus1 = new ElectricBus("EB001", "Tesla eBus", 80);
                GasPoweredVan van1 = new GasPoweredVan("GV001", "Ford Transit", 60);

                // Display ranges
                Console.WriteLine($"{bus1.ModelName} Range: {bus1.CalculateRange()} km");
                Console.WriteLine($"{van1.ModelName} Range: {van1.CalculateRange()} km");

                // Example of exception: battery/fuel too low
                ElectricBus bus2 = new ElectricBus("EB002", "Mini eBus", 3);
                Console.WriteLine($"{bus2.ModelName} Range: {bus2.CalculateRange()} km");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("System Shutdown");
            }
        }
    }
}