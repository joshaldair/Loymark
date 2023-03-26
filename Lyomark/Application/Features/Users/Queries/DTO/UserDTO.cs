using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int? Cellphone { get; set; }
        public string Country { get; set; }
        public bool ContactInfo { get; set; }
        public int Id { get; set; }
    }
}
