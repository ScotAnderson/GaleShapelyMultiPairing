using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{
    //   Buyers want up to be matched with up to three sellers. Some want one, some two, some three. 
    // There are enough sellers to satisfy all the buyer's desired sellers but no more.

    public static class GaleShapelyMultiSolver
    {
        public static void FindSolution(List<Buyer> buyers, List<Seller> sellers)
        {
            if (buyers == null)
            {
                throw new ArgumentNullException("buyers");
            }

            if (sellers == null)
            {
                throw new ArgumentNullException("sellers");
            }

            int numSellers = 0;
            foreach (Buyer b in buyers)
            {
                numSellers += b.NumSellersDesired;
            }

            if (numSellers != sellers.Count)
            {
                throw new ArgumentException("Should be exactly enough sellers to satisfy all buyers, but no more or less.");
            }

            bool isStable = false;
            while (!isStable)
            {
                foreach (Buyer b in buyers)
                {
                    b.Propose();
                }

                foreach (Seller s in sellers)
                {
                    s.EvaluateProposals();
                }

                isStable = true;
                foreach (Buyer b in buyers)
                {
                    if (!b.Engaged)
                    {
                        isStable = false;
                        break;
                    }
                }
            }
        }
    }
}
