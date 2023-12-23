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
using APIProject.Models.DTO;
using APIProject.Models.ViewModels;

namespace APIProject.Handlers
{
    public class InterestHandler
    {
        public static IResult AddInterestToPerson(ApplicationContext context, int id, InterestDto interestDto, InterestLinkDto interestLinkDto)
        {
            Person newInterestPerson = context.Persons
                .Where(p => p.Id == id)
                .Include(p => p.Interests)
                .Include(p => p.InterestLinks)
                .Single();

            if (newInterestPerson == null)
            {
                return Results.NotFound();
            }

            newInterestPerson.Interests.Add(new Interest()
            {
                Title = interestDto.Title,
                Description = interestDto.Description
            });

            newInterestPerson.InterestLinks.Add(new InterestLink()
            {
                Url = interestLinkDto.Url,
                Description = interestLinkDto.Description
            });

            context.SaveChanges();

            return Results.Json(newInterestPerson);
        }

        public static IResult ViewInterest(ApplicationContext context, int id)
        {
            var interestPerson = context.Interests
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

            if (interestPerson == null)
            {
                return Results.NotFound();
            }

            return Results.Json(interestPerson);
        }
    }
}
