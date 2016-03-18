﻿using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Extensions;
using System;
using UltimateTeam.Toolkit.Exceptions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class SendItemToTradePileRequest : FutRequestBase, IFutRequest<SendItemToTradePileResponse>
    {
        private readonly ItemData _itemData;
        private AppVersion _appVersion;

        public SendItemToTradePileRequest(ItemData itemData)
        {
            itemData.ThrowIfNullArgument();
            _itemData = itemData;
        }

        public async Task<SendItemToTradePileResponse> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddMethodOverrideHeader(HttpMethod.Put);
                AddCommonHeaders();
                var content = string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", _itemData.Id);
                var tradepileResponseMessage = await HttpClient
                    .PostAsync(string.Format(Resources.FutHome + Resources.ListItem), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<SendItemToTradePileResponse>(tradepileResponseMessage);
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddCommonMobileHeaders();
                var content = string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", _itemData.Id);
                var tradepileResponseMessage = await HttpClient
                    .PutAsync(string.Format(Resources.FutHome + Resources.ListItem + "?_=" + DateTimeExtensions.ToUnixTime(DateTime.Now)), new StringContent(content))
                    .ConfigureAwait(false);

                return await Deserialize<SendItemToTradePileResponse>(tradepileResponseMessage);
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}
