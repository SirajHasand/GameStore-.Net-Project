using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class GameStorecontext(DbContextOptions<GameStorecontext>options) 
:DbContext(options)
{
    public DbSet<Game> Games { get; set; }
    
    public DbSet<Genre> Genres { get; set; }


}
