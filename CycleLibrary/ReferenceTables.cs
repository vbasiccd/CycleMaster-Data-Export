using System;
using System.Collections.Generic;
using System.Text;

namespace CycleLibrary
{
    public static class ReferenceTables
    {
        public static Dictionary<int, string> weather = new Dictionary<int, string>()
        {
            { 0,"Sunny" },
            { 1,"Little Cloudy" },
            { 2,"Cloudy" },
            { 3,"Rainy" },
            { 4,"Snowy" },
            { 5,"Stormy" }
        };
        
    }
}
