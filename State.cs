using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Maximum_Rotation
{
    public class State : IComparable
    {
        const int N = 3;
        const int CHILDREN = 8;

        private State parent;

        private int[,] condition;

        private int[,] resultMas = new int[N, N] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        int h;
        int g;

        public State()
        {
            g = 0;
            condition = new int[N, N];
        }

        public State(State parent, int[,] condition)
        {
            g = 0;
            this.parent = parent;
            this.condition = condition;
        }

        // h - эвристика
        public State(State parent, int[,] condition, int h)
        {
            g = parent.g+1;
            this.parent = parent;
            this.condition = condition;
            if (h == 1)
            {
                h1();
            }
            else if (h == 2)
            {
                h2();
            }
        }

        public bool isCompleted()
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (this.condition[i,j] != this.resultMas[i,j]) return false; 

            return true;
        }
        public Stack<int[,]> getSolutionPath(Stack<int[,]> solutionPath, ref int step)
        {
            solutionPath.Push(this.condition);
            if (parent == null)
            {
                return solutionPath;
            }
            step++;
            return parent.getSolutionPath(solutionPath, ref step);
        }

        public Queue<int[,]> getSolutionPath1(Queue<int[,]> solutionPath, ref int step)
        {
            solutionPath.Enqueue(this.condition);
            if (parent == null)
            {
                return solutionPath;
            }
            step++;
            return parent.getSolutionPath1(solutionPath, ref step);
        }

        private int[,] povorotlevo(int[,] mas, int indexN, int indexM)
        {
            var tmp = mas[indexN + 1, indexM + 1];
            mas[indexN + 1, indexM + 1] = mas[indexN + 1, indexM];
            mas[indexN + 1, indexM] = mas[indexN, indexM];
            mas[indexN, indexM] = mas[indexN, indexM + 1];
            mas[indexN, indexM + 1] = tmp;
            return mas;
        }

        private int[,] povorotPravo(int[,] mas, int indexN, int indexM)
        {
            var tmp = mas[indexN, indexM];
            mas[indexN, indexM] = mas[indexN + 1, indexM];
            mas[indexN + 1, indexM] = mas[indexN + 1, indexM + 1];
            mas[indexN + 1, indexM + 1] = mas[indexN, indexM + 1];
            mas[indexN, indexM + 1] = tmp;
            return mas;
        }



        public List<State> revealCondition()
        {
            int indexM = 0, indexN = 0;
            List<State> result = new List<State>();
            //result = new List<Condition>();
            for (int i = 0; i < CHILDREN; i++)
            {
                int[,] mas;

                if (i % 2 == 0)
                {
                    mas = povorotlevo((int[,])condition.Clone(), indexN, indexM);
                }
                else
                {
                    mas = povorotPravo((int[,])condition.Clone(), indexN, indexM);
                }
                State conditionDaungher = new State(this, mas);
                result.Add(conditionDaungher);


                if (i == 1)
                {
                    indexM++;
                }
                if (i == 3)
                {
                    indexN++;
                    indexM--;
                }
                if (i == 5)
                {
                    indexM++;
                }

            }



            return result;
        }

        // h - эвристика
        public List<State> revealCondition(int h)
        {
            int indexM = 0, indexN = 0;
            List<State> result = new List<State>();
            //result = new List<Condition>();
            for (int i = 0; i < CHILDREN; i++)
            {
                int[,] mas;

                if (i % 2 == 0)
                {
                    mas = povorotlevo((int[,])condition.Clone(), indexN, indexM);
                }
                else
                {
                    mas = povorotPravo((int[,])condition.Clone(), indexN, indexM);
                }
                State conditionDaungher = new State(this, mas, h);
                result.Add(conditionDaungher);


                if (i == 1)
                {
                    indexM++;
                }
                if (i == 3)
                {
                    indexN++;
                    indexM--;
                }
                if (i == 5)
                {
                    indexM++;
                }

            }



            return result;
        }


        public override int GetHashCode()
        {
            string str = "";
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    str += this.condition[i, j];

                    return int.Parse(str);

        }

        public override bool Equals(object obj)
        {
            int[,] masNew = ((State)obj).condition;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (masNew[i, j] != this.condition[i, j])
                        return false;



            return true;
        }

        public void h1()
        {
            
            h = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (condition[i, j] != resultMas[i, j])
                    {
                        h++;
                    }
                }
            }
            ///////////////
            if (h % 4 == 0)
            {
                h = h / 4;
            }
            else
            {
                double t = h;
                t = t / 4;
                t -= h / 4;
                if (t < 0.5)
                {
                    h = h / 4;
                }
                else
                {
                    h = h / 4 + 1;
                }
            }
        }

        public void h2()
        {            
            
            h = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    for (int i1 = 0; i1 < N; i1++)
                    {
                        for (int j1 = 0; j1 < N; j1++)
                        {
                            if (condition[i, j] == resultMas[i1, j1])
                            {
                                h += Math.Abs(i - i1) + Math.Abs(j - j1);
                            }
                        }
                    }
                }
            }
            ////////////////////
            if (h % 4 == 0)
            {
                h = h / 4;
            }
            else
            {
                double t = h;
                t = t / 4;
                t -= h / 4;
                if (t < 0.5)
                {
                    h = h / 4;
                }
                else
                {
                    h = h / 4 + 1;
                }
            }
        }
        public int getH()
        { return h; }

        public int getF()
        {
            //Console.WriteLine((h+g).ToString());
            return h + g;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }


    }
}
