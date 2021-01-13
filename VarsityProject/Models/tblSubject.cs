namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblSubject")]
    public partial class tblSubject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSubject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            tblCourseSubjects = new HashSet<tblCourseSubject>();
            tblLectureSubjects = new HashSet<tblLectureSubject>();
            tblSubjectTests = new HashSet<tblSubjectTest>();
        }

        [Key]
        public long tid { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        [StringLength(50)]
        public string subjectCode { get; set; }

        public int stateid { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int createdby { get; set; }

        public int updatedby { get; set; }

        public long? courseid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        public virtual tblCourse tblCourse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCourseSubject> tblCourseSubjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLectureSubject> tblLectureSubjects { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSubjectTest> tblSubjectTests { get; set; }
    }
}
