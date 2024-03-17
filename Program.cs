namespace AppConHilosATM;

class Program
{
    private static int _accountBalance = 1000;
    private static Random _random = new Random();

    static void PerformTransaction(object? threadId)
    {
        for (int i = 0; i < 5; i++)
        {
            int amountToWithdraw = _random.Next(10, 101);
            
            Thread.Sleep(1000);

            lock (typeof(Program))
            {
                if (_accountBalance >= amountToWithdraw)
                {
                    _accountBalance -= amountToWithdraw;
                    Console.WriteLine($"Thread {threadId}: Se retiraron ${amountToWithdraw} pesos." +
                                      $" Quedan ${_accountBalance} pesos.");

                } else 
                {
                    Console.WriteLine($"Thread {threadId}: Fondos insuficientes." +
                                      $" Se requieren: ${amountToWithdraw} pesos");
                }
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("¡Bienvenido al cajero automático!");
        Console.WriteLine($"Cuentas con ${_accountBalance} pesos");
        Console.WriteLine("Presione Enter para iniciar transacciones...");
        Console.ReadLine();

        Thread[] threads = new Thread[5];
        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(PerformTransaction);
            threads[i].Start(i + 1);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }
        
        Console.WriteLine("Todas las transacciones completadas.");
        Console.WriteLine($"Saldo final de la cuenta: ${_accountBalance}");
    }
}