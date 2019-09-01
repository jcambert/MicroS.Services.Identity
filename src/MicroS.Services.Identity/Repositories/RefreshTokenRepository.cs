using MicroS.Services.Identity.Domain;
using MicroS_Common.Mongo;
using System.Threading.Tasks;

namespace MicroS.Services.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);

        public async Task AddAsync(RefreshToken token)
            => await _repository.AddAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.UpdateAsync(token);
    }
}
