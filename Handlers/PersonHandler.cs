using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using APIProject.Models;
using Microsoft.AspNetCore.Mvc;
using APIProject.Data;
using System.Linq;
using System;

namespace APIProject.Handlers
{
    public static class PersonHandler 
    {
        public static IResult ViewAllPersons(ApplicationContext context)
        {
            try
            {
                var persons = context.Persons
                    .Select(p => new {p.Id, p.Name, p.Age, p.PhoneNumber})
                    .ToList();
                return Results.Json(persons);
            }

            catch (Exception ex)
            {
                return Results.NotFound();
            }
        }

        public static IResult ViewSinglePerson(ApplicationContext context, int id)
        {
            var person = context.Persons
                .Where(p => p.Id == id)
                .SingleOrDefault();

            if (person == null)
            {
                return Results.NotFound();
            }

            return Results.Json(person);
        }

        public static IResult ViewInterestsPerson(ApplicationContext context, int id)
        {
            var interestPerson = context.Persons
                .Include(p => p.Interests)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Name,
                    Interests = p.Interests.Select(i => new
                    {
                        i.Title,
                        i.Description
                    })
                })
                .FirstOrDefault();

            if (interestPerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(interestPerson);
        }

        public static IResult ViewLinksPerson(ApplicationContext context, int id)
        {
            var linksPerson = context.Persons
                .Include(p => p.InterestLinks)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.Name,
                    InterestLinks = p.InterestLinks.Select(l => new
                    {
                        l.Url,
                        l.Description
                    })
                })
                .FirstOrDefault();

            if (linksPerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(linksPerson);
        }
    }
}
