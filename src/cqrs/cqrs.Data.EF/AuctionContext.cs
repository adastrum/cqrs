using System;
using cqrs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace cqrs.Data.Sql.EF
{
    public class AuctionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        public AuctionContext() { }

        public AuctionContext(DbContextOptions<AuctionContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasKey(y => y.Id);
                user.Property(x => x.Name).IsRequired();
                user.HasIndex(x => x.Name).IsUnique();
            });

            builder.Entity<Lot>(lot =>
            {
                lot.HasKey(x => x.Id);
                lot.Property(x => x.Name).IsRequired();
                lot.Property(x => x.Description).IsRequired();
            });

            builder.Entity<Bid>(bid =>
            {
                bid.HasKey(x => x.Id);
                bid.OwnsOne(x => x.Amount, y =>
                {
                    y.Property(z => z.Amount).IsRequired();
                    y.Property(z => z.Currency).IsRequired();
                });
                bid.Property(x => x.Date).IsRequired();
                bid.HasOne(x => x.Bidder).WithMany();
            });

            builder.Entity<Auction>(auction =>
            {
                auction.HasKey(x => x.Id);
                auction.HasOne(x => x.Lot);
                auction.Property(x => x.StartDate).IsRequired(false);
                auction.Property(x => x.CloseDate).IsRequired(false);
                auction.Property(x => x.Duration)
                    .HasConversion(
                        x => x.Ticks,
                        x => new TimeSpan(x));
                auction.OwnsOne(x => x.InitialAmount, y =>
                {
                    y.Property(z => z.Amount).IsRequired();
                    y.Property(z => z.Currency).IsRequired();
                });
                auction.HasOne(x => x.Seller).WithMany();
                auction.HasMany(x => x.Bids).WithOne();
                auction.Property(x => x.Status).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cqrs;Trusted_Connection=True;");
        }
    }
}
