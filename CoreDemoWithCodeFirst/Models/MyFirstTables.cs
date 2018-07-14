using System;
using System.Collections.Generic;

namespace CoreDemoWithCodeFirst.Models
{
    public class MyFirstTables
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Profile { get; set; }
        public string Country { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
