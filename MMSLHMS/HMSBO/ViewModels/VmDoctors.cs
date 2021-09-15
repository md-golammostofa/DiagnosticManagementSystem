using HMSBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HMSBO.ViewModels
{
   public class VmDoctors
    {
        public long DocId { get; set; }
        public string DocName { get; set; }
        public string DocDegree { get; set; }
        public string DocRegNo { get; set; }
        public string DocAddress { get; set; }
        public string DocType { get; set; }
        public string DocSpecialist { get; set; }
        public string DocMobile { get; set; }
        public string DocEmail { get; set; }
        public string HospitalName { get; set; }
        public string Remarks { get; set; }
        public double? GenerelFee { get; set; }
        public double? ReportFee { get; set; }
        public double? FollowUpFee { get; set; }
        public double CounselingFee { get; set; }
        public string OtherProfession { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public string DocImage { get; set; }
        public string DocSign { get; set; }
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public List<DoctorDetails> doctorDetails { get; set; }
        //
        public HttpPostedFileBase DoctorImage { get; set; }
    }
}
