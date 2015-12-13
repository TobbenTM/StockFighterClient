using System;

namespace StockFighterClient.Models {
    class Bid {
        int Price { get; set; }
        int Qty { get; set; }
        bool IsBuy { get; set; }

        override
        public String ToString() {
            return (String.Format("Price: {0}, Quantity: {1}, ", Price, Qty) + (IsBuy ? "Buy Order" : "Sell Order"));
        }
    }
}
