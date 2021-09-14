using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Module
    {
        [Key]
        public int MId { get; set; }
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public string IconColor { get; set; }
        public string EntryUser { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}",ApplyFormatInEditMode =true)]
        public Nullable<DateTime> EntryDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}