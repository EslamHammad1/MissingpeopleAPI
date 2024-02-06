using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Test_1.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public byte[] Image { get; set; } 

       }
    }

