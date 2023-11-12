﻿using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public static class Program
{
	public static void Main(String[] args)
	{
		Console.WriteLine("Hello, World!");
		Program.PrintBuiltIntMetadata();
	}
	private static void PrintBuiltIntMetadata()
	{
		Console.WriteLine(IDataType.GetMetadata<JBoolean>());
		Console.WriteLine(IDataType.GetMetadata<JByte>());
		Console.WriteLine(IDataType.GetMetadata<JChar>());
		Console.WriteLine(IDataType.GetMetadata<JShort>());
		Console.WriteLine(IDataType.GetMetadata<JInt>());
		Console.WriteLine(IDataType.GetMetadata<JLong>());
		Console.WriteLine(IDataType.GetMetadata<JFloat>());
		Console.WriteLine(IDataType.GetMetadata<JDouble>());
		Console.WriteLine(IDataType.GetMetadata<JLocalObject>());
		Console.WriteLine(IDataType.GetMetadata<JClassObject>());
		Console.WriteLine(IDataType.GetMetadata<JStringObject>());
		Console.WriteLine(IDataType.GetMetadata<JEnumObject>());
		Console.WriteLine(IDataType.GetMetadata<JNumberObject>());
		Console.WriteLine(IDataType.GetMetadata<JThrowableObject>());

		Console.WriteLine("====== Array types ======");

		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JBoolean>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JByte>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JChar>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JShort>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JInt>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLong>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JFloat>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JDouble>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLocalObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JClassObject>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JStringObject>>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JBooleanObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JDoubleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JIntegerObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharacterObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());
		Console.WriteLine(IDataType.GetMetadata<JShortObject>());
	}
}