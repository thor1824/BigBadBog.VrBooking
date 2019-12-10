using System.Collections.Generic;

namespace VrBooking.Core.Entity
{
    public class FilterPageProductList
    {
        public List<Product> Products { get; set; }

        public Category Filter { get; set; }

        public int ItemsTotal { get; set; }

        public int ItemsPrPage { get; set; }

        public int PageIndex { get; set; }

        public int PageTotal { get; set; }
    }
}
