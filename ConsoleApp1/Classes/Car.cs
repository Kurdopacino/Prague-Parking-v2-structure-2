using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




public class Car : Vehicle
{
    public override string VehicleType => "BIL";

    public Car(string registrationNumber) : base(registrationNumber) { }
}






