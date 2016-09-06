using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{

    /// <summary>
    /// Basic Buyer class (Proposer)
    /// (Choosing Buyer class as the proposer, because in a vacuum of information, I favor the buyer's preferences to the seller's preferences)
    /// Buyers want to buy from between 1 and 3 sellers.
    /// </summary>

    public class Buyer
    {
        public string Name { get; private set; }
        public int NumSellersDesired { get; private set; }
        public List<Seller> SellersRanked { get; private set; }
        public List<Seller> SellersConfirmed { get; private set; }

        public bool IsAllSellersConfirmed
        {
            get { return NumSellersDesired == SellersConfirmed.Count; }
        }

        private int _nextSellerIndex { get; set; }

        public Buyer(string name)
        {
            this.Name = name;
            this.SellersConfirmed = new List<Seller>();
            this._nextSellerIndex = 0;
        }

        public void SetPreferences(int numSellersDesired, List<Seller> sellersRanked)
        {
            if (numSellersDesired < 1 || numSellersDesired > 3)
            {
                throw new ArgumentOutOfRangeException("Argument must be in range 1-3 inclusive.", "NumSellersDesired");
            }

            if (sellersRanked == null)
            {
                throw new ArgumentNullException("RankedSellers");
            }
            
            this.NumSellersDesired = numSellersDesired;
            this.SellersRanked = sellersRanked;
        }

        // Make as many proposals each round as necessary to "fill up" the buyer.
        public int SubmitOrderRequest()
        {
            int numOrdersThisRouned = NumSellersDesired - SellersConfirmed.Count;

            // Validation check. Should never run out of sellers before orders are complete in well formed data.
            if (numOrdersThisRouned + _nextSellerIndex > SellersRanked.Count)
            {
                throw new InvalidOperationException("Run out of sellers before proposals are complete.");
            }

            for (int i = 0; i < numOrdersThisRouned; i++)
            {
                SellersRanked[_nextSellerIndex].ReceiveOrderRequest(this);
                _nextSellerIndex++;
            }

            return numOrdersThisRouned;
        }

        public void OrderAccepted(Seller seller)
        {
            // Validation check. Proposals should never be sent out that would push us over the edge.
            if (SellersConfirmed.Count >= NumSellersDesired)
            {
                throw new InvalidOperationException("Cannot add another Seller to SellersConfirmed. List is already at desired number of Sellers.");
            }

            SellersConfirmed.Add(seller);
        }

        public void OrderCancelled(Seller seller)
        {
            SellersConfirmed.Remove(seller);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
