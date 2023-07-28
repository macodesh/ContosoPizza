using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

// Deixa implícito que "pizza" será incluído no corpo da solicitação.
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    { }

    // Get all

    // Esse atributo indica que o método responde apenas ao HTTP GET.
    [HttpGet]
    // Retorna uma instância de ActionResult do tipo List<Pizza>.
    // ActionResult é a classe base para todos os resultados de ações no ASP.NET Core.
    // Consulta o Service e retorna os dados em formato JSON.
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    // Get by Id

    // Esse atributo indica uma nova rota GET que deve incluir um parâmetro ID na rota.
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        // Consulta o banco de dados e verifica a existência da pizza.
        var pizza = PizzaService.Get(id);
        // Caso não exista, retorna um status 404 (Not Found).
        if (pizza == null) return NotFound();
        // Caso exista, retorna a pizza buscada e o status 200 OK (que está implícito).
        return pizza;
    }

    // Post

    // Atributo que mapeará solicitações HTTP POST enviadas para /pizza usando o método Create().
    [HttpPost]
    // Esse método retorna uma resposta IActionResult caso seja bem-sucedido.
    // Caso a ação falhe, o status retornado é 400.
    public IActionResult Create(Pizza pizza)
    {
        // Salva a pizza e retorna o resultado.
        PizzaService.Add(pizza);
        // IActionResult retorna o status 201 (Created) e o id da pizza recém-criada.
        // nameof evita hard-coding do nome da ação.
        // CreatedAtAction usa o nome da ação para gerar um cabeçalho de resposta HTTP location com uma URL para a pizza recém-criada.
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // Put

    // Cria uma rota PUT que deve incluir o ID como parâmetro
    [HttpPut("{id}")]
    // Os métodos retornam IActionResult porque o tipo de retorno ActionResult não é conhecido até o runtime.
    // Os métodos BadRequest, NotFound e NoContent retornam os tipos BadRequestResult, NotFoundResult e NoContentResult respectivamente.
    public IActionResult Update(int id, Pizza pizza)
    {
        // Retorna um status 400 (Bad Request) caso o id não exista ou o corpo da solicitação seja inválido.
        if (id != pizza.Id) return BadRequest();

        // Atualiza a pizza e retorna o resultado.
        var existingPizza = PizzaService.Get(id);

        // Retorna 404 caso a pizza não exista no BD.
        if (existingPizza is null) return NotFound();

        PizzaService.Update(pizza);
        // Retorna uma IActionResult com o status 204 (No Content) caso seja bem-sucedido.
        return NoContent();
    }

    // Delete
    // Retorna uma IActionResult com o status 204 caso seja bem-sucedido.
    // Retorna um status 404 caso o id não exista.
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null) return NotFound();

        // Exclui a pizza e retorna o resultado.
        PizzaService.Delete(id);
        return NoContent();
    }
}
