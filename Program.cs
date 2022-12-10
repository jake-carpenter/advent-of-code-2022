using System.Reflection;
using AdventOfCode.Days;

var days = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(type => type.BaseType == typeof(BaseDay))
    .Select(type => (BaseDay)Activator.CreateInstance(type)!);

foreach (var day in days)
{
    await day.Execute();
}
