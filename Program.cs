using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIProject.Data;
using APIProject.Models;
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

            // Creating list of objects to our database (JSON format)
            List<Interest> interests = new List<Interest>()
            {
                new Interest() {Title = "Drawing", Description = "Drawing different objects"},
                new Interest() {Title = "Workout", Description = "Hit the gym regularly" },
                new Interest() {Title = "Gaming", Description = "Video gaming and/or computer games"},
                new Interest() {Title = "Cooking", Description = "Trying new recipes and experimenting in kitchen"},
                new Interest() {Title = "Programming", Description = "Program different type of software and/or games"},
                new Interest() {Title = "Football", Description = "Run around and hit a ball"}
            };

            List<Person> persons = new List<Person>()
            {
                new Person() {Age = 28, Name = "Sara", PhoneNumber = 07077777},
                new Person() {Age = 20, Name = "William", PhoneNumber = 08979779},
                new Person() {Age = 37, Name = "Filip", PhoneNumber = 34332544},
                new Person() {Age = 22, Name = "Kristina", PhoneNumber = 54543543}
            };

            List<InterestLink> interestLinks = new List<InterestLink>()
            {
                new InterestLink() { Url = "https://www.pinterest.com/", Description = "Website for artistic people to share images" },
                new InterestLink() { Url = "https://github.com/", Description = "Website for programmers to share and collaborate on code" },
                new InterestLink() { Url = "https://www.twitch.tv/", Description = "Website for streaming games" },
                new InterestLink() { Url = "https://www.bbc.co.uk/food", Description = "Recipes for food enthusiasts" },
                new InterestLink() { Url = "https://sportamore.com/", Description = "E-commerce for active people" },
                new InterestLink() { Url = "https://www.nfl.com/", Description = "News, statistics and other valuable information about football teams" }
            };

            // Saving objects to our database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                context.Interests.AddRange(interests);
                context.Persons.AddRange(persons);
                context.InterestLinks.AddRange(interestLinks);
                context.SaveChanges();

                persons[0].Interests.Add(interests[0]);
                persons[0].InterestLinks.Add(interestLinks[0]);
                persons[0].Interests.Add(interests[4]);
                persons[0].InterestLinks.Add(interestLinks[1]);

                persons[1].Interests.Add(interests[2]);
                persons[1].InterestLinks.Add(interestLinks[2]);

                persons[2].Interests.Add(interests[1]);
                persons[2].InterestLinks.Add(interestLinks[4]);
                persons[2].Interests.Add(interests[5]);
                persons[2].InterestLinks.Add(interestLinks[5]);

                persons[3].Interests.Add(interests[3]);
                persons[3].InterestLinks.Add(interestLinks[3]);
                persons[3].Interests.Add(interests[1]);
                persons[3].InterestLinks.Add(interestLinks[4]);
                persons[3].Interests.Add(interests[4]);
                persons[3].InterestLinks.Add(interestLinks[1]);
            };

            // Viewing all persons
            app.MapGet("/persons/", () => Results.Json(persons));

            // Viewing all interests
            app.MapGet("/interests/", () => Results.Json(interests));

            // Viewing all links associated with interests
            app.MapGet("/interestLinks/", () => Results.Json(interestLinks));

            // POST new interest
            app.MapPost("/interests/add", (ApplicationContext context, Interest interest) =>
            {
                interests.Add(interest);
                return Results.Created();
            });

            // POST new link associated with person and interest
            app.MapPost("/interestLinks/add", (ApplicationContext context, InterestLink interestLink) =>
            {
                interestLinks.Add(interestLink);
                return Results.Created();
            });

            // Viewing all persons tied to specific interest
            app.MapGet("/interests/persons/{id}", () => Results.Json(interests.Select(i => i.Persons)));

            // Viewing specific person and all their interests
            app.MapGet("/persons/interests/{id}", () => Results.Json(persons.Select(p => p.Interests)));

            // Viewing all links associated with specific person
            app.MapGet("/interestLinks/persons/{id}", () => Results.Json(interestLinks.Select(l => l.Persons)));

            // Viewing a specific person and all their basic information
            app.MapGet("/persons/{id}", (ApplicationContext context, int id) =>
            {
                if (id < 0 || id >= persons.Count)
                {
                    return Results.NotFound();
                }

                return Results.Json(persons[id]);
            });

            // Viewing a specific interest and all information about it
            app.MapGet("/interests/{id}", (ApplicationContext context, int id) =>
            {
                if (id < 0 || id >= interests.Count)
                {
                    return Results.NotFound();
                }

                return Results.Json(interests[id]);
            });

            app.Run();
            
        }
    }
}

