using System;
using System.Threading;

namespace AuctionR
{
    public class Auction
    {
        static int _auctionId = 0;

        public Auction(AuctionItem item, DateTimeOffset startTime, DateTimeOffset endTime, decimal startingPrice, decimal reserve)
        {
            Id = Interlocked.Increment(ref _auctionId);
            Item = item;
            StartTime = startTime;
            EndTime = endTime;
            StartingPrice = startingPrice;
        }

        public int Id { get; }
        public AuctionItem Item { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }
        public decimal StartingPrice { get; }
        public decimal BidPrice { get; }
        public bool Active { get; set; }
    }
}
