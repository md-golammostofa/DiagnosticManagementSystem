using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class vmDoctor
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string  MobileNumber { get; set; }
        public string LicenseNo { get; set; }
        public string SpouseName { get; set; }
        public string ParmanentAddress { get; set; }
        public string ImageSrc { get; set; }
    }
}