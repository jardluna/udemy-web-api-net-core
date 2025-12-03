using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public CategoriasController(IUnitOfWork uow)
    {
        _uow  = uow;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categoria = _uow.CategoriaRepository.GetAll();
        return Ok(categoria);
    }


    [HttpGet("produtos/")]
    public ActionResult<IEnumerable<Categoria>> GetProdutos()
    {
        var categoria = _uow.CategoriaRepository.GetProdutosAll();
        return Ok(categoria);
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
        if (categoria == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(categoria);
    }


    [HttpPost]
    public ActionResult<Categoria> Post(Categoria categoria)
    {
        if (categoria == null) { return BadRequest("Dados inválidos"); }
        _uow.CategoriaRepository.Create(categoria);
        _uow.Commit();
        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }


    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId) { return BadRequest("Dados inválidos"); }
        _uow.CategoriaRepository.Update(categoria);
        _uow.Commit();
        return Ok(categoria);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> Delete(int id)
    {
        var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
        if (categoria == null) { return NotFound($"Id:{id} não encontrado"); }
        _uow.CategoriaRepository.Delete(categoria);
        _uow.Commit();
        return Ok(categoria);
    }
}