namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSubjectTest")]
    public partial class tblSubjectTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSubjectTest()
        {
            tblStudentMarks = new HashSet<tblStudentMark>();
        }

        [Key]
        public long TID { get; set; }

        public long subjectid { get; set; }

        public long testid { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int stateid { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStudentMark> tblStudentMarks { get; set; }

        public virtual tblSubject tblSubject { get; set; }

        public virtual tblTest tblTest { get; set; }
    }
}
