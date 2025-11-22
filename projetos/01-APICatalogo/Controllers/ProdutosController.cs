using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly CatalogoDbContext _context;

    public ProdutosController(CatalogoDbContext context)
    {
        _context = context;
    }


    [HttpGet] // Método que RETORNA/LÊ todos os Produtos da tabela
    public ActionResult<IEnumerable<Produto>> Get()
    {
        try
        {
            var produtos = _context.Produtos.AsNoTracking().Take(5).ToList();

            if (produtos is null)
            {
                return NotFound("Produto não encontrado");
            }

            return produtos;
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpGet("{id:int}", Name = "ObterProduto")] // Método que RETORNA/LÊ um Produto pelo id na tabela
    public ActionResult Get(int id)
    {
        try
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound($"Produto do id:{id} não encontrado");
            }

            return Ok(produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpPost] // Método que CRIA um Produto na tabela
    public ActionResult Post(Produto produto)
    {
        try
        {
            if (produto is null)
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpPut("{id:int}")] // Método que ATUALIZA um Produto pelo id na tabela
    public ActionResult Put(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest($"Produto do id:{id} não encontrado");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpDelete("{id:int}")] // Método que DELETA um Produto da tabela
    public ActionResult Delete(int id)
    {
        try
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound($"Produto do id:{id} não encontrado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }
}