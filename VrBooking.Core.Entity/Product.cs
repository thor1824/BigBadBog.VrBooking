using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category category { get; set; }

    }
}
