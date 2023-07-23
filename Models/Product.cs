using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Antique_Store_API.Models;
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal price { get; set; }
    public string? url { get; set; } = string.Empty;
    public string tag { get; set; }
}