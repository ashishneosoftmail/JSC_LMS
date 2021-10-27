using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JSC_LSM.UI.Models
{
    public class Login
    {
        [Required]
        public String Email { get; set; }
        public String Password { get; set; }
        public int Role { get; set; }
    }
}
