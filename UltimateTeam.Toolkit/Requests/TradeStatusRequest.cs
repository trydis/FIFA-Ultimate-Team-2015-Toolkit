﻿using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.RequestFactory;

namespace UltimateTeam.Toolkit.Requests
{
    internal class TradeStatusRequest : FutRequestBase, IFutRequest<AuctionResponse>
    {
        private readonly IEnumerable<long> _tradeIds;

        public TradeStatusRequest(IEnumerable<long> tradeIds)
        {
            tradeIds.ThrowIfNullArgument();
            _tradeIds = tradeIds;
        }

        public async Task<AuctionResponse> PerformRequestAsync()
        {
            var uriString = string.Format(Resources.FutHome + Resources.TradeStatus, string.Join("%2C", _tradeIds));
            Task<HttpResponseMessage> tradeStatusResponseMessageTask;

            AddCommonHeaders();
            tradeStatusResponseMessageTask = HttpClient.GetAsync(uriString);
            var tradeStatusResponseMessage = await tradeStatusResponseMessageTask.ConfigureAwait(false);

            return await DeserializeAsync<AuctionResponse>(tradeStatusResponseMessage);
        }
    }
}
