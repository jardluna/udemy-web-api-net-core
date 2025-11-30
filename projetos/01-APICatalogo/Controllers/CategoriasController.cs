using _01_APICatalogo.Context;
using _01_APICatalogo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace _01_APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly CatalogoDbContext _context;
    private readonly IConfiguration _configuration;

    public CategoriasController(CatalogoDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    [HttpGet] // Método que RETORNA/LÊ todos as Categorias da tabela
    public async Task<ActionResult<IEnumerable<Categoria>>> GetAsync()
    {
        try
        {
            //var categorias = _context.Categorias.AsNoTracking().Take(5).ToList(); // Take() é um método que pega apenas uma quantidade 
            // limitada de itens de uma lista, coleção ou consulta ao banco.

            var categorias = await _context.Categorias.AsNoTracking().ToListAsync(); // AsNoTracking() melhora a performace do código
                                                                                     // quando há apenas leitura de dados

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

    /*Lendo arquivos do appsettings.json
     * 
     * [HttpGet("LerArquivoConfiguracao")]
    public string GetValores()
    {
        var valor1 = _configuration["chave1"];
        var valor2 = _configuration["chave2"];

        var secao1 = _configuration["secao1:chave2"];

        return $"Chave1 = {valor1} \nChave2 = {valor2} \nSeção1 => Chave2 = {secao1}";
    }*/

    [HttpGet("Produtos")] // Método que RETORNA/LÊ todos as Categorias e Produtos da tabela
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasEProdutosAsync()
    {
        try
        {
            //var categorias = _context.Categorias.AsNoTracking().
                //Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList(); // Where() é um filtro que mostra apenas
                                                                                    // os itens que atendem as condições

            var categorias = await _context.Categorias.AsNoTracking().
                Include(p => p.Produtos).ToListAsync();

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
    public async Task<ActionResult<Categoria>> GetAsync(int id)
    {
        try
        {
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria do id ({id}) não encontrada");
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
    public async Task<ActionResult<Categoria>> PostAsync(Categoria categoria)
    {
        try
        {
            if (categoria is null)
            {
                return BadRequest();
            }

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

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
    public async Task<ActionResult<Categoria>> PutAsync(int id, Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest($"Categoria do id ({id}) não encontrada");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }


    [HttpDelete("{id:int}")] // Método que DELETA uma Categoria da tabela
    public async Task<ActionResult<Categoria>> DeleteAsync(int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria do id ({id}) não encontrada");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
        }
    }
}