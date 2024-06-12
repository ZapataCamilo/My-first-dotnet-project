using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data;

public class MenuContext: DbContext
{
    public MenuContext(DbContextOptions<MenuContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishIngredient>().HasKey(di => new
        {
            di.DishId,
            di.IngredientId
        });
        modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish).WithMany(di => di.DishIngredient).HasForeignKey(d => d.DishId);
        modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient).WithMany(di => di.DishIngredients).HasForeignKey(i => i.IngredientId);

        modelBuilder.Entity<Dish>().HasData(
            new Dish { Id = 1, Name = "Margheritta", Price = 7.50, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSWwNz-YV88e3LFP6iisBcZT-loky1VotV4aQ&s" }
            );
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Tomato Sauce" },
            new Ingredient { Id = 2, Name = "Mozzarella" }
            );
        modelBuilder.Entity<DishIngredient>().HasData(
            new DishIngredient { DishId = 1, IngredientId = 1 },
            new DishIngredient { DishId = 1, IngredientId = 2 }
            );
        
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<DishIngredient> DishIngredients { get; set; }
}