using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pokelist.Model
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> Get();
        Task<Pokemon> Get(int id);
    }
}