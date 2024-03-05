namespace coursework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            Requests = new HashSet<Requests>();
        }

        [Key]
        public int ClientID { get; set; }

        [StringLength(40)]
        public string LastName { get; set; }

        [StringLength(40)]
        public string FirstName { get; set; }

        [StringLength(40)]
        public string Patronymic { get; set; }

        [StringLength(100)]
        public string ContactInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requests> Requests { get; set; }
    }
}
