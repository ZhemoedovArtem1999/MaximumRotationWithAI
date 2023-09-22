using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maximum_Rotation
{
    internal class AIdouble
    {

        private Queue<State> open1 = new Queue<State>();

        private HashSet<State> openSet1 = new HashSet<State>();
        private HashSet<State> closed1 = new HashSet<State>();

        private Queue<State> open2 = new Queue<State>();

        private HashSet<State> openSet2 = new HashSet<State>();
        private HashSet<State> closed2 = new HashSet<State>();


        private Stack<int[,]> solutionPath = new Stack<int[,]>();
        private Queue<int[,]> solutionPath1 = new Queue<int[,]>();

        public int step = 0;
        public int iterations = 0;
        public int reveling = 0; // количество раскрытий
        public int revelated = 0;// количество по итогу раскрытых

        public int maxCondition = 0;
        public int maxOpen = 0;
        public int maxClose = 0;


        public Stack<int[,]> getSolution(int[,] init, int[,] result)
        {
            Queue<int[,]> solution = new Queue<int[,]>();
            Stack<int[,]> solution1 = new Stack<int[,]>();
            Stack<int[,]> solutionRes = new Stack<int[,]>();
            State initialCondition = new State(null, init);

            open1.Enqueue(initialCondition);
            openSet1.Add(initialCondition);
            initialCondition = new State(null, result);
            open2.Enqueue(initialCondition);
            openSet2.Add(initialCondition);

            while (open1.Count != 0)
            {
                State condition = open1.Dequeue();
                if (condition.isCompleted())
                {
                    return condition.getSolutionPath(solutionPath, ref step);
                }
                closed1.Add(condition);
                openSet1.Remove(condition);

                List<State> revealedCondition = condition.revealCondition();
                //condition.revealCondition(ref revealedCondition);
                reveling++;
                revelated += revealedCondition.Count;


                foreach (State child in revealedCondition)
                {
                    if (!openSet1.Contains(child) && !closed1.Contains(child))
                    {
                        open1.Enqueue(child);
                        openSet1.Add(child);
                    }
                    else
                    {
                        //Console.WriteLine("Повтор\n");
                    }
                }




                condition = open2.Dequeue();
                //bool flag = false;

                if (closed1.Contains(condition))
                {
                    State condN = new State(null, null); ; // состояние для построения дерева с начального состояния
                    foreach(State cond in closed1)
                    {
                        if (condition.Equals(cond))
                        {
                            condN= cond;
                            break;
                        }
                    }
                    // очередь
                    solution = condN.getSolutionPath1(solutionPath1, ref step);
                    //стек
                    solution1 = condition.getSolutionPath(solutionPath, ref step);
                    solution.Dequeue();

                    int n = solution1.Count;

                    for (int i = 0; i < n; i++)
                    {
                        solutionRes.Push(solution1.Pop());
                    }
                    n = solution.Count;
                    for (int i = 0; i < n; i++)
                    {
                        solutionRes.Push(solution.Dequeue());
                    }

                    
                    return solutionRes;
                }


            

                //if (condition.isCompleted())
                //{
                //    return condition.getSolutionPath(solutionPath, ref step);
                //}
                closed2.Add(condition);
                openSet2.Remove(condition);

                revealedCondition = condition.revealCondition();
                //condition.revealCondition(ref revealedCondition);
                reveling++;
                revelated += revealedCondition.Count;


                foreach (State child in revealedCondition)
                {
                    if (!openSet2.Contains(child) && !closed2.Contains(child))
                    {
                        open2.Enqueue(child);
                        openSet2.Add(child);
                    }
                    else
                    {
                        //Console.WriteLine("Повтор\n");
                    }
                }




                iterations++;

                if (openSet1.Count + closed1.Count > maxCondition)
                {
                    maxCondition = openSet1.Count + closed1.Count;
                }

                if (openSet1.Count > maxOpen)
                {
                    maxOpen = openSet1.Count;
                }
                if (closed1.Count > maxClose)
                {
                    maxClose = closed1.Count;
                }


                //Console.WriteLine(open1.Count + "\n");

                //Form1.textBox1.Text = open.Count().ToString();

            }

            return new Stack<int[,]>();
        }
    }
}
