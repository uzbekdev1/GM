using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM.DAL.Infrastructure
{
    public abstract class AuditEntity : BaseEntity
    {

        [Column(TypeName = "datetime2")]
        public DateTime Added { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }
          
    }
}