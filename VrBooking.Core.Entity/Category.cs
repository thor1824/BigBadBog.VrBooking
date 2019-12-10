using System.Collections.Generic;

namespace VrBooking.Core.Entity
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public List<Product> Products { get; set; }
    }
}
