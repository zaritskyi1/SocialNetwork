using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Configuration
{
    public class ParticipantEntityTypeConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => new
            {
                p.UserId,
                p.ConversationId
            });

            builder.HasOne<User>(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserId);

            builder.HasOne<Conversation>(p => p.Conversation)
                .WithMany(c => c.Participants)
                .HasForeignKey(p => p.ConversationId);

            builder.Property(p => p.HasUnreadMessages)
                .IsRequired();
        }
    }
}
