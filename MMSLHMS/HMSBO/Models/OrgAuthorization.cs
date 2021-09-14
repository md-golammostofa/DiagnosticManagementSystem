using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class OrgAuthorization
    {
        [Key]
        public int Id { get; set; }
        public int OrgId { get; set; }
        public int SubmenuId { get; set; }
        public int MainmenuId { get; set; }
        public int ModuleId { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
    }
}
