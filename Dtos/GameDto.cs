   namespace GameStore.Api.Dtos;


// simply we can say that a DTO is a simple object that is used to
// transfer data between different parts of an application,
public record class GameDto (
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate

);