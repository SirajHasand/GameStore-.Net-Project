using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints 
{ 
const string EndpointName = "GetGameById"; // this is used to give a name to the route, so we can use it in the CreatedAtRoute method in the POST endpoint

     private static readonly List<GameDto> games = new List<GameDto>
 {
     new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99m, new DateOnly(2017, 3, 3)),
     new GameDto(2, "God of War", "Action-Adventure", 49.99m, new DateOnly(2018, 4, 20)),
     new GameDto(3, "Red Dead Redemption 2", "Action-Adventure", 39.99m, new DateOnly(2018, 10, 26))
 };
    public static void MapGamesEndpoints(this WebApplication app)
    {
        // we can use MapGroup to group our endpoints, this is useful for versioning and also for adding common tags to our endpoints
        var Group = app.MapGroup("/games").WithTags("Games");
        //GET  /games
        Group.MapGet("/", () => games);
 
        // GET /games/1
        Group.MapGet("/{id}", (int id) =>{
            var game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound($"Game with id {id} not found");

            }
            return Results.Ok(game);})  
        .WithName(EndpointName); // this is used to give a name to the route, so we can use it in the CreatedAtRoute method in the POST endpoint
 
        //POST /games
        Group.MapPost("/", (CreateGameDto newGame) =>
        {
           if (games.Any(game => game.Name == newGame.Name))
    {
        return Results.Conflict($"A game with the name '{newGame.Name}' already exists.");
    }
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
   
        });
 
        // PUT /games/1 
        Group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            int index = games.IndexOf(game);
            games[index] = new GameDto(
                game.Id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
 
        }); 
        // DELETE /games/1
        Group.MapDelete("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            games.Remove(game);
            return Results.NoContent();
 
        });
    }
}
 