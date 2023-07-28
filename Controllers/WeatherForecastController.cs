using Microsoft.AspNetCore.Mvc;

// Declaração de namespace.
namespace ContosoPizza.Controllers;

// Indica que este é controlador de API.
// Permite a vinculação automática dos parâmetros de solicitação HTTP para os parâmetros do método.
// Habilita comportamentos adicionais que facilitam a criação de APIs Web.
[ApiController]
// Define a rota do controlador.
// [controller] é uma variável de espaço reservada que será substituída pelo nome do controlador.
// O token "controller" é substituído pelo nome do controlador, sem diferenciação de maiúsculas e minúsculas.
// A palavra "Controller" não faz parte do nome da rota gerada por esse token.
// Ex: aqui é gerada a rota /weatherforecast.
// A rota poderia conter caracteres estáticos, como api/[controller].
// Neste caso, a rota seria /api/weatherforecast.
[Route("[controller]")]
// Herda da classe ControllerBase (classe base para controladores em ASP.NET Core).
public class WeatherForecastController : ControllerBase
{
    // Matriz de strings contendo diferentes descrições climáticas.
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // Construtor que recebe uma instância como parâmetro, usada para realizar o registro de informações.
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    // Este atributo encaminha solicitações Get para o método "public IEnumerable<WeatherForecast> Get() {}".
    [HttpGet(Name = "GetWeatherForecast")]
    // O método Get() é um endpoint HTTP GET que retorna uma coleção de WeatherForecast.
    // Neste caso, é uma lista de previsões do tempo geradas aleatoriamente para os próximos 5 dias.
    public IEnumerable<WeatherForecast> Get()
    {
        // Cria um range de números inteiros de 1 a 5 e, em seguida, realiza uma seleção para cada número.
        // Depois cria uma nova instância da classe WeatherForecast e inicializa sua propriedade Date.
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            // A data é definida como a data e hora atual (DateTime.Now) mais o valor do índice.
            Date = DateTime.Now.AddDays(index),
            // As previsões são geradas usando a classe Random.
            // A classe Random é uma classe estática que fornece métodos para gerar números aleatórios.
            // O método Next() da classe Random retorna um número inteiro no intervalo especificado (-20 a 54).
            // O valor retornado é a temperatura em graus Celsius.
            TemperatureC = Random.Shared.Next(-20, 55),
            // Escolhe aleatoriamente um resumo do clima da matriz Summaries.
            // O método Next() da classe Random é usado para gerar um índice aleatório dentro dos limites do tamanho da matriz.
            // O resumo é selecionado aleatoriamente com base no índice gerado.
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        // Converte o resultado da seleção em uma matriz de objetos WeatherForecast.
        .ToArray();
    }
}
