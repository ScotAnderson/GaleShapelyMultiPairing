using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{
    /// <summary>
    /// Basic Seller Class (Evaluator/Acceptor)
    /// Sellers only want a single buyer.
    /// </summary>
    public class Seller
    {
        public string Name { get; private set; }
        public List<Buyer> BuyersRanked { get; private set; }
        public Buyer Buyer
        {
            get { return BuyersRanked[this._buyerIndex]; }
        }

        private List<Buyer> _ordersSubmitted { get; set; }
        private int _buyerIndex { get; set; }

        public Seller(string name)
        {
            this.Name = name;
            this._ordersSubmitted = new List<Buyer>();
            this._buyerIndex = int.MaxValue;
        }

        public void SetPreferences(List<Buyer> buyersRanked)
        {
            if (buyersRanked == null)
            {
                throw new ArgumentNullException("RankedBuyers");
            }

            this.BuyersRanked = buyersRanked;
        }

        // On order received, see if the order (buyer) is better than the current order, and cancel if necessary to upgrade
        public void ReceiveOrderRequest(Buyer buyer)
        {
            int newIndex = BuyersRanked.IndexOf(buyer);
            if (this._buyerIndex > newIndex)
            {
                if (this._buyerIndex < int.MaxValue)
                {
                    BuyersRanked[this._buyerIndex].OrderCancelled(this);
                }

                this._buyerIndex = newIndex;
                this.Buyer.OrderAccepted(this);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
