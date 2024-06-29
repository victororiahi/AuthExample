using AuthExample.domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthExample.domain
{
    public class AuthExampleDbContext : IdentityDbContext<User, Role, int>
    {
        public AuthExampleDbContext(DbContextOptions<AuthExampleDbContext> options) : base(options)

        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var tableNameWithAspNet = builder.Model.GetEntityTypes().Where(e => e.GetTableName().StartsWith("AspNet")).ToList();
            tableNameWithAspNet.ForEach(x =>
            {
                x.SetTableName(x.GetTableName().Substring(6));
            });

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.FirstName).HasColumnType("varchar(100)").IsRequired(false);
            builder.Entity<User>().Property(p => p.LastName).HasColumnType("varchar(100)").IsRequired(false);
            builder.Entity<User>().Property(f => f.PhoneNumber).HasMaxLength(20).IsRequired(false);
            //builder.Entity<User>().Property(f => f.Email).HasMaxLength(50).HasDefaultValue("abc@xyz.com");
            builder.Entity<User>().Property(p => p.PasswordHash).HasColumnName("Password").HasColumnType("varchar(250)");
            builder.Entity<User>().Property(p => p.PhoneNumber).HasColumnType("varchar(15)");
            builder.Entity<User>().Property(p => p.Email).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.NormalizedEmail).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.UserName).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.NormalizedUserName).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.ConcurrencyStamp).HasMaxLength(250).HasColumnType("varchar");
            builder.Entity<User>().Property(p => p.SecurityStamp).HasColumnType("varchar(250)");
            builder.Entity<User>().Property(p => p.FirstName).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.LastName).HasColumnType("varchar(100)");
            builder.Entity<User>().Property(p => p.AccessFailedCount).HasDefaultValue(5);
            builder.Entity<User>().Property(p => p.EmailConfirmed).HasDefaultValue(false);
            builder.Entity<User>().Property(p => p.LockoutEnabled).HasDefaultValue(false);
            builder.Entity<User>().Property(p => p.PhoneNumberConfirmed).HasDefaultValue(false);
            builder.Entity<User>().Property(p => p.TwoFactorEnabled).HasDefaultValue(false);

            builder.Entity<User>().HasData(new User
            {
                Id = 1,
                FirstName = "Victor",
                LastName = "Oriahi",
                Email = "abc@xyz.com",
                NormalizedEmail = "abc@xyz.com".ToUpper(),
                UserName = "abc@xyz.com",
                NormalizedUserName = "abc@xyz.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PhoneNumber = "080123456789",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                AccessFailedCount = 1,
                PasswordHash = "AQAAAAEAACcQAAAAEHz9jeDAGD5NrInBBafBqFjW3XbnNG4w08PuNblIMvwdU1kzpGQd8mX3ca28HlBPkA=="
            });

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            });
        }
    }
}
