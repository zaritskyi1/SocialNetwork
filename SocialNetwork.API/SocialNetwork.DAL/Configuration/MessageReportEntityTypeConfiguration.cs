using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Configuration
{
    public class MessageReportEntityTypeConfiguration : IEntityTypeConfiguration<MessageReport>
    {
        public void Configure(EntityTypeBuilder<MessageReport> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasAlternateKey(m => new
            {
                m.UserId, 
                m.MessageId
            });

            builder.HasOne(m => m.User)
                .WithMany(u => u.MessageReports)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(m => m.UserId)
                .IsRequired();

            builder.HasOne(m => m.Message)
                .WithMany(m => m.MessageReports)
                .HasForeignKey(m => m.MessageId)
                .IsRequired();

            builder.Property(m => m.CreatedDate)
                .IsRequired();
        }
    }
}
