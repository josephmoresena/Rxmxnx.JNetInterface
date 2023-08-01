﻿using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;

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
		Console.WriteLine(IDataType.GetMetadata<MyClass>());
		Console.WriteLine(IDataType.GetMetadata<MyClass2>());
		Console.WriteLine("Inicio 1");
		foreach (Type type in IDataType<MyClass>.GetInterfaces())
			Console.WriteLine(type);
		Console.WriteLine("Inicio 2");
		foreach (Type type in IDataType<MyClass2>.GetInterfaces())
			Console.WriteLine(type);
		Console.WriteLine("Inicio 3");
		foreach (Type type in IDataType<MyClass3>.GetInterfaces())
			Console.WriteLine(type);
	}

	public class MyClass : JLocalObject, IClassType<MyClass>
	{
		public static JDataTypeMetadata Metadata
			=> JMetadataBuilder<MyClass>.Create(new(() => "application.example.Class1"u8)).Build();
		public MyClass(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
		protected MyClass(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }
		public static MyClass? Create(JObject? jObject) => default;
	}

	public class MyClass2 : MyClass, IClassType<MyClass2>
	{
		public static JDataTypeMetadata Metadata
			=> JMetadataBuilder<MyClass>.Create(new(() => "application.example.Class2"u8)).Build();
		public MyClass2(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
		protected MyClass2(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }
		public static MyClass2? Create(JObject? jObject) => default;
	}

	public class MyClass3 : MyClass2, IClassType<MyClass3>
	{
		public static JDataTypeMetadata Metadata
			=> JMetadataBuilder<MyClass>.Create(new(() => "application.example.Class2"u8)).Build();
		public MyClass3(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
		protected MyClass3(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }
		public static MyClass3? Create(JObject? jObject) => default;
	}
}