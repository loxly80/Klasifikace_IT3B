using System;
using System.Collections.Generic;
using System.Text;

namespace Klasifikace_IT3B
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Teacher Teacher { get; set; }
    }
}
