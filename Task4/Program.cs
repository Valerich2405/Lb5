namespace Task4
{
    public class Program
    {
        static Semaphore semaphore = new Semaphore(1, 1);
        static object lockObject = new object();

        static void MultiplyMatrices(int[,] matrixA, int[,] matrixB, int[,] resultMatrix, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    int sum = 0;
                    for (int k = 0; k < matrixA.GetLength(1); k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }

                    lock (lockObject)
                    {
                        semaphore.WaitOne();
                        resultMatrix[i, j] = sum;
                        semaphore.Release();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[,] matrixA = { { 1, 2 }, { 3, 4 } };
            int[,] matrixB = { { 5, 6 }, { 7, 8 } };
            int[,] resultMatrix = new int[matrixA.GetLength(0), matrixB.GetLength(1)];

            Thread t1 = new Thread(() => MultiplyMatrices(matrixA, matrixB, resultMatrix, 0, matrixA.GetLength(0) / 2));
            Thread t2 = new Thread(() => MultiplyMatrices(matrixA, matrixB, resultMatrix, matrixA.GetLength(0) / 2, matrixA.GetLength(0)));

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            Console.WriteLine("Result matrix:");
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    Console.Write(resultMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}