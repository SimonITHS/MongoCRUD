using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoo.models;

namespace MongoDBDemoo.data
{
    public class MongoCRUD
    {
        private IMongoDatabase db;


        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        //add course 
        public async Task<List<Courses>> AddCourse(string table, Courses course)
        {
            var collection = db.GetCollection<Courses>(table);
            await collection.InsertOneAsync(course);
            return collection.AsQueryable().ToList();
        }

        //get all courses

        public async Task<List<Courses>> GetAllCourses(string table)
        {
            var collection = db.GetCollection<Courses>(table);
            var courses = await collection.AsQueryable().ToListAsync();
            return courses;
        }


        //update course
        public async Task<Courses> UpdateCourseById(string table, string id, Courses updatedCourse)
        {
            var collection = db.GetCollection<Courses>(table);

            var existingCourse = await collection.Find(x =>x.Id == id).FirstOrDefaultAsync(); 
            if (existingCourse == null)
            {
                return null;
            }
            else
            {
                var update = Builders<Courses>.Update
                    .Set(x => x.Name, updatedCourse.Name)
                    .Set(x => x.Description, updatedCourse.Description)
                    .Set(x => x.Link, updatedCourse.Link)
                    .Set(x => x.Category, updatedCourse.Category);

                await collection.UpdateOneAsync(x => x.Id == id, update);
                return await collection.Find(x =>x.Id == id).FirstOrDefaultAsync(); 
            }

        }
        //get course by id

        public async Task<Courses> GetById(string table, string id)
        {
            var collection = db.GetCollection<Courses>(table);
            var course = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return course;
        }

        //delete course

        public async Task<string> DeleteCourse(string table, string id)
        {
            var collection = db.GetCollection<Courses>(table);
            var course = await collection.DeleteOneAsync(x => x.Id == id);
            return "Deleted course";
        }




    }
}
