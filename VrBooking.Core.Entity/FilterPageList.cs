using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VrBooking.Core.Entity
{
    public class FilterPageList<T>
    {
        public List<T> List { get; set; }
        
        public Category FilterCategory { get; set; }

        public int ItemsTotal { get; set; }
        
        public int ItemsPrPage { get; set; }

        public int PageIndex { get; set; }

        public int PageTotal { get; set; }
    }
}
