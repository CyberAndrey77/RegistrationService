using Microsoft.EntityFrameworkCore;
using RegistrationService.Models;
using System.Security.Cryptography;

namespace RegistrationService.Services
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Список пользователей из бд.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Список токенов из бд.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Задание значений таблицы при создании бд.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            byte[] passwordHash;
            using (SHA512 shaM = new SHA512Managed())
            {
                passwordHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes("12345"));
            }
            modelBuilder.Entity<User>().HasData(
                  new User
                  {
                      Id = 1,
                      Name = "Андрей",
                      Email = "lapardin.andrey@mail.ru",
                      Password = passwordHash,
                      Role = UserRole.Admin.ToString(),
                      CreationTime = DateTime.Now.ToString("dd.MM.yyyy")
                  }
            );
        }
    }
}
