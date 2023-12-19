using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data;
using APIProject.Models;
using System;

namespace APIProject
{
    public class Person
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<InterestLink> InterestLinks { get; set; }
    }
}
