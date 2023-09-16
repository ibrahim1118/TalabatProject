using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Adminpanal.Models
{
    public class RoleVM
    {
      
        [MaxLength(100)]
        public string Name { get; set; }    
    }
}
