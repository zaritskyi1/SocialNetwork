using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Configuration;
using SocialNetwork.DAL.Models;

namespace SocialNetwork.DAL.Data
{
    public class SocialNetworkContext : IdentityDbContext<User>
    {
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            builder.ApplyConfiguration(new ConversationEntityTypeConfiguration());
            builder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            builder.ApplyConfiguration(new ParticipantEntityTypeConfiguration());
            builder.ApplyConfiguration(new MessageReportEntityTypeConfiguration());
        }

        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<MessageReport> MessageReports { get; set; }
    }
}
