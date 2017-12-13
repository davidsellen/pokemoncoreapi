using System.Collections.Generic;

namespace pokelist.Model
{
    public class Pokemon 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public List<Ability> Abilities { get; set; }
        public List<PokeType> Types { get; set; }
    }
}