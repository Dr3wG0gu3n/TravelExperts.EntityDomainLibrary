using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.EntityDomainLibrary;

/*****************************************************
 * Author: Chris & Yu Wen
 * Date: Jan 16, 2014
 * Task: Workshop 2-CMPP248
 * 
 * Class Product
 * Constructor and properties
 * ************************************************/

namespace TravelExperts
{
    public class Product
    {
        public Product() { }        //constructor

        public int ProductId { get; set; }
        public string ProdName { get; set; }
    }
}
