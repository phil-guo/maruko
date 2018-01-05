using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Maruko.MongoDB.MongoDBRepos;
using Maruko.Domain.Entities;
using MongoDB.Driver;

namespace Maruko.MongoDb.Test.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index([FromServices] MongoDbBaseRepository<User, Guid> server)
        {
            //var user = new User()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "simple",
            //    Password = "qwe123QWE",
            //    Address = "重庆江北"
            //};

            //await server.InsertAsync(user);

            var user = await server.GetByIdAsync(new Guid("d06fe44f-d603-4e7b-a4cb-080fd181ab3a"));

            var userList = await server.SearchAsync(Builders<User>.Filter.Empty);

            return View();
        }
    }

    public class User : Entity<Guid>, IHasCreationTime, IHasModificationTime
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
