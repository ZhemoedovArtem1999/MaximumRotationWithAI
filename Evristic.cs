using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maximum_Rotation
{
    public class Evristic
    {
        QueuePriority open = new QueuePriority();
        QueuePriority closed = new QueuePriority();
        private HashSet<State> openSet = new HashSet<State>();
        private HashSet<State> closedSet = new HashSet<State>();
        private Stack<int[,]> solutionPath = new Stack<int[,]>();

        public int step = 0;
        public int iterations = 0;
        public int reveling = 0; // количество раскрытий
        public int revelated = 0;// количество по итогу раскрытых

        public int maxCondition = 0;
        public int maxOpen = 0;
        public int maxClose = 0;

        int h = 0;

        public Evristic()
        {

        }
        public Evristic(int h)
        {
            this.h = h;
        }

        public Stack<int[,]> getSolution(int[,] init)
        {

            State initialCondition = new State(null, init);

            if (h == 1)
            {
                initialCondition.h1();
            }
            else
            {
                initialCondition.h2();
            }


            // State Current = new State();
            State Child;
            bool Solve;
            Solve = false;

            open.Add(initialCondition);
            openSet.Add(initialCondition);

            while (open.Count() != 0 && Solve == false)
            {
                State Current = open.Dequeue();
                openSet.Remove(Current);
                if (Current.isCompleted())
                {
                    Solve = true;
                    return Current.getSolutionPath(solutionPath, ref step);
                }

                closed.Add(Current);
                closedSet.Add(Current);

                List<State> revealedCondition = Current.revealCondition(h);
                //condition.revealCondition(ref revealedCondition);
                reveling++;
                revelated += revealedCondition.Count;




                foreach (State child in revealedCondition)
                {
                    if (!openSet.Contains(child) && !closedSet.Contains(child))
                    {
                        open.Add(child);
                        openSet.Add(child);
                        //Console.WriteLine("Добавлен потомок");
                        // openSet.Add(child);
                    }
                    else
                    {
                        //Console.WriteLine("Вошел");
                        int temp;
                        if (open.Contains(child) != -1)
                        {
                            temp = open.Contains(child);
                            if (child.getF() < open.FindIndexAd(temp).getF())
                            {
                                openSet.Remove(open.FindIndexAd(temp));
                                open.RemoveAt(temp);
                                
                                open.Add(child);
                                openSet.Add(child);
                                ///////////////////////////////////////////////////
                            }
                        }

                        else if (closed.Contains(child) != -1)
                        {
                            temp = closed.Contains(child);
                            if (child.getF() < closed.FindIndexAd(temp).getF())
                            {
                                closedSet.Remove(closed.FindIndexAd(temp));
                                closed.RemoveAt(temp);

                                closed.Add(child);
                                closedSet.Add(child);
                                ///////////////////////////////////////////////////
                            }
                        }


                        //Console.WriteLine("Повтор\n");
                    }
                }
                iterations++;
               //Console.WriteLine(open.Count());
               // Thread.Sleep(300);

                if (open.Count() + closed.Count() > maxCondition)
                {
                    maxCondition = open.Count() + closed.Count();
                }

                if (open.Count() > maxOpen)
                {
                    maxOpen = open.Count();
                }
                if (closed.Count() > maxClose)
                {
                    maxClose = closed.Count();
                }
            }


            return new Stack<int[,]>();
        }
    }
}
