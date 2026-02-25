using GameStore.Api.Dtos;
const string EndpointName = "GetGameById"; // this is used to give a name to the route, so we can use it in the CreatedAtRoute method in the POST endpoint

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// this is just a sample data to test our API, in real world application we will get this data from database

 List<GameDto> games = new List<GameDto>
 {
     new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99m, new DateOnly(2017, 3, 3)),
     new GameDto(2, "God of War", "Action-Adventure", 49.99m, new DateOnly(2018, 4, 20)),
     new GameDto(3, "Red Dead Redemption 2", "Action-Adventure", 39.99m, new DateOnly(2018, 10, 26))
 };

//GET  /games
app.MapGet("/games", () => games);

// GET /games/1
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(EndpointName); // this is used to give a name to the route, so we can use it in the CreatedAtRoute method in the POST endpoint

//POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    return Results.CreatedAtRoute(EndpointName, new { id = game.Id }, game);
    // CreatedAtRoute is used to return a 201 Created response with a Location header 
    // that contains the URL of the newly created resource,
    //  and the body of the response contains the newly created resource itself.

    // The first parameter is the name of the route that we want to use to generate the URL of the newly created resource,
    // the second parameter is an anonymous object that contains the route parameters that we want to use
    // to generate the URL of the newly created resource, in this case we want to use the id of the newly created game,
    // and the third parameter is the body of the response, which is the newly created game

    

});

// PUT /games/1 
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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
app.MapDelete("/games/{id}", (int id) =>
{
    var game = games.Find(game => game.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    games.Remove(game);
    return Results.NoContent();
});



app.Run();
