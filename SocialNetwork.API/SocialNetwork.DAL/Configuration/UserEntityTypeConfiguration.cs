using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Activities)
                .HasMaxLength(256);

            builder.Property(u => u.City)
                .HasMaxLength(128);

            builder.Property(u => u.Country)
                .HasMaxLength(128);

            builder.Property(u => u.Created)
                .IsRequired();

            builder.Property(u => u.DateOfBirth)
                .IsRequired();

            builder.Property(u => u.FavoriteGames)
                .HasMaxLength(256);

            builder.Property(u => u.FavoriteMovies)
                .HasMaxLength(256);

            builder.Property(u => u.FavoriteQuotes)
                .HasMaxLength(256);

            builder.Property(u => u.Gender)
                .HasMaxLength(32);

            builder.Property(u => u.LastActive)
                .IsRequired();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(u => u.Status)
                .HasMaxLength(256);
        }
    }
}
