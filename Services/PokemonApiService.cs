using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pokelist.Core;
using pokelist.Model;

namespace pokelist.Services
{
    public class PokemonApiService : IPokemonService
    {
        const string API_URL = "https://pokeapi.co/api/v2/pokemon/";
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await ApiClient.Get<IEnumerable<Pokemon>>(API_URL, (data) =>
            {
                JObject dymanicData = JObject.Parse(data);
                return dymanicData["results"].Select(item => new Pokemon
                {
                    Id = ReadIdFromUrl((string)item["url"]),
                    Name = (string)item["name"]
                });
            });
        }

        private int ReadIdFromUrl(string url)
        {
            var uri = new System.Uri(url);
            var lastSegment = uri.Segments[uri.Segments.Length - 1];
            var id = Regex.Match(lastSegment, "\\d+").Value;
            return int.Parse(id);
        }

        public async Task<Pokemon> Get(int id)
        {
            return await ApiClient.Get<Pokemon>($"{API_URL}{id}", (data) =>
            {
                JObject item = JObject.Parse(data);
                return new Pokemon
                {
                    Id = (int)item["id"],
                    Name = (string)item["name"],
                    Height = (int)item["height"],
                    Weight = (int)item["weight"],
                    Abilities = ReadAbilities(item["abilities"]),
                    Types = ReadTypes(item["types"])
                };
            });
        }

        private List<PokeType> ReadTypes(JToken jToken)
        {
            return jToken.Select(item => new PokeType
            {
                Slot = (int)item["slot"],
                Name = (string)item["type"]["name"]
            }).ToList();
        }

        private List<Ability> ReadAbilities(JToken jToken)
        {
            return jToken.Select(item => new Ability
            {
                Slot = (int)item["slot"],
                IsHidden = (bool)item["is_hidden"],
                Name = (string)item["ability"]["name"]
            }).ToList();
        }

    }
}