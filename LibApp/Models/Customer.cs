using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        // Data Annotation - convention name
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
        public bool SubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public Customer()
        {

        }
    }
}
