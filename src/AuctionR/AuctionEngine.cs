using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace AuctionR
{
    public class AuctionEngine : IDisposable
    {
        private Random _random = new Random();
        private ConcurrentDictionary<int, Auction> _auctions = new ConcurrentDictionary<int, Auction>();
        private Timer _timer;
        private IHubContext<AuctionHub> _auctionHubContext;

        public AuctionEngine(ILogger<AuctionEngine> logger, IApplicationLifetime appLifetime, IHubContext<AuctionHub> auctionHubContext)
        {
            appLifetime.ApplicationStarted.Register(() => StartTimer());
            _auctionHubContext = auctionHubContext;
            for (var i = 0; i < 5; i++)
            {
                var auction = GenerateAuction();
                _auctions[auction.Id] = auction;
            }
        }

        private void StartTimer()
        {
            _timer = new Timer(state => ((AuctionEngine)state).ScanAuctions(), this, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void ScanAuctions()
        {
            foreach (var auction in _auctions)
            {
                if (auction.Value.EndTime < DateTime.Now)
                {
                    if (_auctions.TryRemove(auction.Key, out _))
                    {
                        _auctionHubContext.Clients.All.InvokeAsync("AuctionEnded", auction.Key);
                    }
                }
            }
        }

        public IEnumerable<Auction> Auctions => _auctions.Values;

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private Auction GenerateAuction()
        {
            var auctionItems = new[]
            {
                new AuctionItem { Name = "Item 1" },
                new AuctionItem { Name = "Item 2" },
                new AuctionItem { Name = "Item 3" },
                new AuctionItem { Name = "Item 4" }
            };

            var startTime = DateTimeOffset.Now.AddSeconds(_random.Next(10, 60));
            var endTime = startTime.AddSeconds(_random.Next(60, 120));
            var startingPrice = _random.Next(100, 200);
            var reserve = startingPrice + _random.Next(1, 50);
            return new Auction(auctionItems[_random.Next(0, auctionItems.Length - 1)], startTime, endTime, startingPrice, reserve);
        }
    }
}
