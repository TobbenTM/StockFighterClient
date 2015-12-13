using System;
using System.Text;

namespace StockFighterClient.Models {
    class Stock {
        public String Venue { get; set; }
        public String Name { get; set; }
        public String Symbol { get; set; }
        public Bid[] Bids { get; set; }
        public Bid[] Asks { get; set; }
        public String TimeStamp { get; set; }

        override
        public String ToString() {
            StringBuilder sb = new StringBuilder();
            String name = Name ?? "";
            sb.Append(String.Format("{0} ({1}), on venue {2}.", name, Symbol, Venue));
            sb.Append((TimeStamp != null ? ("\nCaptured at: " + TimeStamp) : ""));
            sb.Append((Bids != null ? "\nBids for this stock:" : ""));
            foreach(Bid bid in Bids ?? new Bid[0]) {
                sb.Append(bid.ToString() + "\n");
            }
            sb.Append((Asks != null ? "\nAsks for this stock:" : ""));
            foreach (Bid ask in Asks ?? new Bid[0]) {
                sb.Append(ask.ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}
