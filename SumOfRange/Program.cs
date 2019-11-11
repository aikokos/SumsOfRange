using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelloApp
{
    class Program
    {
        static void SumOfRange(int n, CancellationToken token)
        {
            int result = 0;
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }
                result += i;
                Thread.Sleep(1000);
            }
            Console.WriteLine($"Сумма чисел до {n} равна {result}");
        }
        // определение асинхронного метода
        static async void SumOfRangeAsync(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(() => SumOfRange(n, token));
        }

        static void Main(string[] args)
        {
            
            while (true)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                int n = Int32.Parse(Console.ReadLine());
                SumOfRangeAsync(n, token);
                Thread.Sleep(3000);
                cts.Cancel();

            }
            
        }
    }
}