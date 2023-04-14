using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fishnice.Models;

public class Fish
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [Display(Name = "Catch Date")]
    [DataType(DataType.Date)]
    public DateTime CatchDate { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }
}