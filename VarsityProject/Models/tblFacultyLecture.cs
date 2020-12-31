namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblFacultyLecture")]
    public partial class tblFacultyLecture
    {
        [Key]
        public long TID { get; set; }

        public int FacultyID { get; set; }

        public int LectureID { get; set; }

        public int createdby { get; set; }

        public int updatedby { get; set; }

        public DateTime insertdate { get; set; }

        public DateTime lastupdate { get; set; }

        public int stateid { get; set; }

        public virtual tblAdministrator tblAdministrator { get; set; }

        public virtual tblAdministrator tblAdministrator1 { get; set; }

        public virtual tblFaculty tblFaculty { get; set; }

        public virtual tblLecturer tblLecturer { get; set; }

        public virtual tblState tblState { get; set; }
    }
}
