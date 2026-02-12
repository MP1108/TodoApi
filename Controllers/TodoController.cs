using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static List<Todo> todos = new List<Todo>
    {
        new Todo { Id = 1, Tytul = "Kupić wode", Zrobione = false },
        new Todo { Id = 2, Tytul = "Zrobić projekt na zaliczenie", Zrobione = true }
    };

    private static int nextId = 3;

    // GET
    [HttpGet]
    public ActionResult<List<Todo>> GetAll()
    {
        return Ok(todos);
    }

    // GET
    [HttpGet("{id}")]
    public ActionResult<Todo> Get(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo == null)
            return NotFound("Nie ma takiego zadania");

        return Ok(todo);
    }

    // POST
    [HttpPost]
    public ActionResult<Todo> Create([FromBody] Todo nowe)
    {
        if (string.IsNullOrWhiteSpace(nowe.Tytul))
            return BadRequest("Musisz podać tytuł");

        nowe.Id = nextId++;
        nowe.Dodano = DateTime.Now;
        todos.Add(nowe);

        return CreatedAtAction(nameof(Get), new { id = nowe.Id }, nowe);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Todo updated)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo == null)
            return NotFound();

        if (!string.IsNullOrWhiteSpace(updated.Tytul))
            todo.Tytul = updated.Tytul;

        todo.Zrobione = updated.Zrobione;

        return Ok(todo);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo == null)
            return NotFound();

        todos.Remove(todo);
        return NoContent();
    }
}