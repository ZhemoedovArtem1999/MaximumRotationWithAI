using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Maximum_Rotation
{
    internal class QueuePriority
    {
        List<State> Queue;
        public QueuePriority()
        {
            Queue = new List<State>();
        }
        public void Add(State elem)
        {
            if (Queue.Count > 0)
            {
                if (Queue[Queue.Count - 1].getF() <= elem.getF())
                    Queue.Add(elem);
                else
                    for (int i = 0; i < Queue.Count; i++)
                    {
                        if (Queue[i].getF() > elem.getF())
                        {
                            Queue.Insert(i, elem);
                            break;
                        }
                    }
            }
            else
            {
                Queue.Add(elem);
            }
        }
        public State Dequeue()
        {
            State First = Queue[0];
            Queue.RemoveAt(0);
            return First;
        }

        public int Count()
        {
            return Queue.Count;
        }

        public int Contains(State elem)
        {
            if (Queue.Count != 0)
            {
                for (int i = 0; i < Queue.Count; i++)
                {
                    if (Queue[i].Equals(elem))
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
                return -1;
        }


        public State FindIndexAd(int i)
        {
            return Queue[i];
        }

        public void RemoveAt(int index)
        {
            Queue.RemoveAt(index);
        }
    }
}
