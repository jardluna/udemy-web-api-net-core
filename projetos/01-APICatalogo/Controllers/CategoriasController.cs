using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;

    public CategoriasController(IRepository<Categoria> repository)
    {
        _repository = repository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categoria = _repository.GetAll();
        return Ok(categoria);
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.GetById(c => c.CategoriaId == id);
        if (categoria == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(categoria);
    }


    [HttpPost]
    public ActionResult<Categoria> Post(Categoria categoria)
    {
        if (categoria == null) { return BadRequest("Dados inválidos"); }
        _repository.Create(categoria);
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }


    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId) { return BadRequest("Dados inválidos"); }
        _repository.Update(categoria);
        return Ok(categoria);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> Delete(int id)
    {
        var categoria = _repository.GetById(c => c.CategoriaId == id);
        if (categoria == null) { return NotFound($"Id:{id} não encontrado"); }
        _repository.Delete(categoria);
        return Ok(categoria);
    }
}