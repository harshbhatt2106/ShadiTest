using Microsoft.AspNetCore.Mvc;
using ShadiTest.Models;

namespace ShadiTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User { Id = 1, Name = "Harsh", Email = "harsh@example.com", Age = 25 },
        new User { Id = 2, Name = "Rahul", Email = "rahul@example.com", Age = 28 }
    };

    private static int _nextId = 2;

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(Users);
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetById(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);

        if (user is null)
            return NotFound(new { message = "User not found." });

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> Create(User user)
    {
        user.Id = Interlocked.Increment(ref _nextId);
        Users.Add(user);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, User updatedUser)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);

        if (user is null)
            return NotFound(new { message = "User not found." });

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = Users.FirstOrDefault(x => x.Id == id);

        if (user is null)
            return NotFound(new { message = "User not found." });

        Users.Remove(user);
        return NoContent();
    }
}
