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
using APIProject.Models.ViewModels;
using APIProject.Models.DTO;
using System.Net;
using System.Reflection;

namespace APIProject.Handlers
{
    public class InterestHandler
    {
        // Viewing all interests using 'InterestViewModel' class
        public static IResult ViewAllInterests(ApplicationContext context)
        {
            InterestViewModel[] listInterests = context.Interests
                .Select(i => new InterestViewModel()
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description
                })
                .ToArray();

            return Results.Json(listInterests);
        }

        public static IResult ViewSpecificInterest(ApplicationContext context, int id)
        {
            var specificInterest = context.Interests
                .Where(i => i.Id == id)
                .Include(i => i.Persons)
                .Select(i => new
                {
                    i.Title,
                    i.Description,
                    Persons = i.Persons.Select(p => new
                    {
                        p.Name
                    })
                })
                .ToList();

            if(specificInterest == null)
            {
                return Results.NotFound();
            }

            return Results.Json(specificInterest);
        }

        // Creating new interest using 'InterestDto' properties
        public static IResult AddInterest(ApplicationContext context, InterestDto interestDto)
        {
            Interest newInterest = new Interest()
            {
                Title = interestDto.Title,
                Description = interestDto.Description
            };

            // Saving new interest to database
            context.Interests.Add(newInterest);
            context.SaveChanges();

            // Displaying added interest to user
            InterestViewModel interestViewModel = new InterestViewModel()
            {
                Id = newInterest.Id,
                Title = newInterest.Title,
                Description = newInterest.Description
            };

            return Results.Json(interestViewModel);

        }

        // Creating new link and linking it to person and interest
        public static IResult AddLink(ApplicationContext context, int personId, int interestId, InterestLinkDto interestLinkDto)
        {
            InterestLink existingLink = context.InterestLinks
                .FirstOrDefault(l => l.PersonId == personId && l.InterestId == interestId);

            if (existingLink != null)
            {
                return Results.Conflict();
            }

            InterestLink newLink = new InterestLink()
            {
                Url = interestLinkDto.Url,
                Description = interestLinkDto.Description,
                PersonId = personId,
                InterestId = interestId
            };

            context.InterestLinks.Add(newLink);
            context.SaveChanges();

            InterestLinkViewModel interestLinkViewModel = new InterestLinkViewModel()
            {
                Id = newLink.Id,
                Url = newLink.Url,
                Description = newLink.Description
            };

            return Results.Json(interestLinkViewModel);
        }

        // Adding interest to specific person
        public static IResult AddInterestToPerson(ApplicationContext context, int personId, int interestId)
        {
            try
            {
                Interest interestToAdd = context.Interests
               .FirstOrDefault(i => i.Id == interestId);

                // Checking if interest exist or not
                if (interestToAdd == null)
                {
                    return Results.Problem(statusCode: 404, detail: "Interest not found");
                }

                Person person = context.Persons
               .Include(p => p.Interests)
               .FirstOrDefault(p => p.Id == personId);

                // Checking if person exists in database
                if (person == null)
                {
                    return Results.Problem(statusCode: 404, detail: "Person not found");
                }

                person.Interests.Add(interestToAdd);
                context.SaveChanges();

                // Displaying the interest that gets saved via 'InterestViewModel' class
                InterestViewModel interestViewModel = new InterestViewModel()
                {
                    Id = interestToAdd.Id,
                    Title = interestToAdd.Title,
                    Description = interestToAdd.Description
                };

                return Results.Json(interestViewModel);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Results.Problem();
            }
        }
    }
}
