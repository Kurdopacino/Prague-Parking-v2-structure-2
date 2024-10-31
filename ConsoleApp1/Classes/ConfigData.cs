using System;
using System.Collections.Generic;

public class ConfigData
{
    public int TotalParkingSpaces { get; set; }
    public int FreeMinutes { get; set; }
    public Dictionary<string, decimal> Prices { get; set; }

    public ConfigData()
    {
        // Standardvärden
        TotalParkingSpaces = 100; // Antal parkeringsplatser
        FreeMinutes = 10; // Antal gratis minuter

        Prices = new Dictionary<string, decimal>
        {
            { "BIL", 20.0m },   // Timpris för bil
            { "MC", 10.0m },    // Timpris för motorcykel
            { "BUSS", 50.0m },  // Timpris för buss
            { "CYKEL", 5.0m }    // Timpris för cykel
        };
    }
}
