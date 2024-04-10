using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Random rnd = new Random();
            bool jakaa = false;

            //start the game loop
            do
            {
                int[] omat = new int[21];
                int[] PC = new int[21];

                int numero;
                int numeroAI = 0;
                int summa = 0;
                int summaAI = 0;

                string kuvaKortit;

                int i = 2;

                //deal first 2 cards
                for (int ii = 0; ii < 2; ii++)
                {
                    numero = rnd.Next(2, 15);

                    switch (numero)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            Console.WriteLine("Sait kortin " + numero);
                            break;

                        case 11:
                            numero = 10;
                            kuvaKortit = "Jätkä";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 12:
                            numero = 10;
                            kuvaKortit = "Kuningatar";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 13:
                            numero = 10;
                            kuvaKortit = "Kuningas";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 14:
                            kuvaKortit = "Ässä";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            Console.WriteLine("Haluatko että Ässä on 1 vai 11");
                            int ässä = int.Parse(Console.ReadLine());
                            numero = ässä;
                            break;
                    }

                    omat[ii] = numero;
                    summa += omat[ii];

                    //deal for dealer
                    numeroAI = rnd.Next(2, 15);

                    switch (numeroAI)
                    {
                        case 11:
                            numeroAI = 10;
                            break;

                        case 12:
                            numeroAI = 10;
                            break;

                        case 13:
                            numeroAI = 10;
                            break;

                        case 14:
                            if (summaAI + 11 > 21)
                                numeroAI = 1;
                            else
                                numeroAI = 11;

                            break;
                    }

                    PC[ii] = numeroAI;
                    summaAI += PC[ii];
                }

                //display cards
                for (int a = 0; a < omat.Length; a++)
                {
                    if (omat[a] < 0)
                        break;

                    Console.Write(omat[a]);
                    Console.Write(", ");
                }

                Console.WriteLine("Yhteensä " + summa);

                //start loop to continue drawing cards
                while (summa < 21)
                {

                    Console.Write("Haluatko uuden kortin: ");
                    string jatko = Console.ReadLine();

                    //if calls
                    if (!jatko.StartsWith("kyl"))
                    {
                        for (int ii = 0; ii < omat.Length; ii++)
                        {
                            if (omat[ii] < 0)
                                break;

                            Console.Write(omat[ii]);
                            Console.Write(", ");
                        }
                        Console.WriteLine("Yhteensä " + summa);
                        break;
                    }

                    numero = rnd.Next(2, 15);

                    switch (numero)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            Console.WriteLine("Sait kortin " + numero);
                            break;

                        case 11:
                            numero = 10;
                            kuvaKortit = "Jätkä";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 12:
                            numero = 10;
                            kuvaKortit = "Kuningatar";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 13:
                            numero = 10;
                            kuvaKortit = "Kuningas";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            break;

                        case 14:
                            kuvaKortit = "Ässä";
                            Console.WriteLine("Sait kortin " + kuvaKortit);
                            Console.WriteLine("Haluatko että Ässä on 1 vai 11");
                            int ässä = int.Parse(Console.ReadLine());
                            numero = ässä;
                            break;
                    }

                    omat[i] = numero;
                    summa += omat[i];

                    for (int ii = 0; ii < omat.Length; ii++)
                    {
                        if (omat[ii] < 0)
                            break;

                        Console.Write(omat[ii]);
                        Console.Write(", ");
                    }

                    Console.WriteLine("Yhteensä " + summa);

                    if (summa == 21)
                    {
                        Console.WriteLine("BLACKJACK!");
                    }
                    else if (summa > 21)
                    {
                        Console.WriteLine("Yli meni");
                    }

                    i++;
                }

                i = 2;
                //start loop for AI to draw
                while (summaAI < 17 && summa < 22)
                {

                    numeroAI = rnd.Next(2, 15);
                    switch (numeroAI)
                    {
                        case 11:
                            numeroAI = 10;
                            break;

                        case 12:
                            numeroAI = 10;
                            break;

                        case 13:
                            numeroAI = 10;
                            break;

                        case 14:
                            if (summaAI + 11 > 21)
                                numeroAI = 1;
                            else
                                numeroAI = 11;

                            break;
                    }

                    PC[i] = numeroAI;
                    summaAI += PC[i];
                    i++;
                }

                Console.Clear();
                Console.Write("Korttisi: ");

                for (int ii = 0; ii < omat.Length; ii++)
                {
                    if (omat[ii] > 0)
                    {
                        Console.Write(omat[ii]);
                        Console.Write(", ");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine("Yhteensä " + summa);

                //calculate winner
                if (summa > summaAI && summa < 22 || summa < 22 && summaAI>21 || summa >21 && summa < summaAI)
                    Console.WriteLine("Hyvä sinä voitit. sinulla on {0} ja vastustajalla {1}", summa, summaAI);

                else if (summa == summaAI)
                {

                    int korttiMäärä = 0;
                    int korttiMääräAI = 0;

                    for (int ii = 0; ii < omat.Length; ii++)
                    {
                        if (omat[ii] < 0)
                            break;

                        korttiMäärä++;
                    }

                    for (int ii = 0; ii < omat.Length; ii++)
                    {
                        if (PC[ii] > 0)
                            break;

                        korttiMääräAI++;
                    }

                    if (korttiMäärä < korttiMääräAI)
                        Console.WriteLine("Hyvä sinä voitit koska sinulla on vähemmän kortteja. sinulla on {0} ja vastustajalla {1}", summa, summaAI);

                    else if (korttiMäärä == korttiMääräAI)
                        Console.WriteLine("Tasapeli. sinulla on {0} ja vastustajalla {1}", summa, summaAI);

                    else
                        Console.WriteLine("Harmi hävisit. sinulla on {0} ja vastustajalla {1}", summa, summaAI);
                }
                else
                    Console.WriteLine("Harmi hävisit. sinulla on {0} ja vastustajalla {1}", summa, summaAI);

                Console.WriteLine();

                //wants to play another
                Console.Write("Haluatko pelata toisen: ");
                string jatkaa = Console.ReadLine();

                if (jatkaa.StartsWith("e"))
                    jakaa = true;
                else if(jatkaa.StartsWith("kyl"))
                    jakaa = false;

                Console.Clear();

            } while (jakaa == false);
        }
    }
}
