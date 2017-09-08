using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AuctionEngine _auctionEngine;

        public IndexModel(AuctionEngine auctionEngine)
        {
            _auctionEngine = auctionEngine;
        }

        public IEnumerable<Auction> Auctions => _auctionEngine.Auctions;

        public void OnGet()
        {

        }
    }
}
