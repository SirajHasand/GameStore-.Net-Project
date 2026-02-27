using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GameStore.Api.Dtos;

public record class CreateGameDto
(
    [Required] 
    [StringLength(100, MinimumLength = 3)]
    string Name,
    [Required] [StringLength(100, MinimumLength = 2)] string Genre,
    [Required] [Range(0, 1000)] decimal Price,
    [Required] DateOnly ReleaseDate
);