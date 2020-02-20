using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMgmtClient.Models
{
    public class Contact
    {

        public int ContactId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage ="Please enter valid email")]
        public string Email { get; set; }

        [Display(Name ="Contact Number")]
        //[RegularExpression(@"^[1-9]\d$", ErrorMessage ="Please enter valid contact number")]
        public string ContactNumber { get; set; }
        public bool Status { get; set; }


    }
}