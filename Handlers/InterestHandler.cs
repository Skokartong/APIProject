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

            return Results.Json(newInterest);
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

            return Results.Json(newLink);
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
