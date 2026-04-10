using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokedexApi.Models;

namespace PokedexApi.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;
        //injeção de dependência do HttpClient para realizar as requisições HTTP à PokeApi
        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }//faz uma requisição http e adiciona a variação privada única

        public async Task<List<string?>?> GetAllPokemon(int limit)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon?limit={limit}"); //realiza o chamado à PokeApi para obter a lista de todos os Pokémon disponíveis, limitando a {limit} resultados

            if (!response.IsSuccessStatusCode) return null; //se diferente de SUCESSO = null

            var content = await response.Content.ReadAsStringAsync(); //lê o conteúdo da resposta como string

            dynamic data = JsonConvert.DeserializeObject<dynamic>(content); //desserializa o conteúdo JSON para um objeto dinâmico para acessar os dados dos Pokémon de forma flexível

            var pokemonList = new List<string>(); //cria uma lista para armazenar os nomes dos Pokémon

            foreach (var item in data.results)
            {
                var pokemon = item.name.ToString(); //para cada Pokémon na lista de resultados, chama o método GetPokemon para obter os detalhes do Pokémon com base no nome
                if (pokemon != null) pokemonList.Add(pokemon); //se o Pokémon for encontrado, adiciona à lista de Pokémon
            }

            return pokemonList; //retorna a lista completa de Pokémon encontrados
        }

        public async Task<PokemonResponse?> GetPokemonByName(string name)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}"); //realiza o chamado à PokeApi para obter os dados do Pokémon com base no nome fornecido

            if (!response.IsSuccessStatusCode) return null; //se diferente de SUCESSO = null

            var content = await response.Content.ReadAsStringAsync(); //lê o conteúdo da resposta como string   

            dynamic data = JsonConvert.DeserializeObject<dynamic>(content); //desserializa o conteúdo JSON para um objeto dinâmico para acessar os dados do Pokémon de forma flexível

            var pokemon = new PokemonResponse
            {
                Id = data.id,
                Name = data.name,
                Height = data.height,
                Types = new List<string>(),
                Abilities = new List<string>()
            }; //cria um objeto PokemonResponse e preenche os campos Name e Image com os dados obtidos da PokeApi, além de inicializar a lista de tipos vazia

            foreach (var type in data.types)
            {
                pokemon.Types.Add((string)type.type.name); //adiciona os tipos do Pokémon à lista de tipos
            }

            foreach (var ability in data.abilities)
            {
                pokemon.Abilities.Add((string)ability.ability.name); //adiciona as habilidades do Pokémon à lista de habilidades
            }

            return pokemon; //retorna o objeto PokemonResponse preenchido com os dados do Pokémon
        }

        public async Task<PokemonResponse?> GetPokemonById(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}"); //realiza o chamado à PokeApi para obter os dados do Pokémon com base no ID fornecido

            if (!response.IsSuccessStatusCode) return null; //se diferente de SUCESSO = null

            var content = await response.Content.ReadAsStringAsync(); //lê o conteúdo da resposta como string   

            dynamic data = JsonConvert.DeserializeObject<dynamic>(content); //desserializa o conteúdo JSON para um objeto dinâmico para acessar os dados do Pokémon de forma flexível

            var pokemon = new PokemonResponse
            {
                Id = data.id,
                Name = data.name,
                Height = data.height,
                Types = new List<string>(),
                Abilities = new List<string>()
            }; //cria um objeto PokemonResponse e preenche os campos Name e Image com os dados obtidos da PokeApi, além de inicializar a lista de tipos vazia

            foreach (var type in data.types)
            {
                pokemon.Types.Add((string)type.type.name); //adiciona os tipos do Pokémon à lista de tipos
            }

            foreach (var ability in data.abilities)
            {
                pokemon.Abilities.Add((string)ability.ability.name); //adiciona as habilidades do Pokémon à lista de habilidades
            }

            return pokemon; //retorna o objeto PokemonResponse preenchido com os dados do Pokémon
        }

        public async Task<List<string>?> GetPokemonByType(string type)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/type/{type.ToLower()}"); //realiza o chamado à PokeApi para obter os dados do Pokémon com base no tipo fornecido

            if (!response.IsSuccessStatusCode) return null; //se diferente de SUCESSO = null

            var content = await response.Content.ReadAsStringAsync(); //lê o conteúdo da resposta como string   

            dynamic data = JsonConvert.DeserializeObject<dynamic>(content); //desserializa o conteúdo JSON para um objeto dinâmico para acessar os dados do Pokémon de forma flexível

            var pokemon = new List<string>(); //cria uma lista para armazenar os nomes dos Pokémon do tipo especificado

            foreach (var pokemonData in data.pokemon)
            {
                pokemon.Add((string)pokemonData.pokemon.name); //adiciona os nomes dos Pokémon do tipo especificado à lista
            }

            return pokemon; //retorna o objeto PokemonResponse preenchido com os dados do Pokémon
        }
    }
}