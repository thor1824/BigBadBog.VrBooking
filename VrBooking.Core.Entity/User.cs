using System;
using System.Collections.Generic;
using System.Text;

namespace VrBooking.Core.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SchoolMail { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }
}
