using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : BaseDomainModel
    {
        public User()
        {
            Activities = new HashSet<Activity>();
        }

        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int? Cellphone { get; set; }  
        public string Country { get; set; }
        public bool ContactInfo { get; set; }

        public  ICollection<Activity> Activities { get; set; }
    }
}
