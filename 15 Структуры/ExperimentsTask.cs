// Вставьте сюда финальное содержимое файла ExperimentsTask.cs
using System.Collections.Generic;

namespace StructBenchmarking
{
    public abstract class Product
    {
        List<ExperimentResult> times = new List<ExperimentResult>();
        Benchmark benchmark;// { get; set; }

        public Product()
        {
            benchmark = new Benchmark();
        }

        public abstract ITask Task(int count);

        public List<ExperimentResult> RunExperiment(int repetitionsCount)
        {
            foreach (var counts in Constants.FieldCounts)
            {
                times.Add(
                    new ExperimentResult(counts,
                    benchmark.MeasureDurationInMs(Task(counts), repetitionsCount)));
            }

            return times;
        }
    }

    public class ProductClassArrayCreationTask : Product
    {
        public override ITask Task(int count)
        {
            return new ClassArrayCreationTask(count);
        }
    }

    public class ProductStructArrayCreationTask : Product
    {
        public override ITask Task(int count)
        {
            return new StructArrayCreationTask(count);
        }
    }

    public class ProductStructArgumentTask : Product
    {
        public override ITask Task(int count)
        {
            return new MethodCallWithStructArgumentTask(count);
        }
    }

    public class ProductClassArgumentTask : Product
    {
        public override ITask Task(int count)
        {
            return new MethodCallWithClassArgumentTask(count);
        }
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
          IBenchmark benchmark, int repetitionsCount)
        {
            Product pr1 = new ProductClassArrayCreationTask();
            var classesTimes = pr1.RunExperiment(repetitionsCount);
            Product pr2 = new ProductStructArrayCreationTask();
            var structuresTimes = pr2.RunExperiment(repetitionsCount);

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            Product pr3 = new ProductClassArgumentTask();
            var classesTimes = pr3.RunExperiment(repetitionsCount);
            Product pr4 = new ProductStructArgumentTask();
            var structuresTimes = pr4.RunExperiment(repetitionsCount);

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}
