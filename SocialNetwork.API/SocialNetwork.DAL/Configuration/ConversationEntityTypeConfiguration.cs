using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Configuration
{
    public class ConversationEntityTypeConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title)
                .HasMaxLength(40);
            builder.Property(c => c.LastMessageDate)
                .IsRequired();
            builder.Property(c => c.CreatedDate)
                .IsRequired();
        }
    }
}
