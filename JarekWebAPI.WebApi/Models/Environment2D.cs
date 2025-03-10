using System.ComponentModel.DataAnnotations;

namespace JarekWebAPI.WebApi;

public class Environment2D
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public float MaxHeight { get; set; }
    [Required]
    public float MaxLength { get; set; }
}
