using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Bus : Vehicle
{
    public override string VehicleType => "BUSS";

    public Bus(string registrationNumber) : base(registrationNumber) { }
}




