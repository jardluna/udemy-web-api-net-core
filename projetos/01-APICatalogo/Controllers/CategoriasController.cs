using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_APICatalogo.Controllers;

[Route("api/[controller]")]
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
        var categorias = _context.Categorias.ToList();

        if (categorias is null)
        {
            return NotFound();
        }

        return Ok(categorias);
    }

    [HttpGet("Produtos")] // Método que RETORNA/LÊ todos as Categorias e Produtos da tabela
    public ActionResult<IEnumerable<Categoria>> GetCategoriasEProdutos()
    {
        var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

        if (categorias is null)
        {
            return NotFound();
        }

        return Ok(categorias);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")] // Método que RETORNA/LÊ uma Categoria pelo id na tabela
    public ActionResult Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }

        return Ok(categoria);
    }

    [HttpPost] // Método que CRIA uma Categoria na tabela
    public ActionResult Post(Categoria categoria)
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

    [HttpPut("{id:int}")] // Método que ATUALIZA uma Categoria pelo id na tabela
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")] // Método que DELETA uma Categoria da tabela
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}
