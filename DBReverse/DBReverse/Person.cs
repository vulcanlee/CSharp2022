using System;
using System.Collections.Generic;

namespace DBReverse
{
    public partial class Person
    {
        public Person()
        {
            StudentGrades = new HashSet<StudentGrade>();
            Courses = new HashSet<Course>();
        }

        public int PersonId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; } = null!;
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
