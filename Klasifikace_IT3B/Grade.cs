using System;
using System.Collections.Generic;
using System.Text;

namespace Klasifikace_IT3B
{
    public class Grade
    {
        public int Id { get; set; }
        public int GradeNumber { get; set; }
        public Subject Subject { get; set; }
        public double Weight { get; set; }
        public string Comment { get; set; }
    }
}
