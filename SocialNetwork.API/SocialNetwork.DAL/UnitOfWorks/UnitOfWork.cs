using System;
using System.Threading.Tasks;
using SocialNetwork.DAL.Data;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Repositories.Interfaces;

namespace SocialNetwork.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SocialNetworkContext _context;
        private IUserRepository _userRepository;
        private IFriendshipRepository _friendshipRepository;
        private IConversationRepository _conversationRepository;
        private IMessageRepository _messageRepository;
        private IParticipantRepository _participantRepository;
        private IMessageReportRepository _messageReportRepository;

        public UnitOfWork(SocialNetworkContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }

        public IFriendshipRepository FriendshipRepository
        {
            get
            {
                return _friendshipRepository ??= new FriendshipRepository(_context);
            }
        }

        public IConversationRepository ConversationRepository
        {
            get
            {
                return _conversationRepository ??= new ConversationRepository(_context);
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                return _messageRepository ??= new MessageRepository(_context);
            }
        }

        public IParticipantRepository ParticipantRepository
        {
            get
            {
                return _participantRepository ??= new ParticipantRepository(_context);
            }
        }

        public IMessageReportRepository MessageReportRepository
        {
            get
            {
                return _messageReportRepository ??= new MessageReportRepository(_context);
            }
        }

        public Task Commit()
        {
            return _context.SaveChangesAsync();
        }

        public void RollBack()
        {
            _context?.Dispose();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
