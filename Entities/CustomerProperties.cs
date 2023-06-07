using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodaja_kruha_backend.Entities
{
    public class CustomerProperties
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Property { get; set; }
    }
}