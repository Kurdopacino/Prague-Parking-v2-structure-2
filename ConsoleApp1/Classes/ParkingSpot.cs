using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ParkingSpot
{
    public int Id { get; private set; }
    public Vehicle Vehicle { get; private set; }
    public string V { get; }

    public ParkingSpot(int id)
    {
        Id = id;
        Vehicle = null; // Ingen bil parkerad initialt
    }

    public ParkingSpot(string v)
    {
        V = v;
    }

    public bool IsAvailable()
    {
        return Vehicle == null; // Om Vehicle är null är platsen tillgänglig
    }

    public void ParkVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle; // Spara fordonet i parkeringsplatsen
    }

    public override string ToString()
    {
        if (Vehicle != null)
        {
            return $"[{Vehicle.VehicleType} {Vehicle.RegistrationNumber}]"; // Upptagen plats
        }
        return "[ ]"; // Ledig plats
    }

    internal Vehicle RetrieveVehicle(string? registration)
    {
        throw new NotImplementedException();
    }
}





