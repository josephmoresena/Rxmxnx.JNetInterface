# Visual Basic .NET Support

`JNetInterface` is partially compatible with Visual Basic .NET because the latter does not support many of the modern
.NET features such as ref structs and static members in interfaces.

Thus, any application or library in Visual Basic .NET can use the APIs and types exposed in `JNetInterface` (or in F#
or C# assemblies derived from it), but it will not be able to create Java reference types as in C# or F#.

## Primitives

In C#, `JNetInterface` uses operators to convert from one primitive type to another. In Visual Basic .NET, `widening`
can be used natively, but to use `narrowing` (which is implemented in C# through explicit operators), you must use the
functions `CBool`, `CSByte`, `CChar`, `CDbl`, `CSng`, `CInt`, `CLng`, and `CShort` on the `Value` property of each
primitive type.

```vb
Dim booleanValue As JBoolean = True
Dim byteValue As JByte = CSByte(-2)
Dim charValue As JChar = "."C
Dim doubleValue As JDouble = 3.14159265359
Dim floatValue As JFloat = 2.71828F
Dim intValue As JInt = 486
Dim longValue As JLong = 3000000000L
Dim shortValue As JShort = 1024S
```
