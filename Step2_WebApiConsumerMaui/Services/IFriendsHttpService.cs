using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Step2_WebApiConsumerConsole.Models;

namespace Step2_WebApiConsumerConsole.Services
{
    public interface IFriendsHttpService
    {
        Task<WebApiID> GetInfoAsync();

        Task<IEnumerable<Friend>> GetFriendsAsync(int? count=null);
        Task<Friend> GetFriendAsync(Guid Id);

        Task<Friend> UpdateFriendAsync(Friend friend);

        Task<Friend> CreateFriendAsync(Friend friend);
        Task<Friend> DeleteFriendAsync(Guid Id);

        Task<IEnumerable<GoodQuote>> GetQuotesAsync(int? count = null);
    }
}
