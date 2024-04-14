using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yard.domain.Models
{
    public class Address : BaseEntity
    {
        //public Address()
        //{
        //    Country = "Nigeria";
        //}
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

    }
}

//Fluent API - OnModelCreating;
//Data Annotation
//Poco