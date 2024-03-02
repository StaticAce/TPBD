using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProiectTPBD
{
    internal class ProiectDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public ProiectDbContext()
        {
            OnConfiguring(new DbContextOptionsBuilder());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=WALTER;Database=ProiectTPBD;Trusted_Connection=True;TrustServerCertificate=True");
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }

        public string Nume { get; set; }

        public int Salar { get; set; }
    }
}
