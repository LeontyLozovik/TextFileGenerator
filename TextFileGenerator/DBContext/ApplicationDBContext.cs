using Microsoft.EntityFrameworkCore;
using TextFileGenerator.Models;

namespace TextFileGenerator.DBContext
{
    public class ApplicationDBContext : DbContext     //Структура базы данных
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<DBModel> Record { get; set; }
    }
}
