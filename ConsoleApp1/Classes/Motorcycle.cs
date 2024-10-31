using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Motorcycle : Vehicle
{
    public override string VehicleType => "MC";

    public Motorcycle(string registrationNumber) : base(registrationNumber) { }
}





