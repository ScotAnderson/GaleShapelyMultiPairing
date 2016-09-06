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

        // This is the classic test with 3 suitors and 3 reviewers described in wikipedia article
        [TestMethod]
        public void ClassicScenarioTestCase()
        {
            Buyer buyerA = new Buyer("Buyer A");
            Buyer buyerB = new Buyer("Buyer B");
            Buyer buyerC = new Buyer("Buyer C");

            Seller sellerX = new Seller("Seller X");
            Seller sellerY = new Seller("Seller Y");
            Seller sellerZ = new Seller("Seller Z");

            buyerA.SetPreferences(1, new List<Seller> { sellerY, sellerX, sellerZ });
            buyerB.SetPreferences(1, new List<Seller> { sellerZ, sellerY, sellerX });
            buyerC.SetPreferences(1, new List<Seller> { sellerX, sellerZ, sellerY });

            sellerX.SetPreferences(new List<Buyer> { buyerB, buyerA, buyerC });
            sellerY.SetPreferences(new List<Buyer> { buyerC, buyerB, buyerA });
            sellerZ.SetPreferences(new List<Buyer> { buyerA, buyerC, buyerB });

            GaleShapelyMultiSolver.FindSolution(new List<Buyer> { buyerA, buyerB, buyerC }, new List<Seller> { sellerX, sellerY, sellerZ });
            Assert.AreSame(sellerY, buyerA.SellersConfirmed[0]);
            Assert.AreSame(sellerZ, buyerB.SellersConfirmed[0]);
            Assert.AreSame(sellerX, buyerC.SellersConfirmed[0]);

            Assert.AreSame(buyerC, sellerX.Buyer);
            Assert.AreSame(buyerA, sellerY.Buyer);
            Assert.AreSame(buyerB, sellerZ.Buyer);
        }

        [TestMethod]
        public void Complex1TestCase()
        {
            List<Buyer> buyers = CreateBuyers(4);
            List<Seller> sellers = CreateSellers(8);

            buyers[0].SetPreferences(1, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7] });
            buyers[1].SetPreferences(2, new List<Seller> { sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[0], sellers[1] });
            buyers[2].SetPreferences(2, new List<Seller> { sellers[4], sellers[5], sellers[6], sellers[7], sellers[0], sellers[1], sellers[2], sellers[3] });
            buyers[3].SetPreferences(3, new List<Seller> { sellers[6], sellers[7], sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5] });

            sellers[0].SetPreferences(new List<Buyer> { buyers[3], buyers[2], buyers[1], buyers[0] });
            sellers[1].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0], buyers[3] });
            sellers[2].SetPreferences(new List<Buyer> { buyers[1], buyers[0], buyers[3], buyers[2] });
            sellers[3].SetPreferences(new List<Buyer> { buyers[0], buyers[3], buyers[2], buyers[1] });
            sellers[4].SetPreferences(new List<Buyer> { buyers[3], buyers[2], buyers[1], buyers[0] });
            sellers[5].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0], buyers[3] });
            sellers[6].SetPreferences(new List<Buyer> { buyers[1], buyers[0], buyers[3], buyers[2] });
            sellers[7].SetPreferences(new List<Buyer> { buyers[0], buyers[3], buyers[2], buyers[1] });

            GaleShapelyMultiSolver.FindSolution(buyers, sellers);

            Assert.AreEqual(1, buyers[0].SellersConfirmed.Count);
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[1]));
            Assert.AreSame(buyers[0], sellers[1].Buyer);

            Assert.AreEqual(2, buyers[1].SellersConfirmed.Count);
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[2]));
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[3]));
            Assert.AreSame(buyers[1], sellers[2].Buyer);
            Assert.AreSame(buyers[1], sellers[3].Buyer);

            Assert.AreEqual(2, buyers[2].SellersConfirmed.Count);
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[4]));
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[5]));
            Assert.AreSame(buyers[2], sellers[4].Buyer);
            Assert.AreSame(buyers[2], sellers[5].Buyer);

            Assert.AreEqual(3, buyers[3].SellersConfirmed.Count);
            Assert.IsTrue(buyers[3].SellersConfirmed.Contains(sellers[0]));
            Assert.IsTrue(buyers[3].SellersConfirmed.Contains(sellers[6]));
            Assert.IsTrue(buyers[3].SellersConfirmed.Contains(sellers[7]));
            Assert.AreSame(buyers[3], sellers[0].Buyer);
            Assert.AreSame(buyers[3], sellers[6].Buyer);
            Assert.AreSame(buyers[3], sellers[7].Buyer);
        }

        [TestMethod]
        public void Complex2TestCase()
        {
            List<Buyer> buyers = CreateBuyers(3);
            List<Seller> sellers = CreateSellers(9);

            buyers[0].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });
            buyers[1].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });
            buyers[2].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });

            sellers[0].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[1].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[2].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[3].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[4].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[5].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[6].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[7].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });
            sellers[8].SetPreferences(new List<Buyer> { buyers[0], buyers[1], buyers[2] });

            GaleShapelyMultiSolver.FindSolution(buyers, sellers);

            Assert.AreEqual(3, buyers[0].SellersConfirmed.Count);
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[0]));
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[1]));
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[2]));
            Assert.AreSame(buyers[0], sellers[0].Buyer);
            Assert.AreSame(buyers[0], sellers[1].Buyer);
            Assert.AreSame(buyers[0], sellers[2].Buyer);

            Assert.AreEqual(3, buyers[1].SellersConfirmed.Count);
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[3]));
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[4]));
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[5]));
            Assert.AreSame(buyers[1], sellers[3].Buyer);
            Assert.AreSame(buyers[1], sellers[4].Buyer);
            Assert.AreSame(buyers[1], sellers[5].Buyer);

            Assert.AreEqual(3, buyers[2].SellersConfirmed.Count);
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[6]));
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[7]));
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[8]));
            Assert.AreSame(buyers[2], sellers[6].Buyer);
            Assert.AreSame(buyers[2], sellers[7].Buyer);
            Assert.AreSame(buyers[2], sellers[8].Buyer);
        }

        [TestMethod]
        public void Complex3TestCase()
        {
            List<Buyer> buyers = CreateBuyers(3);
            List<Seller> sellers = CreateSellers(9);

            buyers[0].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });
            buyers[1].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });
            buyers[2].SetPreferences(3, new List<Seller> { sellers[0], sellers[1], sellers[2], sellers[3], sellers[4], sellers[5], sellers[6], sellers[7], sellers[8] });

            sellers[0].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[1].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[2].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[3].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[4].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[5].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[6].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[7].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });
            sellers[8].SetPreferences(new List<Buyer> { buyers[2], buyers[1], buyers[0] });

            GaleShapelyMultiSolver.FindSolution(buyers, sellers);

            Assert.AreEqual(3, buyers[0].SellersConfirmed.Count);
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[6]));
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[7]));
            Assert.IsTrue(buyers[0].SellersConfirmed.Contains(sellers[8]));
            Assert.AreSame(buyers[0], sellers[6].Buyer);
            Assert.AreSame(buyers[0], sellers[7].Buyer);
            Assert.AreSame(buyers[0], sellers[8].Buyer);

            Assert.AreEqual(3, buyers[1].SellersConfirmed.Count);
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[3]));
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[4]));
            Assert.IsTrue(buyers[1].SellersConfirmed.Contains(sellers[5]));
            Assert.AreSame(buyers[1], sellers[3].Buyer);
            Assert.AreSame(buyers[1], sellers[4].Buyer);
            Assert.AreSame(buyers[1], sellers[5].Buyer);

            Assert.AreEqual(3, buyers[2].SellersConfirmed.Count);
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[0]));
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[1]));
            Assert.IsTrue(buyers[2].SellersConfirmed.Contains(sellers[2]));
            Assert.AreSame(buyers[2], sellers[0].Buyer);
            Assert.AreSame(buyers[2], sellers[1].Buyer);
            Assert.AreSame(buyers[2], sellers[2].Buyer);
        }

        #region Utility Functions to Create Buyers/Sellers
        public List<Buyer> CreateBuyers(int count)
        {
            List<Buyer> returnValue = new List<Buyer>();

            for (int i = 0; i < count; i++)
            {
                returnValue.Add(new Buyer(string.Format("Buyer #{0}", i)));
            }

            return returnValue;
        }

        public List<Seller> CreateSellers(int count)
        {
            List<Seller> returnValue = new List<Seller>();

            for (int i = 0; i < count; i++)
            {
                returnValue.Add(new Seller(string.Format("Seller #{0}", i)));
            }

            return returnValue;
        }
        #endregion
    }
}
