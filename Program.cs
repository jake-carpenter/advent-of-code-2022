using System.Reflection;
using AdventOfCode.Days;

var runAll = !int.TryParse(args.FirstOrDefault(), out var dayArg);

var days = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.BaseType == typeof(BaseDay))
    .Select(type => (BaseDay)Activator.CreateInstance(type)!);

foreach (var day in days)
{
    if (runAll || day.DayNumber == dayArg)
    {
        await day.Execute();
    }
}