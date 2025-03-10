using System.ComponentModel.DataAnnotations;

namespace JarekWebAPI.WebApi;

public class Object2D
{
    public Guid Id { get; set; }
    [Required] 
    public Guid EnvironmentId { get; set; }
    [Required]
    public string PrefabId { get; set; }
    [Required]
    public float PositionX { get; set; }
    [Required]
    public float PositionY { get; set; }
    [Required]
    public float ScaleX { get; set; }
    [Required]
    public float ScaleY { get; set; }
    [Required]
    public int SortingLayer { get; set; }
}
