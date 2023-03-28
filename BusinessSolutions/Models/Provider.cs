using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Models
{
    public class Provider
    {
        [Key]  public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")] public string Name { get; set; }
    }
}
