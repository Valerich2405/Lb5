namespace Task_2
{
    public class Program
    {
        static Semaphore semaphore = new Semaphore(2, 2);

        static void TrafficLight(int trafficLightNumber)
        {
            while (true)
            {
                lock (semaphore)
                {
                    Console.WriteLine($"Свiтлофор {trafficLightNumber}: Зелений");
                    Thread.Sleep(3000);
                    Console.WriteLine($"Свiтлофор {trafficLightNumber}: Жовтий");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Свiтлофор {trafficLightNumber}: Червоний");
                    Thread.Sleep(2000);
                }
            }
        }

        static void Main(string[] args)
        {
            Thread trafficLight1 = new Thread(() => TrafficLight(1));
            Thread trafficLight2 = new Thread(() => TrafficLight(2));
            Thread trafficLight3 = new Thread(() => TrafficLight(3));
            Thread trafficLight4 = new Thread(() => TrafficLight(4));

            trafficLight1.Start();
            trafficLight2.Start();
            trafficLight3.Start();
            trafficLight4.Start();

            trafficLight1.Join();
            trafficLight2.Join();
            trafficLight3.Join();
            trafficLight4.Join();

            Console.ReadLine();
        }
    }
}