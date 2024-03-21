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

        public int? EmployeeID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public DateTime? LoginTime { get; set; }

        public virtual Employees Employees { get; set; }
    }
}
