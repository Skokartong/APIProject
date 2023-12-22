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

            // Add new interest and link for specific person
            app.MapPost("/persons/{id}/interests/add", InterestHandler.AddInterestToPerson);

            // View persons that share the same interest
            app.MapPost("/interests/{id}/persons", InterestHandler.ViewInterest);

            app.Run();         
        }
    }
}

