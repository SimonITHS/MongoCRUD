
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDBDemoo.data;
using MongoDBDemoo.models;

namespace MongoDBDemoo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            MongoCRUD db = new MongoCRUD("azurekurser");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/course", async (Courses course) =>
            {
                var testdb = await db.AddCourse("Courses", course);
                return Results.Ok(testdb);
                
                
            });

            app.MapGet("/courses", async () =>
            {
                var testdb = await db.GetAllCourses("Courses");
                return Results.Ok(testdb);
            });


            //get by id

            app.MapGet("/course/{id}", async (string id) =>
            {
                var course = await db.GetById("Courses", id);
                return Results.Ok(course);

            });
            //update

            app.MapPut("/course/{id}", async (string id, Courses updateCourse) =>
            {
                var testdb = await db.UpdateCourseById("Courses", id, updateCourse);
                return Results.Ok(testdb);
            });
            //Delete
            app.MapDelete("/course/{id}", async (string id) =>
            {
                var course = await db.DeleteCourse("Courses", id);
                return Results.Ok(course);
            });

            app.Run();
        }
    }
}
