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

namespace APIProject.Handlers
{
    public class InterestHandler
    {
        public static IResult AddInterestToPerson(ApplicationContext context, int id, Interest newInterest, [FromBody]InterestLink newInterestLink)
        {
            var newInterestPerson = context.Persons
                .Include(p => p.Interests)
                .Include(p => p.InterestLinks)
                .FirstOrDefault(p => p.Id == id);

            if (newInterestPerson == null)
            {
                return Results.NotFound();
            }

            var interest = new Interest
            {
                Title = newInterest.Title,
                Description = newInterest.Description
            };
            newInterestPerson.Interests.Add(interest);

            var interestLink = new InterestLink
            {
                Url = newInterestLink.Url,
                Description = newInterestLink.Description
            };
            newInterestPerson.InterestLinks.Add(interestLink);
            context.SaveChanges();

            return Results.Json(newInterestPerson);
        }

        public static IResult ViewInterest(ApplicationContext context, int id)
        {
            var viewInterest = context.Interests
                .Include(i => i.Persons)
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    i.Title,
                    Persons = i.Persons.Select(p => new
                    {
                        p.Name,
                        p.Age
                    })
                })
                .FirstOrDefault();

            if(viewInterest == null)
            {
                return Results.NotFound();
            }

            return Results.Json(viewInterest);
        }
    }
}
