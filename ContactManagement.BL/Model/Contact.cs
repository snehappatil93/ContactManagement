using System.ComponentModel.DataAnnotations;

namespace ContactMgmt.BL.Model
{
    public class ContactModel
    {
        
        public int ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public bool Status { get; set; }
    }
}
