﻿using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class ItemRequest : FutRequestBase, IFutRequest<Item>
    {
        private readonly long _baseId;
        private AppVersion _appVersion;

        public ItemRequest(long baseId)
        {
            _baseId = baseId;
        }

        public async Task<Item> PerformRequestAsync(AppVersion appVersion)
        {
            _appVersion = appVersion;

            if (_appVersion == AppVersion.WebApp)
            {
                AddAnonymousHeader();
                var itemResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.Item, _baseId))
                    .ConfigureAwait(false);
                var itemWrapper = await Deserialize<ItemWrapper>(itemResponseMessage);

                return itemWrapper.Item;
            }
            else if (_appVersion == AppVersion.CompanionApp)
            {
                AddAnonymousMobileHeader();
                var itemResponseMessage = await HttpClient
                    .GetAsync(string.Format(Resources.Item, _baseId))
                    .ConfigureAwait(false);
                var itemWrapper = await Deserialize<ItemWrapper>(itemResponseMessage);

                return itemWrapper.Item;
            }
            else
            {
                throw new FutException(string.Format("Unknown AppVersion: {0}", appVersion.ToString()));
            }
        }
    }
}
