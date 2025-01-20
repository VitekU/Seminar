namespace prelevaniVody
{
    struct State
    {
        public int X;
        public int Y;

        public State(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<State> visited = new HashSet<State>();
            Queue<State> queue = new Queue<State>();
            Dictionary<State, State> statePath = new Dictionary<State, State>();
            List<State> states = new List<State>();
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int maxOfX = input[0];
            int maxOfY = input[1];
            State start = new State(0, 0);
            State end = new State(input[2], input[3]);
            
            queue.Enqueue(start);
            visited.Add(start);
            
            while (queue.Count > 0)
            {
                State currentState = queue.Dequeue();
                if (currentState.X == end.X && currentState.Y == end.Y)
                {
                    break;
                }
                states.Clear();
                
                
                // ruzne stavy
                State emptyX = new State(0, currentState.Y);
                State emptyY = new State(currentState.X, 0);
                states.Add(emptyX);
                states.Add(emptyY);
                
                State emptyXFillY = new State(0, maxOfY);
                State emptyYFillX = new State(maxOfX, 0);
                states.Add(emptyXFillY);
                states.Add(emptyYFillX);
                
                State fillX = new State(maxOfX, currentState.Y);
                State fillY = new State(currentState.X, maxOfY);
                State fillBoth = new State(maxOfX, maxOfY);
                states.Add(fillX);
                states.Add(fillY);
                states.Add(fillBoth);
                
                int diffX = maxOfX - currentState.X;
                int diffY = maxOfY - currentState.Y;

                State xtoY;
                if (currentState.X >= diffY)
                {
                    xtoY = new State(currentState.X - diffY, maxOfY);
                }
                else
                {
                    xtoY = new State(0, currentState.Y + currentState.X);
                }
                
                State ytoX;
                if (currentState.Y >= diffX)
                {
                    ytoX = new State(maxOfX, currentState.Y - diffX);
                }
                else
                {
                    ytoX = new State(currentState.X + currentState.Y, 0);
                }
                states.Add(xtoY);
                states.Add(ytoX);

                foreach (var s in states)
                {
                    if (!visited.Contains(s))
                    {
                        queue.Enqueue(s);
                        statePath.Add(s, currentState);
                        visited.Add(s);
                    }
                }
            }

            List<State> statesOut = new List<State>();
            if (statePath.ContainsKey(end))
            {
                State currentState = end;
                statesOut.Add(end);
                while (true)
                {
                    statesOut.Add(statePath[currentState]);
                    currentState = statePath[currentState];
                    if (currentState.X == start.X && currentState.Y == start.Y)
                    {
                        break;
                    }
                }

                statesOut.Reverse();
                Console.WriteLine("Lze");
                Console.WriteLine($"Pocet kroku: {statesOut.Count - 1}");
                Console.WriteLine("Kroky:");
                foreach (var s in statesOut)
                {
                    Console.WriteLine($"[{s.X}, {s.Y}]");
                }
            }
            else
            {
                Console.WriteLine("Nelze");
            }
            
        }
    }
}