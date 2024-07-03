using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Model.User> Get()
        {
            using (var db = new Model.CinepmDBContext())
            {
                IEnumerable<Model.User> users = db.Users.ToList();
                return users;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            using(var db = new Model.CinepmDBContext())
            {
                var obj = await db.Users.FindAsync(id);

                if (obj == null)
                {
                    return NotFound();
                }

                return Ok(obj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Model.User user)
        {
            if(user == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState); ;
            }

            using(var db = new Model.CinepmDBContext())
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();

                return Ok();
            }
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,Model.User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            using(var db = new Model.CinepmDBContext())
            {
                db.Entry(user).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!RegisterExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            using(var db = new Model.CinepmDBContext())
            {
                var user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();

                return Ok();
            }
        }

        private bool RegisterExists(int id)
        {
            using(var db = new Model.CinepmDBContext())
            {
                return db.Users.Any(u => u.Id == id);
            }
        } 
        

    }
}
