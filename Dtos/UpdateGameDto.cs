using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto (
   [Required] 
    [StringLength(100, MinimumLength = 3)]
    string Name,
    [Required] [StringLength(100, MinimumLength = 2)] string Genre,
    [Required] [Range(0, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);