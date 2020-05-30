using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace L7_2
{
    class ComparingETC : IComparable
    {
        public int Number;
        public string Title;
        public double Weight;
        public int Cost;
        public int Price;
        public ComparingETC(int produceNumber, string title, double weight, int productionCost, int sellingPrice)
        {
            this.Number = produceNumber;
            this.Title = title;
            this.Weight = weight;
            this.Cost = productionCost;
            this.Price = sellingPrice;
        }
        public int CompareTo(object com)
        {
            ComparingETC c = (ComparingETC)com;
            if (this.Price > c.Price) return 1;
            if (this.Price < c.Price) return -1;
            return 0;
        }
        public class SortByWeight : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                ComparingETC c1 = (ComparingETC)ob1;
                ComparingETC c2 = (ComparingETC)ob2;
                if (c1.Weight > c2.Weight) return 1;
                if (c1.Weight < c2.Weight) return -1;
                return 0;
            }
        }
        public class SortByCost : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                ComparingETC c1 = (ComparingETC)ob1;
                ComparingETC c2 = (ComparingETC)ob2;
                if (c1.Cost > c2.Cost) return 1;
                if (c1.Cost < c2.Cost) return -1;
                return 0;
            }
        }
        virtual public string Passport()
        {

            return (Number + "\t\t\t" + Title + "\t\t\t" + Weight + "\t\t\t$" + Cost + "\t\t\t$" + Price);
        }
    }
    class ProductRange
    {
        public static int count = Sizing() - 1;
        public int[] ProductNumber = new int[count];
        public string[] Title = new string[count];
        public double[] Weight = new double[count];
        public int[] ProductionCost = new int[count];
        public int[] SellingPrice = new int[count];
        public string Heading;
        List<ProductRange> prod = new List<ProductRange>();
        public ProductRange()
        {
            
        }
        public ProductRange(int n, int produceNumber, string title, double weight, int productionCost, int sellingPrice)
        {
            if (produceNumber < 0) throw new System.Exception("Produce number is incorrect.");
            else ProductNumber[n] = produceNumber;
            if (title.Length == 0) throw new System.Exception("Title is not found.");
            else Title[n] = title;
            if (weight < 0) throw new System.Exception("Weight is incorrect.");
            else Weight[n] = weight;
            if (productionCost < 0) throw new System.Exception("Production cost is incorrect.");
            else ProductionCost[n] = productionCost;
            if (sellingPrice < 0) throw new System.Exception("Selling price is incorrect.");
            else SellingPrice[n] = sellingPrice;
        }
        public static int Sizing()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int size = lines.Length;
            f.Close();
            return size;
        }
        public void Exiting()
        {
            string ex = "";
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length > ex.Length) ex = Title[b];
            }
            for (int b = 0; b < Title.Length; b++)
            {
                if (Title[b].Length != ex.Length)
                {
                    int dif = ex.Length - Title[b].Length;
                    for (int a = 0; a < dif; a++) Title[b] += " ";
                }
            }
            string joy = "";
            Console.WriteLine("\nEnter 'p' to sort by price and 'e' to exit. ");
            do
            {
                joy = Console.ReadLine();
                switch (joy)
                {
                    case "e": break;
                    case "p":
                        {
                            string oy = "";
                            SortingPrice();
                            Console.WriteLine("\nEnter 'w' to sort by weight, 'c' to sort by production cost and 'e' to exit. ");
                            do
                            {
                                oy = Console.ReadLine();
                                switch (oy)
                                {
                                    case "e": break;
                                    case "w":
                                        {
                                            string woy = "";
                                            SortingWeight();
                                            Console.WriteLine("\nEnter 'c' to sort by production cost and 'e' to exit. ");
                                            do
                                            {
                                                woy = Console.ReadLine();
                                                switch (woy)
                                                {
                                                    case "e": break;
                                                    case "c": SortingCost(); break;
                                                    default: Console.WriteLine("Try again. ('c' to sort by production cost and 'e' to exit)"); break;
                                                }
                                            } while (woy != "e");
                                            break;
                                        }
                                    case "c":
                                        {
                                            string coy = "";
                                            SortingCost();
                                            Console.WriteLine("\nEnter 'w' to sort by weight and 'e' to exit. ");
                                            do
                                            {
                                                coy = Console.ReadLine();
                                                switch (coy)
                                                {
                                                    case "e": break;
                                                    case "w": SortingWeight(); break;
                                                    default: Console.WriteLine("Try again. ('w' to sort by weight and 'e' to exit)"); break;
                                                }
                                            } while (coy != "e");
                                            break;
                                        }
                                    default: Console.WriteLine("Try again. ('w' to sort by weight, 'c' to sort by production cost and 'e' to exit)"); break;
                                }
                            } while (oy != "e");
                            break;
                        }
                    default: Console.WriteLine("Try again. ('p' to sort by price and 'e' to exit)"); break;
                }
            } while (joy != "e");
        }
        public void Opening()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Open);
            using var f = new StreamReader(fileStream);
            string mains = f.ReadToEnd();
            string[] lines = mains.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Heading = lines[0];
            f.Close();
            int count = lines.Length;
            int t = 0;
            for (int i = 1; i < count; i++)
            {
                if (lines[i].Length > t) t = lines[i].Length;

            }
            char[,] temps = new char[t, count - 1];
            for (int i = 1; i < count; i++)
            {
                char[] tem = lines[i].ToCharArray();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    temps[j, i - 1] = tem[j];
                }
            }
            bool maybe = false;
            bool start = false;
            bool go = false;
            bool yes = false;
            string prodd = "";
            string worr = "";
            string pagg = "";
            string coss = "";
            string numm = "";
            for (int i = 1; i < count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!maybe)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            ProductNumber[i - 1] = Int32.Parse(prodd);
                            maybe = true;
                            j += 2;
                        }
                        if (char.IsNumber(temps[j, i - 1])) prodd = prodd + temps[j, i - 1];
                    }
                    else if (!start)
                    {

                        if (temps[j, i - 1] == '\t')
                        {
                            start = true;
                            j += 2;
                        }
                        else if (!(char.IsControl(temps[j, i - 1])) && (!(char.IsWhiteSpace(temps[j, i - 1])))) worr = worr + temps[j, i - 1];
                    }
                    else if (!go)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            go = true;
                            j += 2;
                        }
                        if (char.IsNumber(temps[j, i - 1]) || temps[j, i - 1] == ',') pagg = pagg + temps[j, i - 1];
                    }
                    else if (!yes)
                    {
                        if (temps[j, i - 1] == '\t')
                        {
                            yes = true;
                            j += 2;
                        }
                        if (char.IsNumber(temps[j, i - 1])) coss = coss + temps[j, i - 1];
                    }
                    else
                    {
                        if (char.IsNumber(temps[j, i - 1])) numm = numm + temps[j, i - 1];
                        if (j == lines[i].Length - 1)
                        {
                            ProductNumber[i - 1] = Int32.Parse(prodd);
                            Title[i - 1] = worr;
                            Weight[i - 1] = Double.Parse(pagg);
                            ProductionCost[i - 1] = Int32.Parse(coss);
                            SellingPrice[i - 1] = Int32.Parse(numm);
                            prod.Add(new ProductRange(i - 1, Int32.Parse(prodd), worr, Double.Parse(pagg), Int32.Parse(coss), Int32.Parse(numm)));
                            numm = "";
                            prodd = "";
                            worr = "";
                            pagg = "";
                            coss = "";
                            maybe = false;
                            start = false;
                            go = false;
                            yes = false;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                Console.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
           
            }

        }
        public void Adding(int produceNumber, string title, double weight, int productionCost, int sellingPrice)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Array.Resize(ref ProductNumber, ProductNumber.Length + 1);
            Array.Resize(ref Title, Title.Length + 1);
            Array.Resize(ref Weight, Weight.Length + 1);
            Array.Resize(ref ProductionCost, ProductionCost.Length + 1);
            Array.Resize(ref SellingPrice, SellingPrice.Length + 1);
            ProductNumber[ProductNumber.Length - 1] = produceNumber;
            Title[Title.Length - 1] = title;
            Weight[Weight.Length - 1] = weight;
            ProductionCost[ProductionCost.Length - 1] = productionCost;
            SellingPrice[SellingPrice.Length - 1] = sellingPrice;
            prod.Add(new ProductRange(ProductNumber.Length - 1, produceNumber, title, weight, productionCost, sellingPrice));
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                }
                else f.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                Console.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
            }
            f.Close();
        }
        public void Searching(int q)
        {
            if (q >= 0)
            {
                Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
                Console.WriteLine(ProductNumber[q] + "\t\t\t" + Title[q] + "\t\t\t" + Weight[q] + "\t\t\t$" + ProductionCost[q] + "\t\t\t$" + SellingPrice[q]);
                Console.WriteLine("____________________________________________________________________________________________________________________________________\n");
            }
            else Console.WriteLine(Heading);
        }
        public void Editing(int n, int produceNumber, string title, double weight, int productionCost, int sellingPrice)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            if (produceNumber < 0) throw new System.Exception("Produce number is incorrect.");
            if (title.Length == 0) throw new System.Exception("Title is not found.");
            if (weight < 0) throw new System.Exception("Weight is incorrect.");
            if (productionCost < 0) throw new System.Exception("Production cost of pages is incorrect.");
            if (sellingPrice < 0) throw new System.Exception("Selling price of pages is incorrect.");
            ProductNumber[n] = produceNumber;
            Title[n] = title;
            Weight[n] = weight;
            ProductionCost[n] = productionCost;
            SellingPrice[n] = sellingPrice;
            prod.Insert(n, new ProductRange(n, produceNumber, title, weight, productionCost, sellingPrice));
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                }
                else f.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                Console.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
            }
            f.Close();
        }
        public void Deleting(int n)
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            int[] aa = ProductNumber;
            string[] a = Title;
            double[] ar = Weight;
            int[] arr = ProductionCost;
            int[] aaa = SellingPrice;
            bool start = false;
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == n) start = true;
                if (i == ProductNumber.Length - 1)
                {
                    Array.Resize(ref aa, aa.Length - 1);
                    Array.Resize(ref a, a.Length - 1);
                    Array.Resize(ref ar, ar.Length - 1);
                    Array.Resize(ref arr, arr.Length - 1);
                    Array.Resize(ref aaa, aaa.Length - 1);
                    ProductNumber = aa;
                    Title = a;
                    Weight = ar;
                    ProductionCost = arr;
                    SellingPrice = aaa;
                    break;
                }
                if (start)
                {
                    ProductNumber[i] = ProductNumber[i + 1];
                    Title[i] = Title[i + 1];
                    Weight[i] = Weight[i + 1];
                    ProductionCost[i] = ProductionCost[i + 1];
                    SellingPrice[i] = SellingPrice[i + 1];
                }
            }
            prod.RemoveAt(n);
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                }
                else f.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
                Console.WriteLine(ProductNumber[i] + "\t\t\t" + Title[i] + "\t\t\t" + Weight[i] + "\t\t\t$" + ProductionCost[i] + "\t\t\t$" + SellingPrice[i]);
            }
            f.Close();
        }
        public void SortingPrice()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Console.WriteLine("\n____________________________________________________________________________________________________________________________________\n");
            Console.WriteLine("Sorting by selling price: ");
            ComparingETC[] group = new ComparingETC[ProductNumber.Length];
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                group[i] = new ComparingETC(ProductNumber[i], Title[i], Weight[i], ProductionCost[i], SellingPrice[i]);
            }
            Array.Sort(group);
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(group[i].Passport());
                }
                else f.WriteLine(group[i].Passport());
                Console.WriteLine(group[i].Passport());
            }
            f.Close();
        }
        public void SortingWeight()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Console.WriteLine("\n____________________________________________________________________________________________________________________________________\n");
            Console.WriteLine("Sorting by weight: ");
            ComparingETC[] form = new ComparingETC[ProductNumber.Length];
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                form[i] = new ComparingETC(ProductNumber[i], Title[i], Weight[i], ProductionCost[i], SellingPrice[i]);
            }
            Array.Sort(form, new ComparingETC.SortByWeight());
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(form[i].Passport());
                }
                else f.WriteLine(form[i].Passport());
                Console.WriteLine(form[i].Passport());
            }
            f.Close();
        }
        public void SortingCost()
        {
            var fileStream = new FileStream(@"text.txt", FileMode.Truncate);
            using var f = new StreamWriter(fileStream);
            Console.WriteLine("\n____________________________________________________________________________________________________________________________________\n");
            Console.WriteLine("Sorting by production cost: ");
            ComparingETC[] broth = new ComparingETC[ProductNumber.Length];
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                broth[i] = new ComparingETC(ProductNumber[i], Title[i], Weight[i], ProductionCost[i], SellingPrice[i]);
            }
            Array.Sort(broth, new ComparingETC.SortByCost());
            Console.WriteLine(Heading);
            f.WriteLine(Heading);
            for (int i = 0; i < ProductNumber.Length; i++)
            {
                if (i == ProductionCost.Length - 1)
                {
                    f.Write(broth[i].Passport());
                }
                else f.WriteLine(broth[i].Passport());
                Console.WriteLine(broth[i].Passport());
            }

            f.Close();
        }

    }
    class Program
    {
        static void Main()
        {
            ProductRange r = new ProductRange();
            string task = "";
            string func = "";
            string choice = "";
            string nu = "";
            string ti = "";
            string we = "";
            string po = "";
            string se = "";
            int nuo = -1;
            double weo = -1;
            int poo = -1;
            int seo = -1;
            do
            {
                Console.WriteLine("\nEnter 'o' to open the file and 'e' to exit.");
                task = Console.ReadLine();
                switch (task)
                {
                    case "e": break;
                    case "o":
                        {
                            r.Opening();
                            Console.WriteLine("\n");
                            do
                            {
                                Console.WriteLine("\n'a' to add new information, 's' to search for editing and deleting and 'e' to exit.");
                                func = Console.ReadLine();
                                switch (func)
                                {
                                    case "e": r.Exiting(); task = "e"; break;
                                    case "a":
                                        {
                                            do
                                            {
                                                Console.WriteLine("\nEnter the production number: ");
                                                nu = Console.ReadLine();
                                                nuo = Int32.Parse(nu);
                                            } while (nuo < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the title: ");
                                                ti = Console.ReadLine();
                                            } while (ti.Length == 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the weight: ");
                                                we = Console.ReadLine();
                                                weo = Double.Parse(we);
                                            } while (weo < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the production cost: ");
                                                po = Console.ReadLine();
                                                poo = Int32.Parse(po);
                                            } while (poo < 0);
                                            do
                                            {
                                                Console.WriteLine("\nEnter the selling price: ");
                                                se = Console.ReadLine();
                                                seo = Int32.Parse(se);
                                            } while (seo < 0);

                                            r.Adding(nuo, ti, weo, poo, seo);
                                            break;
                                        }
                                    case "s":
                                        {
                                            int strings = -1;
                                            string strins = "";
                                            do
                                            {
                                                Console.WriteLine("\nEnter the number of string you need: ");
                                                strins = Console.ReadLine();
                                                strings = Int32.Parse(strins);
                                            } while (strings < 1);
                                            strings = strings - 1;
                                            r.Searching(strings);
                                            do
                                            {
                                                Console.WriteLine("\nEnter 'e' to edit, 'd' to delete and 'r' to return.");
                                                choice = Console.ReadLine();
                                                switch (choice)
                                                {
                                                    case "r": break;
                                                    case "e":
                                                        {
                                                            string nn = "";
                                                            string tt = "";
                                                            string ww = "";
                                                            string pp = "";
                                                            string ss = "";
                                                            int nno = -1;
                                                            double wwo = -1;
                                                            int ppo = -1;
                                                            int sso = -1;
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the production number you want to replace the previous one: ");
                                                                nn = Console.ReadLine();
                                                                nno = Int32.Parse(nn);
                                                            } while (nno < 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the title you want to replace the previous one: ");
                                                                tt = Console.ReadLine();
                                                            } while (tt.Length == 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the weight you want to replace the previous one: ");
                                                                ww = Console.ReadLine();
                                                                wwo = Double.Parse(ww);
                                                            } while (wwo < 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the production cost you want to replace the previous one: ");
                                                                pp = Console.ReadLine();
                                                                ppo = Int32.Parse(pp);
                                                            } while (ppo < 0);
                                                            do
                                                            {
                                                                Console.WriteLine("\nEnter the selling price you want to replace the previous one: ");
                                                                ss = Console.ReadLine();
                                                                sso = Int32.Parse(ss);
                                                            } while (sso < 0);
                                                            r.Editing(strings, nno, tt, wwo, ppo, sso);
                                                            break;
                                                        }
                                                    case "d":
                                                        {
                                                            r.Deleting(strings);
                                                            choice = "r";
                                                            break;
                                                        }
                                                    default: Console.WriteLine("Try again. ('e' to edit, 'd' to delete and 'r' to return)"); break;
                                                }
                                            } while (choice != "r");
                                            break;
                                        }
                                    default: Console.WriteLine("Try again. ('a' to add, 's' to search and 'e' to exit)"); break;
                                }
                            } while (func != "e");
                            break;
                        }
                    default: Console.WriteLine("Try again. ('o' to open file and 'e' to exit)"); break;
                }
            } while (task != "e");
        }
    }
}