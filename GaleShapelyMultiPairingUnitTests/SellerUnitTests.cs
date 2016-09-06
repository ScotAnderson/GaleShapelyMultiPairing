using GaleShapelyMultiPairing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GaleShapelyMultiPairingUnitTests
{
    [TestClass]
    public class SellerUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SellerSetPreferencesThrowsOnBuyerRankedNull()
        {
            Seller seller = new Seller("Seller");
            seller.SetPreferences(null);
        }

        [TestMethod]
        public void SellerReceiveOrderRequestTestCase()
        {
            Buyer buyer1 = new Buyer("Buyer 1");
            Buyer buyer2 = new Buyer("Buyer 2");

            List<Buyer> buyers = new List<Buyer>();
            buyers.Add(buyer1);
            buyers.Add(buyer2);

            Seller seller = new Seller("Seller");
            List<Seller> sellers = new List<Seller>();
            sellers.Add(seller);

            buyer1.SetPreferences(1, sellers);
            buyer2.SetPreferences(1, sellers);           

            seller.SetPreferences(buyers);

            seller.ReceiveOrderRequest(buyer2);
            Assert.AreEqual(buyer2, seller.Buyer);

            seller.ReceiveOrderRequest(buyer1);
            Assert.AreEqual(buyer1, seller.Buyer);

            seller.ReceiveOrderRequest(buyer2);
            Assert.AreEqual(buyer1, seller.Buyer);
        }
    }
}
