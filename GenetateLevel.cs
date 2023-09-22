using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maximum_Rotation
{


    public class GenetateLevel
    {
        const int N = 3;

        int[,] massiv = new int[N, N] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        private int indexM = 0;
        private int indexN = 0;
        Random rnd = new Random();
        //Form1 f = new Form1();

        public List<int[,]> list = new List<int[,]>();

        public int step = 0;

        public GenetateLevel() { }

        public GenetateLevel(Form1 form)
        {
            //f = form;
        }


        private int[,] povorotlevo(int[,] mas)
        {
            var tmp = mas[indexN + 1, indexM + 1];
            mas[indexN + 1, indexM + 1] = mas[indexN + 1, indexM];
            mas[indexN + 1, indexM] = mas[indexN, indexM];
            mas[indexN, indexM] = mas[indexN, indexM + 1];
            mas[indexN, indexM + 1] = tmp;
            return mas;
        }

        private int[,] povorotPravo(int[,] mas)
        {
            var tmp = mas[indexN, indexM];
            mas[indexN, indexM] = mas[indexN + 1, indexM];
            mas[indexN + 1, indexM] = mas[indexN + 1, indexM + 1];
            mas[indexN + 1, indexM + 1] = mas[indexN, indexM + 1];
            mas[indexN, indexM + 1] = tmp;
            return mas;
        }

        private int[,] moveLeft(int[,] mas)
        {
            if (indexM == 0)
            { return mas; }

            indexM--;


            return mas;
        }
        private int[,] moveRight(int[,] mas)
        {
            if (indexM == N - 2)
            { return mas; }

            indexM++;

            return mas;
        }

        private int[,] moveTop(int[,] mas)
        {
            if (indexN == 0)
            { return mas; }

            indexN--;


            return mas;

        }

        private int[,] moveBottom(int[,] mas)
        {
            if (indexN == N - 2)
            { return mas; }

            indexN++;


            return mas;
        }


        private void Eguals(int[,] mas1)
        {
            bool flag = false;
            int nn = -1; // отнять  шагов
            if (list.Count >= 2)
            {
                for (int h = 0; h < list.Count; h++)
                {
                    int[,] mas = list.ElementAt(h);

                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (mas1[i, j] != mas[i, j])
                            {
                                flag = true;
                                break;
                                //return;
                            }
                        }
                        if (flag == true) break;

                    }
                    if (flag == false)
                    {
                        nn = h;

                        break;

                    }

                    flag = false;



                    //step -= 2;
                }

                if (nn == -1)
                    step++;
                else
                {
                    step -= list.Count - nn - 1;
                    list.RemoveRange(nn + 1, list.Count - nn - 1);
                }

            }
            else step++;

        }

        public int iii = 0;
        public int kolll = 0;

        public int[,] level()
        {
            step = 0;
            int[,] massivLevel = (int[,])massiv.Clone();

            int kolPovorot = rnd.Next(3, 7);
            kolll = kolPovorot;

            int kol = rnd.Next(500, 1000);
            list.Add((int[,])massiv.Clone());

            // massivLevel = povorotlevo(massivLevel); Eguals((int[,])massivLevel.Clone());
            //   massivLevel = povorotPravo(massivLevel); Eguals((int[,])massivLevel.Clone());


            for (int i = 0; i < kolPovorot; i++)
            {
                int povorot = rnd.Next(1, 3);
                switch (povorot)
                {
                    case 1: massivLevel = povorotlevo(massivLevel); Console.WriteLine("<"); break; Eguals((int[,])massivLevel.Clone()); break; //if (step == kolPovorot) return massivLevel; break;
                    case 2: massivLevel = povorotPravo(massivLevel); Console.WriteLine(">"); break; Eguals((int[,])massivLevel.Clone()); break; //if (step == kolPovorot) return massivLevel; break;
                    default: break;
                }
                if (i % 4 == 0)
                    massivLevel = moveRight(massivLevel);
                else if (i%4==1)
                    massivLevel = moveBottom(massivLevel);
                else if (i%4==2)
                    massivLevel = moveLeft(massivLevel);
                else if (i%4==3)
                    massivLevel = moveTop(massivLevel);

                //Form1 form = new Form1();

                //f.zapoln(massivLevel);

              //  Thread.Sleep(10000);

            }


            /*for (int i = 0; i < kol; i++)
            {
                switch (rnd.Next(1, 7))
                {
                    case 1: massivLevel = povorotlevo(massivLevel); Eguals((int[,])massivLevel.Clone()); if (step == kolPovorot) return massivLevel; iii++; break;
                    case 2: massivLevel = povorotPravo(massivLevel); Eguals((int[,])massivLevel.Clone()); if (step == kolPovorot) return massivLevel; iii++; break;
                    case 3: massivLevel = moveLeft(massivLevel); break;
                    case 4: massivLevel = moveRight(massivLevel); break;
                    case 5: massivLevel = moveTop(massivLevel); break;
                    case 6: massivLevel = moveBottom(massivLevel); break;


                    default:
                        break;
                }
            }*/

            return massivLevel;

        }
    }
}
