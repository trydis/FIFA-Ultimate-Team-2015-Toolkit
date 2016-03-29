using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ReListRequest : FutRequestBase, IFutRequest<byte>
    {
        public async Task<byte> PerformRequestAsync()
        {
            var uriString = Resources.FutHome + Resources.Auctionhouse + Resources.ReList;
            var content = new StringContent(" ");
            Task<HttpResponseMessage> reListMessageTask;

            if (AppVersion == AppVersion.WebApp)
            {
                AddCommonHeaders(HttpMethod.Put);
                reListMessageTask = HttpClient.PostAsync(uriString, content);
            }
            else
            {
                AddCommonMobileHeaders();
                uriString += $"?_={DateTime.Now.ToUnixTime()}";
                reListMessageTask = HttpClient.PutAsync(uriString, content);
            }

            var reListMessage = await reListMessageTask.ConfigureAwait(false);
            reListMessage.EnsureSuccessStatusCode();

            return 0;
        }
    }
}
