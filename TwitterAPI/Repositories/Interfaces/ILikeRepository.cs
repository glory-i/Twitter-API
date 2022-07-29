using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterAPI.DatabaseRepository.Interface;
using TwitterAPI.Model;

namespace TwitterAPI.Repositories.Interfaces
{
    public interface ILikeRepository : IDatabaseRepository<Like>
    {
        public void SaveChanges();
        public Task<string> LikeTweet(string username, int tweetid); 
        public Task<string> UndoLike(string username, int tweetid); 
        public Task<IEnumerable<Account>> AccountsLikedBy(int tweetid); 

    }
}
