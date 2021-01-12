namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblDepartment")]
    public partial class tblDepartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDepartment()
        {
            tblDepartmentLectures = new HashSet<tblDepartmentLecture>();
            tblFaculties = new HashSet<tblFaculty>();
        }

        [Key]
        public int tid { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string departmentCode { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int insertedby { get; set; }

        public int updatedby { get; set; }

        public int stateid { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        public virtual tblAdministrator tblAdministrator1 { get; set; }

        public virtual tblState tblState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDepartmentLecture> tblDepartmentLectures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFaculty> tblFaculties { get; set; }
    }
}
