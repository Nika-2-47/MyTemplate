using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("複数タスク処理開始");

        // Lock用オブジェクト
        object progressLock = new object();

        // 進捗を共有するIProgress<int>を作成
        var progress = new Progress<int>(percent =>
        {
            lock (progressLock)
            {
                Console.WriteLine($"進捗: {percent}%");
            }
       
        });

        // 複数タスクを並列実行
        var task1 = LongRunningOperationAsync("Task1", progress);
        var task2 = LongRunningOperationAsync("Task2", progress);
        var task3 = LongRunningOperationAsync("Task3", progress);

        int[] results = await Task.WhenAll(task1, task2, task3);

        Console.WriteLine("すべてのタスク完了");
        Console.WriteLine($"結果: {string.Join(", ", results)}");
    }

    static async Task<int> LongRunningOperationAsync(string name, IProgress<int> progress)
    {
        int totalSteps = 5;
        for (int i = 1; i <= totalSteps; i++)
        {
            await Task.Delay(500); // 擬似的な処理
            progress?.Report(i * 100 / totalSteps);
        }
        return new Random().Next(1, 100); // サンプル結果
    }
}
