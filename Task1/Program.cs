namespace Task1
{
    public class Program
    {
        static Queue<int> queue = new Queue<int>(); 
        static object lockObject = new object();

        static void Produce()
        {
            for (int i = 0; i < 10; i++)
            {
                int number = new Random().Next(100);
                lock (lockObject)
                {
                    queue.Enqueue(number);
                    Console.WriteLine($"Виробник згенерував число: {number}.");
                }
                Thread.Sleep(1000);
            }
        }

        static void Consume()
        {
            while (true)
            {
                int number;
                lock (lockObject)
                {
                    if (queue.Count == 0)
                        continue;
                    number = queue.Dequeue();
                }
                Console.WriteLine($"Число, що отримав споживач: {number}.");
                Thread.Sleep(500);
            }
        }

        static void Main(string[] args)
        {
            Thread producer = new Thread(Produce);
            Thread consumer = new Thread(Consume);

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();

            Console.ReadLine();
        }
    }
}