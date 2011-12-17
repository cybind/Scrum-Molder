using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace eGo.ScrumMolder.Dto
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public String PasswordSalt { get; set; }

        public int UserType { get; set; }
    }
}
