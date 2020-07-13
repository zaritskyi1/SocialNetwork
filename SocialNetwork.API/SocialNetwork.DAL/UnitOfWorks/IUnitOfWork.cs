using System.Threading.Tasks;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }
        IConversationRepository ConversationRepository { get; }
        IMessageRepository MessageRepository { get; }
        IParticipantRepository ParticipantRepository { get; }
        IMessageReportRepository MessageReportRepository { get; }
        Task Commit();
        void RollBack();
    }
}
