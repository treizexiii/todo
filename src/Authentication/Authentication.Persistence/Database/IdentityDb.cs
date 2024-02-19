using Authentication.Domain.Entities;
using Authentication.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tools.TransactionManager;

namespace Authentication.Persistence.Database;

public class IdentityDb : DbContext, IDbContext
{
    public IdentityDb(DbContextOptions<IdentityDb> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Claim> Claims { get; set; } = null!;
    public DbSet<UserClaim> UserClaims { get; set; } = null!;
    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<Secret> Secrets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username).IsRequired();
        });

        modelBuilder.Entity<Claim>(builder =>
        {
            builder.HasIndex(x => new { x.Name, x.Type }).IsUnique();
            builder.Property(x => x.Type).HasConversion(new EnumToStringConverter<ClaimType>());
        });

        modelBuilder.Entity<Role>(builder =>
        {
            builder.Property(x => x.Name).HasConversion(new EnumToStringConverter<RoleEnum>());
            builder.HasData(RoleType.Roles);

        });

        modelBuilder.Entity<Token>(builder =>
        {
            builder.Property(x => x.Type).HasConversion(new EnumToStringConverter<TokenType>());
        });

        base.OnModelCreating(modelBuilder);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await base.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    public IDbContextTransaction? CurrentTransaction => base.Database.CurrentTransaction;
}