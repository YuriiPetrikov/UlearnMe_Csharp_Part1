// Вставьте сюда финальное содержимое файла BenchmarkTask.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
	{
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.

            //throw new NotImplementedException();
            task.Run();//прогревочный вызов
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for(int i = 0; i < repetitionCount; i++)
            {
                task.Run();
            }

            stopWatch.Stop();
            
            return stopWatch.Elapsed.TotalMilliseconds / repetitionCount;
        }
	}

    public class СreatingStringBuilder : ITask 
    {
        public void Run()
        {
            StringBuilder sb = new StringBuilder();
            while (sb.Length < 10000)
                sb.Append('a');
            string strSb = sb.ToString();
        }
    }

    public class СreatingString : ITask
    {
        public void Run()
        {
            string strSt = new string('a', 10000);
        }
    }

    [TestFixture]
     public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            СreatingStringBuilder sb = new СreatingStringBuilder();
            СreatingString st = new СreatingString();
            int repetitionCount = 10000;

            var bnSb = new Benchmark();
            var bnSt = new Benchmark();
            Assert.Less(bnSt.MeasureDurationInMs(st, repetitionCount), bnSb.MeasureDurationInMs(sb, repetitionCount));
        }
    }
}
