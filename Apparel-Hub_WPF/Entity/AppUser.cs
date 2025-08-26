using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amedig_Panama_WPF.Entity
{
    public class AppUser
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string TabUserRole { get; set; }
        public string Department { get; set; }
        public string ComputerName { get; set; }
        public string ComputerIp { get; set; }
        public DateTime? Created { get; set; }
        public bool IsActive { get; set; }
        public string ActionBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
