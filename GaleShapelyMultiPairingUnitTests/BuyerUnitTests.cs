using GaleShapelyMultiPairing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GaleShapelyMultiPairingUnitTests
{
    [TestClass]
    public class BuyerUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BuyerSetPreferencesThrowsOnNumSellersOutOfBounds()
        {
            Buyer buyer = new Buyer("Buyer");
            buyer.SetPreferences(0, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuyerSetPreferencesThrowsOnSellerRankedNull()
        {
            Buyer buyer = new Buyer("Buyer");
            buyer.SetPreferences(1, null);
        }

        [TestMethod]
        public void BuyerOrderSubmissionTestCase()
        {
            Buyer buyer = new Buyer("Buyer");
            Seller seller0 = new Seller("Seller 0");
            Seller seller1 = new Seller("Seller 1");

            List<Buyer> buyers = new List<Buyer>();
            buyers.Add(buyer);

            List<Seller> sellers = new List<Seller>();
            sellers.Add(seller0);
            sellers.Add(seller1);

            buyer.SetPreferences(2, sellers);
            seller0.SetPreferences(buyers);
            seller1.SetPreferences(buyers);

            int result = buyer.SubmitOrderRequest();

            Assert.AreEqual(2, result);
            Assert.AreEqual(2, buyer.SellersConfirmed.Count);
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller0));
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller1));
            Assert.AreEqual(buyer, seller0.Buyer);
            Assert.AreEqual(buyer, seller1.Buyer);
        }

        [TestMethod]
        public void BuyerOrderAcceptedTestCase()
        {
            Buyer buyer = new Buyer("Buyer");
            buyer.SetPreferences(1, new List<Seller>());
            Seller seller0 = new Seller("Seller 0");

            buyer.OrderAccepted(seller0);

            Assert.AreEqual(1, buyer.SellersConfirmed.Count);
            Assert.IsTrue(buyer.SellersConfirmed.Contains(seller0));

        }
    }
}
