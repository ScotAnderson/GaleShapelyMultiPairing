using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{

    // Buyers want up to be matched with up to three sellers. Some want one, some two, some three.

    public class Buyer
    {
        public int NumSellersDesired { get; private set; }
        public List<Seller> SellerPreferences { get; private set; }
        public string Name { get; private set; }
        public List<Seller> SellersConfirmed { get; private set; }
        public bool Engaged
        {
            get { return NumSellersDesired == SellersConfirmed.Count; }
        }
        private int NextProposal { get; set; }

        public Buyer(string name)
        {
            this.Name = name;
            this.SellersConfirmed = new List<Seller>();
            this.NextProposal = 0;
        }

        public void SetPreferences(int NumSellersDesired, List<Seller> RankedSellers)
        {
            if (NumSellersDesired < 1 || NumSellersDesired > 3)
            {
                throw new ArgumentException("Argument must be in range 1-3 inclusive.", "NumSellersDesired");
            }

            if (RankedSellers == null)
            {
                throw new ArgumentNullException("RankedSellers");
            }
            
            this.NumSellersDesired = NumSellersDesired;
            this.SellerPreferences = RankedSellers;
        }

        public void Propose()
        {
            int numProposalsThisRound = NumSellersDesired - SellersConfirmed.Count;

            if (numProposalsThisRound + NextProposal > SellerPreferences.Count)
            {
                throw new InvalidOperationException("Run out of sellers before proposals are complete.");
            }

            for (int i = 0; i < numProposalsThisRound; i++)
            {
                SellerPreferences[NextProposal].ReceiveProposal(this);
                NextProposal++;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        
    }
}
