using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Bicycle : Vehicle
{
    public override string VehicleType => "CYKEL";

    public Bicycle(string registrationNumber) : base(registrationNumber) { }
}



