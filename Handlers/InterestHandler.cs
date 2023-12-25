using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using APIProject.Models;
using APIProject.Data;
using System.Linq;
using System;
using APIProject.Models;

namespace APIProject.Handlers
{
    public class InterestHandler
    {
        // Add interest and link to specific person
        public static IResult AddInterestToPerson(ApplicationContext context, int id, string newTitle, string newDescription, string newUrl, string newLinkDescription)
        {
            Person newInterestPerson = context.Persons
                .Include(p => p.Interests)
                .Include(p => p.InterestLinks)
                .FirstOrDefault(p => p.Id == id);

            if (newInterestPerson == null)
            {
                return Results.NotFound();
            }

            Interest newInterest = new Interest()
            {
                Title = newTitle,
                Description = newDescription
            };

            InterestLink newLink = new InterestLink()
            {
                Url = newUrl,
                Description = newLinkDescription
            };

            // Adding new interest and link
            newInterestPerson.Interests.Add(newInterest);
            newInterestPerson.InterestLinks.Add(newLink);

            newInterest.Persons = new List<Person> { newInterestPerson };
            newLink.Person = newInterestPerson;

            context.SaveChanges();

            return Results.Json(newInterestPerson);
        }
    }
}
