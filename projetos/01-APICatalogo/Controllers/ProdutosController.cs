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
    public async Task<ActionResult<IEnumerable<Produto>>> GetAsync()
    {
        try
        {
            //var produtos = _context.Produtos.AsNoTracking().Take(5).ToList(); // Take() é um método que pega apenas uma quantidade 
                                                                                // limitada de itens de uma lista, coleção ou consulta ao banco.

            var produtos = await _context.Produtos.AsNoTracking().ToListAsync(); // AsNoTracking() melhora a performace do código
                                                                                 // quando há apenas leitura de dados

            if (produtos is null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produtos);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpGet("{id:int}", Name = "ObterProduto")] // Método que RETORNA/LÊ um Produto pelo id na tabela
    public async Task<ActionResult<Produto>> GetAsync(int id)
    {
        try
        {
            var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound($"Produto do id ({id}) não encontrado");
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
    public async Task<ActionResult<Produto>> PostAsync(Produto produto)
    {
        try
        {
            if (produto is null)
            {
                return BadRequest();
            }

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

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
    public async Task<ActionResult<Produto>> PutAsync(int id, Produto produto)
    {
        try
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest($"Produto do id ({id}) não encontrado");
            }

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpDelete("{id:int}")] // Método que DELETA um Produto da tabela
    public async Task<ActionResult<Produto>> DeleteAsync(int id)
    {
        try
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound($"Produto do id ({id}) não encontrado");
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(produto);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }
}