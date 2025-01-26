using CadastroApi.Data; // Importa o DbContext
using CadastroApi.Models; // Importa o modelo Cadastro
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/cadastro
        [HttpPost]
        public IActionResult Create(Cadastro cadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Cadastros.Add(cadastro);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = cadastro.Id }, cadastro);
        }

        // GET: api/cadastro
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Cadastros.ToList());
        }

        // GET: api/cadastro/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cadastro = _context.Cadastros.Find(id);
            if (cadastro == null)
                return NotFound();

            return Ok(cadastro);
        }

        // PUT: api/cadastro/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Cadastro cadastro)
        {
            if (id != cadastro.Id)
                return BadRequest();

            if (!_context.Cadastros.Any(c => c.Id == id))
                return NotFound();

            _context.Entry(cadastro).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/cadastro/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cadastro = _context.Cadastros.Find(id);
            if (cadastro == null)
                return NotFound();

            _context.Cadastros.Remove(cadastro);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
