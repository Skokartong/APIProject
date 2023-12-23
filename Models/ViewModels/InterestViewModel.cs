namespace APIProject.Models.ViewModels
{
    public class InterestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // Every interest can have a collection of people and links tied to it
        public PersonViewModel[] Persons { get; set; }
        public InterestLinkViewModel[] Links { get; set; }
    }
}
