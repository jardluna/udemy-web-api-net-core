using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public ProdutosController(IUnitOfWork uow)
    {
        _uow = uow;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produto = _uow.ProdutoRepository.GetAll();
        return Ok(produto);
    }


    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(produto);
    }


    [HttpGet("categoria/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetProdutoPorCategoria(int id)
    {
        var produto = _uow.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(produto);
    }


    [HttpPost]
    public ActionResult<Produto> Post(Produto produto)
    {
        if (produto == null) { return BadRequest("Dados inválidos"); }
        _uow.ProdutoRepository.Create(produto);
        _uow.Commit();
        return new CreatedAtRouteResult("ObterProduto", new { id = produto.CategoriaId }, produto);
    }


    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) { return BadRequest($"Dados inválidos"); }
        _uow.ProdutoRepository.Update(produto);
        _uow.Commit();
        return Ok(produto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id)
    {
        var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        _uow.ProdutoRepository.Delete(produto);
        _uow.Commit();
        return Ok(produto);
    }
}