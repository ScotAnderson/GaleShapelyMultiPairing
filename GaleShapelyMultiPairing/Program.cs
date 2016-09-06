using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaleShapelyMultiPairing
{
    class Program
    {
        static void Main(string[] args)
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
            foreach (Buyer b in buyers)
            {
                Console.WriteLine(string.Format("Buyer: {0}, Sellers: {1}", b, string.Join(", ", b.SellersConfirmed)));
            }

            foreach (Seller s in sellers)
            {
                Console.WriteLine(string.Format("Seller: {0}, Buyer: {1}", s, s.Buyer));
            }

            Console.WriteLine("Press a key to close.");
            Console.ReadKey();
        }

        static List<Buyer> CreateBuyers(int count)
        {
            List<Buyer> returnValue = new List<Buyer>();

            for (int i = 0; i < count; i++)
            {
                returnValue.Add(new Buyer(string.Format("Buyer #{0}", i)));
            }

            return returnValue;
        }

        static List<Seller> CreateSellers(int count)
        {
            List<Seller> returnValue = new List<Seller>();

            for (int i = 0; i < count; i++)
            {
                returnValue.Add(new Seller(string.Format("Seller #{0}", i)));
            }

            return returnValue;
        }
    }
}
