namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblStudent")]
    public partial class tblStudent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblStudent()
        {
            tblStudentCourses = new HashSet<tblStudentCourse>();
            tblStudentMarks = new HashSet<tblStudentMark>();
        }

        [Key]
        public long TID { get; set; }

        [Required]
        [StringLength(50)]
        public string studentNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(15)]
        public string cellphone { get; set; }

        public int gender { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        public int stateid { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public long? courseid { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        public int createdby { get; set; }

        public int updatedby { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        public virtual tblAdministrator tblAdministrator1 { get; set; }

        public virtual tblCourse tblCourse { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStudentCourse> tblStudentCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStudentMark> tblStudentMarks { get; set; }
    }
}
