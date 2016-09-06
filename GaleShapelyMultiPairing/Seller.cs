using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{
    public class Seller
    {
        public List<Buyer> BuyerPreferences { get; private set; }
        public string Name { get; private set; }

        private List<Buyer> Proposals { get; set; }

        public Seller(string name)
        {
            this.Name = name;
            this.Proposals = new List<Buyer>();
        }

        public void SetPreferences(List<Buyer> RankedBuyers)
        {
            if (RankedBuyers == null)
            {
                throw new ArgumentNullException("RankedBuyers");
            }

            this.BuyerPreferences = RankedBuyers;
        }

        public void ReceiveProposal(Buyer buyer)
        {
            this.Proposals.Add(buyer);
        }

        public void EvaluateProposals()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
