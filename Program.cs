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
        // Web API Project that showcases people and their interests
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            // View all persons
            app.MapGet("/persons", PersonHandler.ViewAllPersons);

            // View specific person
            app.MapGet("/persons/{id}", PersonHandler.ViewSinglePerson);

            // View all interests tied to a single person
            app.MapGet("/persons/{id}/interests", PersonHandler.ViewInterestsPerson);

            // View all links tied to a single person
            app.MapGet("/persons/{id}/interestLinks", PersonHandler.ViewLinksPerson);

            // Add new person
            app.MapPost("/persons/add", PersonHandler.AddNewPerson);

            // Add new interest
            app.MapPost("/interests/add", InterestHandler.AddInterest);

            // Add new link for specific person and interest
            app.MapPost("persons/{personId}/links/{interestId}/add", InterestHandler.AddLink);

            // Add interest for specific person
            app.MapPost("/persons/{personId}/interests/{interestId}/add", InterestHandler.AddInterestToPerson);

            // Search for specific person (their name)
            app.MapGet("/persons/{name}", PersonHandler.SearchPerson);

            app.Run();         
        }
    }
}

