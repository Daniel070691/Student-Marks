namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblStudentCourse")]
    public partial class tblStudentCourse
    {
        [Key]
        public long TID { get; set; }

        public long studentID { get; set; }

        public long courseID { get; set; }

        public virtual tblCourse tblCourse { get; set; }

        public virtual tblStudent tblStudent { get; set; }
    }
}
