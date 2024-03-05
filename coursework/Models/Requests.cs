namespace coursework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Requests
    {
        [Key]
        public int RequestID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OpenDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? ClientID { get; set; }

        public int? ServiceID { get; set; }

        public int? EmployeeID { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Services Services { get; set; }
    }
}
