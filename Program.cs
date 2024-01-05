using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using APIProject.Handlers;
using APIProject.Models;
using APIProject.Data;
using System.Linq;
using System;

namespace APIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            // GET
            // View all persons
            app.MapGet("/persons", PersonHandler.ViewAllPersons);

            // View all interests
            app.MapGet("/interests", InterestHandler.ViewAllInterests);

            // View specific person
            app.MapGet("/persons/{id}", PersonHandler.ViewSinglePerson);

            // View specific interest and persons that share it
            app.MapGet("interests/{id}", InterestHandler.ViewSpecificInterest);

            // View all interests tied to a single person
            app.MapGet("/persons/{personId}/interests", PersonHandler.ViewInterestsPerson);

            // View all links tied to a single person
            app.MapGet("/persons/{personId}/links", PersonHandler.ViewLinksPerson);


            // POST
            // Add new person
            app.MapPost("/persons/add", PersonHandler.AddNewPerson);

            // Add new interest
            app.MapPost("/interests/add", InterestHandler.AddInterest);

            // Add new link for specific person and interest
            app.MapPost("persons/{personId}/links/{interestId}/add", InterestHandler.AddLink);

            // Add interest for specific person
            app.MapPost("/persons/{personId}/interests/{interestId}/add", InterestHandler.AddInterestToPerson);

            app.Run();         
        }
    }
}

