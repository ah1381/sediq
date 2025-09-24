using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class StudentResponseDto
    {
        public long Id { get; set; }
        public int StudentCode { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string FieldOfStudy { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class StudentCreateDto
    {
        public int StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string FieldOfStudy { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class StudentEditDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FieldOfStudy { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class StudentDeleteDto
    {
        public long Id { get; set; }
    }
}
