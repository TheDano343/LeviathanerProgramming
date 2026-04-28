using LeviathanerProgramming.Models;
using Microsoft.EntityFrameworkCore;
using Programming_Courses.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Lenguaje> Lenguajes { get; set; }
    public DbSet<RoadMap> RoadMaps { get; set; }
    public DbSet<NivelDificultad> NivelDificultades { get; set; }
    public DbSet<TipoRutaAprendizaje> TipoRutaAprendizajes { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Lenguaje>().ToTable("Lenguaje");
        modelBuilder.Entity<RoadMap>().ToTable("RoadMap");
        modelBuilder.Entity<NivelDificultad>().ToTable("NivelDificultad");
        modelBuilder.Entity<TipoRutaAprendizaje>().ToTable("TipoRutaAprendizaje");
    }
}
