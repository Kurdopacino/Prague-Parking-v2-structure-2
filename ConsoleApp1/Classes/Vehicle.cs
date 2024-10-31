using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




public abstract class Vehicle
{
    public string RegistrationNumber { get; private set; }
    public abstract string VehicleType { get; }

    protected Vehicle(string registrationNumber)
    {
        RegistrationNumber = registrationNumber;
    }

    public decimal CalculateCost(int freeMinutes, decimal pricePerHour)
    {
        // Placeholder för kostnadsberäkning, här kan du lägga till logik för att beräkna parkeringstid och kostnad
        return pricePerHour; // Returnerar timpris för nu
    }
}






