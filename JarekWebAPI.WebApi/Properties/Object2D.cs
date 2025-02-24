using System.ComponentModel.DataAnnotations;

namespace JarekWebAPI.WebApi;

public class Object2D
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int PrefabId { get; set; }
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
