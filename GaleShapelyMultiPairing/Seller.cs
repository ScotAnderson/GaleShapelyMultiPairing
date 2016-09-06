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
            this._buyerIndex = -1;
        }

        public void SetPreferences(List<Buyer> buyersRanked)
        {
            if (buyersRanked == null)
            {
                throw new ArgumentNullException("RankedBuyers");
            }

            this.BuyersRanked = buyersRanked;
        }

        public void ReceiveOrderRequest(Buyer buyer)
        {
            int currentBuyerIndex = this._buyerIndex;
            EvaluateOrder(buyer);
            if (this._buyerIndex != currentBuyerIndex)
            {
                if (currentBuyerIndex >= 0)
                {
                    BuyersRanked[currentBuyerIndex].OrderCancelled(this);
                }

                this.Buyer.OrderAccepted(this);
            }
        }

        // Check an individual order to see if it is better, upgrade if so
        private void EvaluateOrder(Buyer newBuyer)
        {
            int newIndex = BuyersRanked.FindIndex((Buyer b) => (b == newBuyer));
            if (this._buyerIndex == -1 || this._buyerIndex > newIndex)
            {
                this._buyerIndex = newIndex;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
