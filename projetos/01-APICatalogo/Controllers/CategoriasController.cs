using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly CatalogoDbContext _context;

    public CategoriasController(CatalogoDbContext context)
    {
        _context = context;
    }


    [HttpGet] // Método que RETORNA/LÊ todos as Categorias da tabela
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var categorias = _context.Categorias.AsNoTracking().Take(5).ToList();

            if (categorias is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpGet("Produtos")] // Método que RETORNA/LÊ todos as Categorias e Produtos da tabela
    public ActionResult<IEnumerable<Categoria>> GetCategoriasEProdutos()
    {
        try
        {
            var categorias = _context.Categorias.AsNoTracking().
                Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();

            if (categorias is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")] // Método que RETORNA/LÊ uma Categoria pelo id na tabela
    public ActionResult Get(int id)
    {
        try
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria do id:{id} não encontrado");
            }

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpPost] // Método que CRIA uma Categoria na tabela
    public ActionResult Post(Categoria categoria)
    {
        try
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpPut("{id:int}")] // Método que ATUALIZA uma Categoria pelo id na tabela
    public ActionResult Put(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest($"Categoria do id:{id} não encontrado");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpDelete("{id:int}")] // Método que DELETA uma Categoria da tabela
    public ActionResult Delete(int id)
    {
        try
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria do id:{id} não encontrado");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }
}