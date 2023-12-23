namespace APIProject.Models.ViewModels
{
    public class PersonViewModel
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }

        // Every person can have multiple interests and links tied to him/her
        public InterestViewModel[] Interests { get; set; }
        public InterestLinkViewModel[] InterestLinks { get; set; }
    }
}
