using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys.Order_Aggraget
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string fName, string lName, string street, string city, string cuntry)
        {
            FName = fName;
            LName = lName;
            Street = street;
            City = city;
            Cuntry = cuntry;
        }

        public string FName { get; set; }

        public string LName { get; set; }

        public  string Street { get; set; }
        
        public string City { get; set; }

        public  string Cuntry { get; set; }

    }
}
