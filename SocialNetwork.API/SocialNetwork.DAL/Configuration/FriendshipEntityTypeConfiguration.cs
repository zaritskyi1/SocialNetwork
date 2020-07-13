using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Configuration
{
    public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasAlternateKey(f => new
            {
                f.SenderId,
                f.ReceiverId
            });

            builder.HasAlternateKey(f => new
            {
                f.ReceiverId,
                f.SenderId
            });

            builder.HasOne<User>(f => f.Sender)
                .WithMany(u => u.FriendshipsSent)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(f => f.Receiver)
                .WithMany(u => u.FriendshipsReceived)
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.Status)
                .HasConversion<string>();

            builder.Property(f => f.StatusChangedDate)
                .IsRequired();
        }
    }
}
