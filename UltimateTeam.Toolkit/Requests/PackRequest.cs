﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PackRequest : FutRequestBase, IFutRequest<PurchasedPackResponse>
    {
        private readonly PackDetails _packDetails;

        public PackRequest(PackDetails PackDetails)
        {
            _packDetails = PackDetails;
        }

        public async Task<PurchasedPackResponse> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.PurchasedItems;

            var content = $"{{\"currency\":{_packDetails.Currency},\"packId\":{_packDetails.PackId}," +
               $"\"useCredits\":{_packDetails.UseCredits},\"useCredits\":{_packDetails.UseCredits}}}";

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Get);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
            }

            var purchasedItemsMessage = await HttpClient
                .PostAsync(uriString, new StringContent(content))
                .ConfigureAwait(false);

            return await DeserializeAsync<PurchasedPackResponse>(purchasedItemsMessage);
        }
    }
}
