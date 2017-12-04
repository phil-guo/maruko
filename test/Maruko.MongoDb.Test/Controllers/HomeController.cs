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
            //    Name = "alangur",
            //    Password = "654321",
            //    Address = "重庆渝中"
            //};

            //await server.InsertAsync(user);

            var user = await server.GetByIdAsync(new Guid("b787fabf-337d-4bcd-92ec-8571905c87d1"));

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
