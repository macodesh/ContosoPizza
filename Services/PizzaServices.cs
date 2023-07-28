// Importa o namespace ContosoPizza.Models, que contém a definição da classe Pizza.
using ContosoPizza.Models;

// Define o namespace ContosoPizza.Services para o arquivo atual.
namespace ContosoPizza.Services
{
    // Cria uma classe estática chamada PizzaService.
    public static class PizzaService
    {
        // Cria uma lista estática de objetos do tipo Pizza para armazenar as pizzas.
        static List<Pizza> Pizzas { get; }
        // Cria uma variável estática nextId e atribui o valor 3.
        static int nextId = 3;

        // Construtor estático da classe PizzaService.
        // Inicializa a lista Pizzas com duas instâncias de Pizza usando a sintaxe de inicialização de coleções.
        static PizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Clássica Italiana", IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Vegetariana", IsGlutenFree = true }
            };
        }

        // Método estático que retorna todas as pizzas contidas na lista.
        public static List<Pizza> GetAll() => Pizzas;

        // Método estático que retorna uma pizza com base no ID fornecido ou null se não encontrada.
        public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        // Método estático que adiciona uma pizza à lista, atribuindo um novo ID a ela.
        public static void Add(Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        // Método estático que remove uma pizza com base no ID fornecido, se encontrada.
        public static void Delete(int id)
        {
            // Verifica se a pizza com o ID fornecido existe na lista.
            // Se não existir, retorna sem fazer nada.
            var pizza = Get(id);
            if (pizza is null) return;
            // Remove a pizza da lista.
            Pizzas.Remove(pizza);
        }

        // Método estático que atualiza as informações de uma pizza existente na lista.
        public static void Update(Pizza pizza)
        {
            // Procura o índice da pizza com o ID fornecido na lista.
            var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
            // Se a pizza não for encontrada (índice igual a -1), retorna sem fazer nada.
            if (index == -1) return;
            // Atualiza a pizza na lista com base no índice encontrado.
            Pizzas[index] = pizza;
        }
    }
}
