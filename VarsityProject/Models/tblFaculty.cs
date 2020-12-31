namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblFaculty")]
    public partial class tblFaculty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblFaculty()
        {
            tblCourses = new HashSet<tblCourse>();
            tblFacultyLectures = new HashSet<tblFacultyLecture>();
        }

        [Key]
        public int tid { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        public int stateid { get; set; }

        public int createdby { get; set; }

        public int updatedby { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int departmentID { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        public virtual tblAdministrator tblAdministrator1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCourse> tblCourses { get; set; }

        public virtual tblDepartment tblDepartment { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFacultyLecture> tblFacultyLectures { get; set; }
    }
}
