namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCourse")]
    public partial class tblCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCourse()
        {
            tblCourseLecturers = new HashSet<tblCourseLecturer>();
            tblCourseSubjects = new HashSet<tblCourseSubject>();
            tblStudentCourses = new HashSet<tblStudentCourse>();
            tblSubjects = new HashSet<tblSubject>();
        }

        [Key]
        public long tid { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        [StringLength(50)]
        public string courseCode { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int createdby { get; set; }

        public int updatedby { get; set; }

        public int stateid { get; set; }

        public int facultyID { get; set; }

        public int duration { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        public virtual tblAdministrator tblAdministrator1 { get; set; }

        public virtual tblFaculty tblFaculty { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCourseLecturer> tblCourseLecturers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCourseSubject> tblCourseSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStudentCourse> tblStudentCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSubject> tblSubjects { get; set; }
    }
}
