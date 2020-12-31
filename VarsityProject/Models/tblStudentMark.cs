namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblStudentMark
    {
        [Key]
        public long TID { get; set; }

        public long subjectTestID { get; set; }

        public long StudentID { get; set; }

        public int Mark { get; set; }

        public virtual tblStudent tblStudent { get; set; }

        public virtual tblSubjectTest tblSubjectTest { get; set; }
    }
}
