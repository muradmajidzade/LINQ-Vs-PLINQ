//using System.Diagnostics;

//var numbers = Enumerable.Range(1, 5_000_000);

//static long HeavyCalculation(int number)
//{
//    long result = 0;

//    for (int i = 0; i < 6769; i++)
//    {
//        result += i * 2891 - 82 / 11 % 2;
//    }

//    return result;
//}

//Stopwatch stopwatch = Stopwatch.StartNew();

//var result = numbers
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch.Stop();

//TimeSpan ts = stopwatch.Elapsed;
//long milliseconds = stopwatch.ElapsedMilliseconds;

//Console.WriteLine($"LINQ Spent time: {ts}");



//Stopwatch stopwatch2 = Stopwatch.StartNew();

//var result2 = numbers
//    .AsParallel()
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch2.Stop();

//TimeSpan ts2 = stopwatch2.Elapsed;
//long milliseconds2 = stopwatch2.ElapsedMilliseconds;

//Console.WriteLine($"PLINQ Spent time: {ts2}");


//long speedup = milliseconds / milliseconds2;
//Console.WriteLine(speedup);


//Stopwatch stopwatch3 = Stopwatch.StartNew();

//var result3 = numbers
//    .AsParallel()
//    .WithDegreeOfParallelism(2)
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch3.Stop();

//TimeSpan ts3 = stopwatch3.Elapsed;
//long milliseconds3 = stopwatch3.ElapsedMilliseconds;

//Console.WriteLine($"PLINQ (2 threads) Spent time: {ts3}");


//Stopwatch stopwatch4 = Stopwatch.StartNew();

//var result4 = numbers
//    .AsParallel()
//    .WithDegreeOfParallelism(4)
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch4.Stop();

//TimeSpan ts4 = stopwatch4.Elapsed;
//long milliseconds4 = stopwatch4.ElapsedMilliseconds;

//Console.WriteLine($"PLINQ (4 threads) Spent time: {ts4}");


//Stopwatch stopwatch5 = Stopwatch.StartNew();

//var result5 = numbers
//    .AsParallel()
//    .WithDegreeOfParallelism(8)
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch5.Stop();

//TimeSpan ts5 = stopwatch5.Elapsed;
//long milliseconds5 = stopwatch5.ElapsedMilliseconds;

//Console.WriteLine($"PLINQ (8 threads) Spent time: {ts5}");

//Stopwatch stopwatch6 = Stopwatch.StartNew();

//var result6 = numbers
//    .AsParallel()
//    .WithDegreeOfParallelism(16)
//    .Select(HeavyCalculation)
//    .ToList();

//stopwatch6.Stop();

//TimeSpan ts6 = stopwatch6.Elapsed;
//long milliseconds6 = stopwatch6.ElapsedMilliseconds;

//Console.WriteLine($"PLINQ (16 threads) Spent time: {ts6}");

// My results
//LINQ Spent time: 00:01:16.5817427
//PLINQ Spent time: 00:00:08.7934029
//8
//PLINQ(2 threads) Spent time: 00:00:39.6041605
//PLINQ(4 threads) Spent time: 00:00:21.0275321
//PLINQ(8 threads) Spent time: 00:00:12.6605594
//PLINQ(16 threads) Spent time: 00:00:08.7339008

// Task 7
//var numbers = Enumerable.Range(1, 100);

//var result = numbers
//    .AsParallel()
//    .Select(x => x)
//    .ToList();

//Console.WriteLine(string.Join(", ", result));

//var result2 = numbers
//    .AsParallel()
//    .AsOrdered()
//    .Select(x => x)
//    .ToList();

//Console.WriteLine(string.Join(", ", result2));

// По правде говоря у меня оба этих варианта запустились с правильным порядком, но как я понял для правильного результата нужно использовать больше значений

// PLINQ может менять порядок так как берет числа пакетами и какому то потоку достается тяжелый пакет а какому то легкий, а возвращает он значения по мере выполнения
// AsOrdered нужен как раз таки для того чтобы напоминать PLINQ о том чтобы он возвращал значения в том же порядке что и принял
// Цена AsOrdered в том что на него уходит время так как нужно хранить также индексы элементов, стоит использовать если порядок реально важен


// Bonus
//var numbers = Enumerable.Range(1, 100);

//var result = numbers
//    .AsParallel()
//    .Select(x => x * 2)
//    .ToList();

//Console.WriteLine(string.Join(", ", result));


//var result2 = numbers
//    .AsParallel()
//    .AsOrdered()
//    .Select(x => x * 2)
//    .ToList();

//Console.WriteLine(string.Join(", ", result2));

// Здесь у меня тоже за одно и тоже время
// Но насколько я понял одинакого либо больше так как время уходит на разделение на пакеты. Параллелизм стоит использовать когда операции сложные а данных много


//Когда стоит использовать PLINQ?

//Объясните своими словами:

//когда PLINQ действительно может ускорить программу;
//когда PLINQ использовать бессмысленно;
//почему большая коллекция сама по себе не гарантирует ускорение;
//почему CPU-bound операции являются хорошим кандидатом;
//как AsOrdered() влияет на выполнение;
//зачем нужен WithDegreeOfParallelism().
//Дополнительное условие

// Task 10 (Ответы на вопросы)
// PLINQ стоит использовать когда данных много или операции сложные так как используется сразу несколько потоков. Для простых задач это глупо так как часто они бывают в выполнении простых задач медленнее
// Пареллельность тратит время на разделение на пакеты и сжатие. Очень часто лучше использовать разделение не только для больших но и для тяжелых задач
// CPU-bound значит время работы ограничено скоростью процессора то есть задача вся состоит из вычислений
// AsOrdered может замедлять выполнение так как уходит время на хранение индексов также нужно возвращать значения в те же места что и взял на что также уходит время
// WithDegreeOfParallelism() нужен для того чтобы можно было выбирать сколько потоков может выполнять задачу одновременно. Есть лимит так как когда потоков больше чем ядер ускорение прекращается и может даже упасть из-за переключения между потоками
