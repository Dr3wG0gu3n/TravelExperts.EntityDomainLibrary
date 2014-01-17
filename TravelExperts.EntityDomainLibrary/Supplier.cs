using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.EntityDomainLibrary
{
    public class Supplier
    {
        //automatic properties
        public int Id { get; set; }
        public string Name { get; set; }

        //constructors
        public Supplier() { }
        public Supplier(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Supplier(string name)
        {
            Name = name;
        }
    }
}
