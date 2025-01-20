namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            // 10 80 30 90 40 test array 

            int[] l = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] sortedArray = QuickSort.Sort(l, 0, l.Length - 1);
            
            foreach (var x in sortedArray)
            {
                Console.Write(x + " ");
            }
        }
    }
}

