using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            this.Student = new tblStudent();
        }
        public tblStudent Student { get; set; }
        public List<Student> StudentList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public string courseid { get; set; }
        public List<StudentCourse> StudentCourseList { get; set; }
        public List<StudentSubjectDTO> StudentSubjectList { get; set; }
        public StudentSubjectDTO StudentSubject { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public selectedValues SelectedValues { get; set; }
        public List<int> courseids { get; set; }
        public StudentCourse selectedCourse { get; set; }

        public StudentTestDTO Test { get; set; }
        public List<StudentTestDTO> TestList { get; set; }
    }

    public class Student
    {
        public int TID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }
        public int status { get; set; }
        public List<StudentCourse> StudentCourseList { get; set; }

    }
    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }
        public List<StudentCourse> MyCourses { get; set; }
    }
    public class StudentCourse
    {
        public int courseID { get; set; }
        public string courseTitle { get; set; }
        public List<StudentSubjectDTO> CourseSubjectList { get; set; }
    }

    public class StudentSubjectDTO
    {
        public int subjectID { get; set; }
        public string subjectTitle { get; set; }
        public List<StudentTestDTO> SubjectTests { get; set; }
    }
    public class StudentTestDTO
    {
        public int testID { get; set; }
        public string testTitle { get; set; }
        public int testMark { get; set; }
    }
    
}