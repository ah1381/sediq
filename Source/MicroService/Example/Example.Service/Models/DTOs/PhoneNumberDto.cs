using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class PhoneNumberResponseDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Ownership { get; set; }
    }

    public class PhoneNumberCreateDto
    {
        public string Number { get; set; }
        public string Ownership { get; set; }
    }

    public class PhoneNumberEditDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Ownership { get; set; }
    }

    public class PhoneNumberDeleteDto
    {
        public int Id { get; set; }
    }
}
