using System.Collections.Generic;
using System.Threading.Tasks;

using pokelist.Model;

namespace pokelist.Services
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> Get();
        Task<Pokemon> Get(int id);
    }
}