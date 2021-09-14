using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class ActionLog
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string ActionName { get; set; }
        [StringLength(200)]
        public string TableName { get; set; }
        [StringLength(50)]
        public string PrimaryKeyId { get; set; }
        [StringLength(50)]
        public string SubKey { get; set; }
        public string MacID { get; set; }
        public int? OrgId { get; set; }
        [StringLength(50)]
        public string UserId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string DataValues { get; set; }
        public Nullable<DateTime> DeleteTime { get; set; }
    }
}
