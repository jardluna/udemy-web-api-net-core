using _01_APICatalogo.Domain;
using _01_APICatalogo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IRepository<Produto> _repository;
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IRepository<Produto> repository, IProdutoRepository produtoRepository)
    {
        _repository = repository;
        _produtoRepository = produtoRepository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produto = _repository.GetAll();
        return Ok(produto);
    }


    [HttpGet("produtos/{id:int}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produto = _produtoRepository.GetProdutosPorCategoria(id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(produto);
    }


    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.GetById(p => p.ProdutoId == id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        return Ok(produto);
    }


    [HttpPost]
    public ActionResult<Produto> Post(Produto produto)
    {
        if (produto == null) { return BadRequest("Dados inválidos"); }
        _repository.Create(produto);
        return new CreatedAtRouteResult("ObterProduto", new { id = produto.CategoriaId }, produto);
    }


    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId) { return BadRequest($"Dados inválidos"); }
        _repository.Update(produto);
        return Ok(produto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id)
    {
        var produto = _repository.GetById(p => p.ProdutoId == id);
        if (produto == null) { return NotFound($"Id:{id} não encontrado"); }
        _repository.Delete(produto);
        return Ok(produto);
    }
}