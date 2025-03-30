# Fluent Api
> This package contains classes and methods for creating Fluent chain of calls.

Here is an example of the usage:
``` C#
Console.ReadLine()
       .Pipe()
       .Map(t => t == null ? string.Empty : t)
       .Tap(Console.WriteLine)
       .Tap(Console.ReadKey(), out var key)
```