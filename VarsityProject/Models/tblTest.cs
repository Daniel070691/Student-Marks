namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblTest")]
    public partial class tblTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTest()
        {
            tblSubjectTests = new HashSet<tblSubjectTest>();
        }

        [Key]
        public long TID { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        public DateTime scheduleDate { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int createdby { get; set; }

        public int stateid { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSubjectTest> tblSubjectTests { get; set; }
    }
}
