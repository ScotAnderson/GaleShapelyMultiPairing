using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{
    /// <summary>
    /// Solver class which loops through the buyers and places orders,
    /// then loops through the sellers and confirms best available order
    /// </summary>
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

            foreach (Seller s in sellers)
            {
                if (s.BuyersRanked.Count != buyers.Count)
                {
                    throw new ArgumentException(string.Format("Seller ({0}) does not have correct number of buyers in its preferences.", s.ToString()));
                }
            }

            int numSellers = sellers.Count;
            foreach (Buyer b in buyers)
            {
                if (b.SellersRanked.Count != sellers.Count)
                {
                    throw new ArgumentException(string.Format("Buyer ({0}) does not have correct number of sellers in its preferences.", b.ToString()));
                }
                numSellers -= b.NumSellersDesired;
            }

            if (numSellers != 0)
            {
                throw new ArgumentException("Should be exactly enough sellers to satisfy all buyers, but no more or less.");
            }

            // This might be improved by keeping a list of buyers that aren't satisfied, and only visiting those buyers?
            bool isStable = false;
            while (!isStable)
            {
                isStable = true;
                foreach (Buyer b in buyers)
                {
                    if (b.SubmitOrderRequest() > 0)
                    {
                        isStable = false;
                    }
                }
            }
        }
    }
}
