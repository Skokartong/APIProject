using Microsoft.EntityFrameworkCore;
using Microsoft.Data;
using APIProject.Models;
using System.Reflection;

namespace APIProject
{
    public class Interest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<InterestLink> InterestLinks { get; set; }
    }
}

