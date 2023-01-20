using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Step2_WebApiConsumerConsole.Models;

namespace Step2_WebApiConsumerConsole.Services
{
    public class FriendsHttpService : BaseHttpService, IFriendsHttpService
    {
        readonly Uri _baseUri;
        readonly IDictionary<string, string> _headers;

        public FriendsHttpService(Uri baseUri)
        {
            _baseUri = baseUri;
            _headers = new Dictionary<string, string>();
        }

        public async Task<WebApiID> GetInfoAsync()
        {
            var url = new Uri(_baseUri, "/id");
            var response = await SendRequestAsync<WebApiID>(url, HttpMethod.Get, _headers);

            return response;
        }
        public async Task<IEnumerable<Friend>> GetFriendsAsync(int? count = null)
        {
            var qp = (count.HasValue) ? $"?count={count}" : null;
            var url = new Uri(_baseUri, $"/api/friends{qp}");

            var response = await SendRequestAsync<List<Friend>>(url, HttpMethod.Get, _headers);
            return response;
        }

        public async Task<Friend> GetFriendAsync(Guid Id)
        {
            var url = new Uri(_baseUri, $"/api/friends/{Id}");
            var response = await SendRequestAsync<Friend>(url, HttpMethod.Get, _headers);

            return response;
        }

        public async Task<Friend> UpdateFriendAsync(Friend friend)
        {
            var url = new Uri(_baseUri, $"/api/friends/{friend.FriendID}");

            //Confirm friend exisit in Database
            var itemToUpdate = await SendRequestAsync<Friend>(url, HttpMethod.Get, _headers);
            if (itemToUpdate == null)
                return null;  //friend does not exist

            //Update Friend, always gives null response, NonSuccess response errors are thrown in BaseHttpService
            await SendRequestAsync<Friend>(url, HttpMethod.Put, _headers, friend);

            return friend;
        }

        public async Task<Friend> CreateFriendAsync(Friend friend)
        {
            var url = new Uri(_baseUri, "/api/friends");
            var response = await SendRequestAsync<Friend>(url, HttpMethod.Post, _headers, friend);

            return response;
        }

        public async Task<Friend> DeleteFriendAsync(Guid Id)
        {
            var url = new Uri(_baseUri, $"/api/friends/{Id}");

            //Confirm customer exisit in Database
            var itemToDel = await SendRequestAsync<Friend>(url, HttpMethod.Get, _headers);
            if (itemToDel == null)
                return null;  //friend does not exist

            //Delete Customer, always gives null response, NonSuccess response errors are thrown
            await SendRequestAsync<Friend>(url, HttpMethod.Delete, _headers);
            return itemToDel;
        }


        public async Task<IEnumerable<GoodQuote>> GetQuotesAsync(int? count = null)
        {
            var qp = (count.HasValue) ? $"?count={count}" : null;
            var url = new Uri(_baseUri, $"/api/quotes{qp}");

            var response = await SendRequestAsync<List<GoodQuote>>(url, HttpMethod.Get, _headers);
            return response;
        }
    }
}
