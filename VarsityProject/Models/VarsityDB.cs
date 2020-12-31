namespace VarsityProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VarsityDB : DbContext
    {
        public VarsityDB()
            : base("name=VarsityDB")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblAdministrator> tblAdministrators { get; set; }
        public virtual DbSet<tblCourse> tblCourses { get; set; }
        public virtual DbSet<tblCourseLecturer> tblCourseLecturers { get; set; }
        public virtual DbSet<tblCourseSubject> tblCourseSubjects { get; set; }
        public virtual DbSet<tblDepartment> tblDepartments { get; set; }
        public virtual DbSet<tblDepartmentLecture> tblDepartmentLectures { get; set; }
        public virtual DbSet<tblFaculty> tblFaculties { get; set; }
        public virtual DbSet<tblFacultyLecture> tblFacultyLectures { get; set; }
        public virtual DbSet<tblLecturer> tblLecturers { get; set; }
        public virtual DbSet<tblLectureSubject> tblLectureSubjects { get; set; }
        public virtual DbSet<tblState> tblStates { get; set; }
        public virtual DbSet<tblStudent> tblStudents { get; set; }
        public virtual DbSet<tblStudentMark> tblStudentMarks { get; set; }
        public virtual DbSet<tblSubject> tblSubjects { get; set; }
        public virtual DbSet<tblSubjectTest> tblSubjectTests { get; set; }
        public virtual DbSet<tblTest> tblTests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAdministrator>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblAdministrator>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<tblAdministrator>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<tblAdministrator>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblAdministrator>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourses)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourses1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourseLecturers)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourseLecturers1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourseSubjects)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblCourseSubjects1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblDepartments)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.insertedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblDepartments1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblDepartmentLectures)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblDepartmentLectures1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblFaculties)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblFaculties1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblFacultyLectures)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblFacultyLectures1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblLecturers)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblLecturers1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblLectureSubjects)
                .WithRequired(e => e.tblAdministrator)
                .HasForeignKey(e => e.createdby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAdministrator>()
                .HasMany(e => e.tblLectureSubjects1)
                .WithRequired(e => e.tblAdministrator1)
                .HasForeignKey(e => e.updatedby)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCourse>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tblCourse>()
                .Property(e => e.courseCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblCourse>()
                .HasMany(e => e.tblCourseLecturers)
                .WithRequired(e => e.tblCourse)
                .HasForeignKey(e => e.CourseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCourse>()
                .HasMany(e => e.tblCourseSubjects)
                .WithRequired(e => e.tblCourse)
                .HasForeignKey(e => e.CourseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCourse>()
                .HasMany(e => e.tblStudents)
                .WithRequired(e => e.tblCourse)
                .HasForeignKey(e => e.courseid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCourse>()
                .HasMany(e => e.tblSubjects)
                .WithRequired(e => e.tblCourse)
                .HasForeignKey(e => e.courseid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDepartment>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<tblDepartment>()
                .Property(e => e.departmentCode)
                .IsUnicode(false);

            modelBuilder.Entity<tblDepartment>()
                .HasMany(e => e.tblDepartmentLectures)
                .WithRequired(e => e.tblDepartment)
                .HasForeignKey(e => e.departmentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDepartment>()
                .HasMany(e => e.tblFaculties)
                .WithRequired(e => e.tblDepartment)
                .HasForeignKey(e => e.departmentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblFaculty>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tblFaculty>()
                .HasMany(e => e.tblCourses)
                .WithRequired(e => e.tblFaculty)
                .HasForeignKey(e => e.facultyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblFaculty>()
                .HasMany(e => e.tblFacultyLectures)
                .WithRequired(e => e.tblFaculty)
                .HasForeignKey(e => e.FacultyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLecturer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblLecturer>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<tblLecturer>()
                .Property(e => e.EmployeeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<tblLecturer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tblLecturer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tblLecturer>()
                .HasMany(e => e.tblCourseLecturers)
                .WithRequired(e => e.tblLecturer)
                .HasForeignKey(e => e.LectureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLecturer>()
                .HasMany(e => e.tblDepartmentLectures)
                .WithRequired(e => e.tblLecturer)
                .HasForeignKey(e => e.lectureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLecturer>()
                .HasMany(e => e.tblFacultyLectures)
                .WithRequired(e => e.tblLecturer)
                .HasForeignKey(e => e.LectureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLecturer>()
                .HasMany(e => e.tblLectureSubjects)
                .WithRequired(e => e.tblLecturer)
                .HasForeignKey(e => e.LectureID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .Property(e => e.Heading)
                .IsUnicode(false);

            modelBuilder.Entity<tblState>()
                .Property(e => e.FriendlyHeading)
                .IsUnicode(false);

            modelBuilder.Entity<tblState>()
                .Property(e => e.Acronym)
                .IsUnicode(false);

            modelBuilder.Entity<tblState>()
                .Property(e => e.Active)
                .IsFixedLength();

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblCourses)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblCourseLecturers)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblCourseSubjects)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblDepartments)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblDepartmentLectures)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblFaculties)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblFacultyLectures)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblLecturers)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblLectureSubjects)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblStudents)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblSubjects)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblSubjectTests)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblState>()
                .HasMany(e => e.tblTests)
                .WithRequired(e => e.tblState)
                .HasForeignKey(e => e.stateid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.studentNo)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.cellphone)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<tblStudent>()
                .HasMany(e => e.tblStudentMarks)
                .WithRequired(e => e.tblStudent)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblSubject>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tblSubject>()
                .HasMany(e => e.tblCourseSubjects)
                .WithRequired(e => e.tblSubject)
                .HasForeignKey(e => e.SubjectID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblSubject>()
                .HasMany(e => e.tblLectureSubjects)
                .WithRequired(e => e.tblSubject)
                .HasForeignKey(e => e.SubjectID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblSubject>()
                .HasMany(e => e.tblSubjectTests)
                .WithRequired(e => e.tblSubject)
                .HasForeignKey(e => e.subjectid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblSubjectTest>()
                .HasMany(e => e.tblStudentMarks)
                .WithRequired(e => e.tblSubjectTest)
                .HasForeignKey(e => e.subjectTestID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblTest>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<tblTest>()
                .HasMany(e => e.tblSubjectTests)
                .WithRequired(e => e.tblTest)
                .HasForeignKey(e => e.testid)
                .WillCascadeOnDelete(false);
        }
    }
}
