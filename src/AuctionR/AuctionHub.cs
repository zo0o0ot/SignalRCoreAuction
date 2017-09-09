using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AuctionR
{
    public class AuctionHub : Hub
    {
        private readonly AuctionEngine _auctionEngine;

        public AuctionHub(AuctionEngine auctionEngine)
        {
            _auctionEngine = auctionEngine;
        }

        public override Task OnConnectedAsync()
        {
            return Clients.Client(Context.ConnectionId)
                .InvokeAsync("AddAuctions", _auctionEngine.Auctions);
        }
    }
}
