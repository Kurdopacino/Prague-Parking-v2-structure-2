using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Se till att Newtonsoft.Json är installerad

class Program
{
    static string FilePath = "parkingData.json"; // Filväg för parkeringsdata
    static List<ParkingSpot> parkingSpots = new List<ParkingSpot>();
    static int TotalParkingSpaces = 100; // Total antal parkeringsplatser

    static void Main()
    {
        InitializeParkingSpots(TotalParkingSpaces);
        LoadParkingData(); // Ladda tidigare sparad parkeringsdata

        while (true)
        {
            Console.Clear();
            DisplayWelcomeMessage();

            string choice = GetMenuChoice();
            switch (choice)
            {
                case "1":
                    ParkVehicle();
                    break;
                case "2":
                    RetrieveVehicle();
                    break;
                case "3":
                    MoveVehicle();
                    break;
                case "4":
                    SearchVehicle();
                    break;
                case "5":
                    ViewParkingMap();
                    break;
                case "6":
                    SaveParkingData(); // Spara data innan programmet stängs
                    return; // Exit the program
                default:
                    Console.WriteLine("Ogiltigt val, Försök igen.");
                    break;
            }

            SaveParkingData(); // Spara data efter varje åtgärd
            Console.WriteLine("Tryck enter för att fortsätta...");
            Console.ReadLine();
        }
    }

    static void InitializeParkingSpots(int totalSpots)
    {
        for (int i = 0; i < totalSpots; i++)
        {
            parkingSpots.Add(new ParkingSpot($"Spot {i + 1}"));
        }
    }

    static ConfigData LoadConfigData()
    {
        if (File.Exists(ConfigFilePath))
        {
            string jsonString = File.ReadAllText(ConfigFilePath);
            ConfigData loadedConfig = JsonSerializer.Deserialize<ConfigData>(jsonString);

            // Sätt standardvärden för saknade delar av konfigurationsfilen
            if (loadedConfig.Prices == null)
            {
                loadedConfig.Prices = new Dictionary<string, decimal>
                {
                    { "BIL", 20.00m },
                    { "MC", 10.00m },
                    { "BUSS", 50.00m },
                    { "CYKEL", 5.00m }
                };
            }
            if (loadedConfig.TotalParkingSpaces == 0)
            {
                loadedConfig.TotalParkingSpaces = 100;
            }
            if (loadedConfig.FreeMinutes == 0)
            {
                loadedConfig.FreeMinutes = 10;
            }

            return loadedConfig;
        }
        // Standardvärden om filen inte finns
        return new ConfigData
        {
            TotalParkingSpaces = 100,
            FreeMinutes = 10,
            Prices = new Dictionary<string, decimal>
            {
                { "BIL", 20.00m },
                { "MC", 10.00m },
                { "BUSS", 50.00m },
                { "CYKEL", 5.00m }
            }
        };
    }

    static void SaveParkingData()
    {
        string jsonData = JsonConvert.SerializeObject(parkingSpots, Formatting.Indented);
        File.WriteAllText(FilePath, jsonData);
        Console.WriteLine("Parkeringsdata har sparats.");
    }

    static void DisplayWelcomeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
  _____                              _____           _    _                    ___  
 |  __ \                            |  __ \         | |  (_)                  |__ \ 
 | |__) | __ __ _  __ _ _   _  ___  | |__) |_ _ _ __| | ___ _ __   __ _  __   __ ) |
 |  ___/ '__/ _` |/ _` | | | |/ _ \ |  ___/ _` | '__| |/ / | '_ \ / _` | \ \ / // / 
 | |   | | | (_| | (_| | |_| |  __/ | |  | (_| | |  |   <| | | | | (_| |  \ V // /_ 
 |_|   |_|  \__,_|\__, |\__,_|\___| |_|   \__,_|_|  |_|\_\_|_| |_|\__, |   \_/|____|
                   __/ |                                           __/ |            
                  |___/                                           |___/");
        Console.ResetColor();


        // Grundläggande parkering system information
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Välkommen till Prague Parking ");
        Console.WriteLine("Antal parkeringsplatser: " + config.TotalParkingSpaces);
        Console.WriteLine("Timpris för bil: " + config.Prices["BIL"] + " kr");
        Console.WriteLine("Timpris för motorcykel: " + config.Prices["MC"] + " kr");
        Console.WriteLine("Timpris för buss: " + config.Prices["BUSS"] + " kr");
        Console.WriteLine("Timpris för cykel: " + config.Prices["CYKEL"] + " kr");
        Console.WriteLine(config.FreeMinutes + " första minuterna är gratis för alla fordon.");
    }

    static int GetAvailableSpotsCount()
    {
        int count = 0;
        foreach (var spot in parkingSpots)
        {
            if (spot.IsAvailable())
            {
                count++;
            }
        }
        return count;
    }

    static string GetMenuChoice()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("1. Parkera Fordon");
        Console.WriteLine("2. Hämta Fordon");
        Console.WriteLine("3. Flytta Fordon");
        Console.WriteLine("4. Sök Fordon på Reg nr");
        Console.WriteLine("5. Visa aktuell parkerings vy");
        Console.WriteLine("6. Avsluta");
        Console.Write("Välj ett alternativ: ");
        return Console.ReadLine();
    }

    static void ParkVehicle()
    {
        Console.Write("Ange fordonstyp (BIL/MC/BUSS/CYKEL): ");
        string vehicleType = Console.ReadLine().ToUpper();
        Vehicle vehicle = null;

        Console.Write("Ange registreringsnummer: ");
        string registration = Console.ReadLine();

        switch (vehicleType)
        {
            case "BIL":
                vehicle = new Car(registration);
                break;
            case "MC":
                vehicle = new Motorcycle(registration);
                break;
            case "BUSS":
                vehicle = new Bus(registration);
                break;
            case "CYKEL":
                vehicle = new Bicycle(registration);
                break;
            default:
                Console.WriteLine("Fel Fordonstyp.");
                return;
        }

        foreach (var spot in parkingSpots)
        {
            if (spot.IsAvailable())
            {
                spot.ParkVehicle(vehicle);
                Console.WriteLine($"Fordon parkerat på {spot.Id}.");
                return;
            }
        }

        Console.WriteLine("Inga tillgängliga parkeringsplatser.");
    }

    static void RetrieveVehicle()
    {
        Console.Write("Ange reg nr för att hämta fordon: ");
        string registration = Console.ReadLine();

        foreach (var spot in parkingSpots)
        {
            Vehicle vehicle = spot.RetrieveVehicle(registration);
            if (vehicle != null)
            {
                Console.WriteLine($"Hämtar {vehicle.VehicleType} med registreringsnummer {vehicle.RegistrationNumber}.");
                return;
            }
        }

        Console.WriteLine("Fordonet hittades inte.");
    }

    static void MoveVehicle()
    {
        Console.Write("Ange aktuellt nummer på parkeringsplats (1-" + parkingSpots.Count + "): ");
        if (int.TryParse(Console.ReadLine(), out int currentSpotIndex) && currentSpotIndex > 0 && currentSpotIndex <= parkingSpots.Count)
        {
            ParkingSpot currentSpot = parkingSpots[currentSpotIndex - 1];
            if (!currentSpot.IsAvailable())
            {
                Vehicle vehicleToMove = currentSpot.Vehicle;

                Console.Write("Ange nytt nummer på parkeringsplatsen (1-" + parkingSpots.Count + "): ");
                if (int.TryParse(Console.ReadLine(), out int newSpotIndex) && newSpotIndex > 0 && newSpotIndex <= parkingSpots.Count)
                {
                    ParkingSpot newSpot = parkingSpots[newSpotIndex - 1];
                    if (newSpot.IsAvailable())
                    {
                        newSpot.ParkVehicle(vehicleToMove);
                        currentSpot.RetrieveVehicle(vehicleToMove.RegistrationNumber); // Frigör den gamla platsen
                        Console.WriteLine($"Fordon flyttades från {currentSpot.Id} till {newSpot.Id}.");
                    }
                    else
                    {
                        Console.WriteLine("Den nya parkeringsplatsen är redan upptagen.");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val av ny parkeringsplats.");
                }
            }
            else
            {
                Console.WriteLine("Inget fordon på den angivna platsen.");
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt val av nuvarande parkeringsplats.");
        }
    }

    static void SearchVehicle()
    {
        Console.Write("Ange registreringsnummer för att söka: ");
        string registration = Console.ReadLine();

        foreach (var spot in parkingSpots)
        {
            Vehicle vehicle = spot.RetrieveVehicle(registration);
            if (vehicle != null)
            {
                Console.WriteLine($"Fordon med registreringsnummer {vehicle.RegistrationNumber} finns på {spot.Id}.");
                return;
            }
        }

        Console.WriteLine("Fordonet hittades inte.");
    }

    static void ViewParkingMap()
    {
        Console.WriteLine("Parkeringsvy:");
        foreach (var spot in parkingSpots)
        {
            if (spot.IsAvailable())
            {
                Console.WriteLine($"{spot.Id}: Ledig");
            }
            else
            {
                Console.WriteLine($"{spot.Id}: {spot.Vehicle.VehicleType} ({spot.Vehicle.RegistrationNumber})");
            }
        }
    }
}
