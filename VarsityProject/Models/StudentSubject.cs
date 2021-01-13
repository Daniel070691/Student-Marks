namespace VarsityProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentSubject")]
    public partial class StudentSubject
    {
        public int StudentSubjectID { get; set; }

        public long StudentID { get; set; }

        public long SubjectID { get; set; }

        public virtual tblStudent tblStudent { get; set; }

        public virtual tblSubject tblSubject { get; set; }
    }
}
