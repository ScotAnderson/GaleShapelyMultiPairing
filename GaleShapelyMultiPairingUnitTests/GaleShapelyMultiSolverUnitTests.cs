using GaleShapelyMultiPairing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GaleShapelyMultiPairingUnitTests
{
    [TestClass]
    public class GaleShapelyMultiSolverUnitTests
    {
        [TestMethod]
        public void SingleBuyerTestCase()
        {
            Buyer buyer = new Buyer("Buyer");
            Seller seller0 = new Seller("Seller 0");
            Seller seller1 = new Seller("Seller 1");
            Seller seller2 = new Seller("Seller 2");

            List<Buyer> buyers = new List<Buyer>();
            buyers.Add(buyer);

            List<Seller> sellers = new List<Seller>();
            sellers.Add(seller0);
            sellers.Add(seller1);
            sellers.Add(seller2);

            buyer.SetPreferences(3, sellers);
            seller0.SetPreferences(buyers);
            seller1.SetPreferences(buyers);
            seller2.SetPreferences(buyers);

            GaleShapelyMultiSolver.FindSolution(buyers, sellers);
            Assert.AreEqual(3, buyer.SellersConfirmed.Count);
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller0));
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller1));
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller2));
            Assert.AreEqual(buyer, seller0.Buyer);
            Assert.AreEqual(buyer, seller1.Buyer);
            Assert.AreEqual(buyer, seller2.Buyer);
        }
    }
}
