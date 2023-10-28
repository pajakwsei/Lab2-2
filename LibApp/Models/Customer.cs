namespace LibApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MembershipType MembershipType { get; set; }
        public Customer()
        {

        }
    }
}
