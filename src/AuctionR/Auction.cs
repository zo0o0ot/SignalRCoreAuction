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
            Reserve = reserve;
        }

        public int Id { get; }
        public AuctionItem Item { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; }
        public decimal StartingPrice { get; }
        public decimal Reserve { get; }
        public decimal BidPrice { get; }
    }
}
