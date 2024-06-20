﻿using UltimateTeam.Toolkit.Models.Auction;

namespace UltimateTeam.Toolkit.Models
{
    public class CreditsResponse
    {
        public uint Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public UnopenedPacks UnopenedPacks { get; set; }

        public BidTokens BidTokens { get; set; }

        public double FutCashBalance { get; set; }
    }
}
