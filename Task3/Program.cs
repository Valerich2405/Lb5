namespace Task3
{
    public class Program
    {
        private static readonly object lockObject = new object();

        static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                Thread leftThread = new Thread(() => QuickSort(array, left, pivot - 1));
                Thread rightThread = new Thread(() => QuickSort(array, pivot + 1, right));
                leftThread.Start();
                rightThread.Start();
                leftThread.Join();
                rightThread.Join();
            }
        }

        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }

        static void Swap(int[] array, int i, int j)
        {
            lock (lockObject)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        static int[] GenerateRandomArray(int size)
        {
            Random rand = new Random();
            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = rand.Next(1, 101);
            }
            return arr;
        }

        static void Main(string[] args)
        {
            int[] array = GenerateRandomArray(10);
            Console.WriteLine("Unsorted array: " + string.Join(", ", array));
            Console.WriteLine();

            QuickSort(array, 0, array.Length - 1);
            Console.WriteLine("Sorted array: " + string.Join(", ", array));

            Console.ReadLine();
        }
    }
}