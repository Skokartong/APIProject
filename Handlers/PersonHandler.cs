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
using APIProject.Models.DTO;
using APIProject.Models.ViewModels;

namespace APIProject.Handlers
{
    public static class PersonHandler
    {
        public static IResult ViewAllPersons(ApplicationContext context)
        {
            PersonViewModel[] listPersons = context.Persons
                .Select(p => new PersonViewModel()
                {
                    Name = p.Name,
                    Age = p.Age,
                    PhoneNumber = p.PhoneNumber,
                })
                .ToArray();

            return Results.Json(listPersons);
        }
        public static IResult ViewSinglePerson(ApplicationContext context, int id)
        {
            var singlePerson = context.Persons
                .Where(p => p.Id == id)
                .Select(p => new PersonViewModel()
                {
                    Name = p.Name,
                    Age = p.Age,
                    PhoneNumber = p.PhoneNumber
                })
                .ToList();

            if (singlePerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(singlePerson);
        }

        public static IResult ViewInterestsPerson(ApplicationContext context, int personId)
        {
            var interestPerson = context.Persons
                .Include(p => p.Interests)
                .Where(p => p.Id == personId)
                .Select(p => new
                {
                    p.Name,
                    Interests = p.Interests.Select(i => new
                    {
                        i.Title,
                        i.Description
                    })
                })
                .ToList();

            if (interestPerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(interestPerson);
        }

        public static IResult ViewLinksPerson(ApplicationContext context, int personId)
        {
            var linksPerson = context.Persons
                .Include(p => p.InterestLinks)
                .Where(p => p.Id == personId)
                .Select(p => new
                {
                    p.Name,
                    InterestLinks = p.InterestLinks.Select(l => new
                    {
                        l.Url,
                        l.Description
                    })
                })
                .ToList();

            if (linksPerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(linksPerson);
        }

        public static IResult AddNewPerson(ApplicationContext context, PersonDto personDto)
        {
            Person newPerson = new Person()
            {
                Name = personDto.Name,
                Age = personDto.Age,
                PhoneNumber = personDto.PhoneNumber
            };

            context.Persons.Add(newPerson);
            context.SaveChanges();

            if (newPerson == null)
            {
                return Results.Problem();
            }

            return Results.Json(newPerson);
        }
    }
}
