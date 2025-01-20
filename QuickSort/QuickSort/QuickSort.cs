namespace QuickSort
{
    class QuickSort
    {
        public static int[] Sort(int[] list, int l, int r)
        {
            if (l >= r)
            {
                return list;
            }

            decimal n = l + r;
            int pivot = list[(int)Math.Floor(n / 2)];
            int i = l;
            int j = r;

            while (i <= j)
            {
                while (list[i] < pivot)
                {
                    i++;
                }
                while (list[j] > pivot)
                {
                    j--;
                }

                if (i < j)
                {
                    int swapI = list[i];
                    int swapJ = list[j];
                    list[j] = swapI;
                    list[i] = swapJ;
                }

                if (i <= j)
                {
                    i++;
                    j--;
                }
            }

            Sort(list, l, j);
            Sort(list, i, r);
            return list;
        }
    }
}

