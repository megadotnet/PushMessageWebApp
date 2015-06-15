using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAuth.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "角色名称")]
        public string Name { get; set; }
    }
}