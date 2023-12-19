using Microsoft.EntityFrameworkCore;
using APIProject.Data;
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

            // Creating objects to our database
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

            // Saving objects to our database
            using (var context = new ApplicationContext())
            {
                context.Interests.AddRange(interests);
                context.Persons.AddRange(persons);
                context.SaveChanges();
            }

            // Viewing all persons
            app.MapGet("/persons/", () => Results.Json(persons));

            // Viewing all interests
            app.MapGet("/interests/", () => Results.Json(interests));

            // POST new interest
            app.MapPost("/interests/add", (ApplicationContext context, Interest interest) =>
            {
                interests.Add(interest);
                return Results.Created();
            });

            // Viewing all people with the same interest
            app.MapGet("/interests/{id}", (ApplicationContext context, int id) =>
            {
                if (id < 0 || id >= interests.Count)
                {
                    return Results.NotFound();
                }

                return Results.Json(interests[id]);
            });

            // Viewing a specific person and their interests
            app.MapGet("/persons/{id}", (ApplicationContext context, int id) =>
            {
                if (id < 0 || id >= persons.Count)
                {
                    return Results.NotFound();
                }

                return Results.Json(persons[id]);
            });

            app.Run();

            //app.MapGet("/persons/", () => Results.Json(persons));

            //app.MapGet("/persons/", () => Results.Json(persons.Select(p => p.Interests));

            //app.Run();
        }
    }
}
