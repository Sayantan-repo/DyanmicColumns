using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppDemo.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string Gender { get; set; }

        public string BloodGroup { get; set; }
    }
}