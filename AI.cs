using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Maximum_Rotation
{
    internal class AI
    {

        private Queue<State> open = new Queue<State>();

        private HashSet<State> openSet = new HashSet<State>();
        private HashSet<State> closed = new HashSet<State>();

        private Stack<int[,]> solutionPath = new Stack<int[,]>();

        public int step = 0;
        public int iterations = 0;
        public int reveling = 0; // количество раскрытий
        public int revelated = 0;// количество по итогу раскрытых

        public int maxCondition = 0;
        public int maxOpen = 0;
        public int maxClose = 0;


        public async Task<Stack<int[,]>> getSolution(int[,] init)
        {
            
            State initialCondition = new State(null, init);

            open.Enqueue(initialCondition);
            openSet.Add(initialCondition);
            while (open.Count != 0)
            {
                State condition = open.Dequeue();
                if (condition.isCompleted())
                {
                    await Task.Delay(0);
                    return condition.getSolutionPath(solutionPath, ref step);
                }
                closed.Add(condition);
                openSet.Remove(condition);

                List<State> revealedCondition = condition.revealCondition();
                //condition.revealCondition(ref revealedCondition);
                reveling++;
                revelated += revealedCondition.Count;


                foreach (State child in revealedCondition)
                {
                    if (!openSet.Contains(child) && !closed.Contains(child))
                    {
                        open.Enqueue(child);
                        openSet.Add(child);
                    }
                    else
                    {
                        //Console.WriteLine("Повтор\n");
                    }
                }
                iterations++;

                if (openSet.Count + closed.Count > maxCondition)
                {
                    maxCondition = openSet.Count + closed.Count;
                }

                if (openSet.Count > maxOpen)
                {
                    maxOpen = openSet.Count;
                }
                if (closed.Count > maxClose)
                {
                    maxClose = closed.Count;
                }


                //Console.WriteLine(open.Count + "\n");

                //Form1.textBox1.Text = open.Count().ToString();

            }
            await Task.Delay(0);
            return new Stack<int[,]>();
        }

    }
}
