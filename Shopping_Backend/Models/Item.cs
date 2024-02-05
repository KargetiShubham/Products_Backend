using System.ComponentModel.DataAnnotations;

namespace Shopping_Backend.Models
{
    public class Item
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }
    }
}
