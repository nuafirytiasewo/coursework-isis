namespace coursework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoginLogs
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LoginTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
