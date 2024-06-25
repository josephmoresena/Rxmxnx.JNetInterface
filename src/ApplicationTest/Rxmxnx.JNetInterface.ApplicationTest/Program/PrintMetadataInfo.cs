using Rxmxnx.JNetInterface.Io;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Lang.Annotation;
using Rxmxnx.JNetInterface.Lang.Reflect;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class Program
{
	private static void PrintMetadataInfo()
	{
		Program.PrintBuiltInMetadata();
		Program.PrintArrayMetadata(JArrayObject<JBoolean>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JByte>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JChar>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JDouble>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JFloat>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JInt>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JLong>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JShort>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JLocalObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JClassObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JThrowableObject>.Metadata, 5);
		Program.PrintArrayMetadata(JArrayObject<JStringObject>.Metadata, 5);
	}
	private static void PrintArrayMetadata(JArrayTypeMetadata arrMetadata, Int32 dimension)
	{
		Console.WriteLine(arrMetadata.ElementMetadata.Signature);
		Boolean stopMetadata = false;
		for (Int32 i = 0; i < dimension; i++)
		{
			Console.WriteLine(arrMetadata.Signature);
			if (arrMetadata.GetArrayMetadata() is not { } arrMet2)
			{
				stopMetadata = true;
				break;
			}
			arrMetadata = arrMet2;
		}
		if (!stopMetadata) Console.WriteLine(arrMetadata.Signature);
		Program.PrintNestedArrayMetadata(arrMetadata);
	}
	private static void PrintNestedArrayMetadata(JArrayTypeMetadata? arrMetadata, Boolean printCurrent = false)
	{
		if (printCurrent && arrMetadata is not null) Console.WriteLine(arrMetadata.Signature);
		while (arrMetadata is not null)
		{
			Console.WriteLine(arrMetadata.ElementMetadata.Signature);
			arrMetadata = arrMetadata.ElementMetadata as JArrayTypeMetadata;
		}
	}
	private static void PrintBuiltInMetadata()
	{
		Console.WriteLine("====== Primitive types ======");
		Console.WriteLine(JPrimitiveTypeMetadata.VoidMetadata);
		Console.WriteLine(IDataType.GetMetadata<JBoolean>());
		Console.WriteLine(IDataType.GetMetadata<JByte>());
		Console.WriteLine(IDataType.GetMetadata<JChar>());
		Console.WriteLine(IDataType.GetMetadata<JShort>());
		Console.WriteLine(IDataType.GetMetadata<JInt>());
		Console.WriteLine(IDataType.GetMetadata<JLong>());
		Console.WriteLine(IDataType.GetMetadata<JFloat>());
		Console.WriteLine(IDataType.GetMetadata<JDouble>());

		Console.WriteLine("====== Reference types ======");
		Console.WriteLine(IDataType.GetMetadata<JLocalObject>());
		Console.WriteLine(IDataType.GetMetadata<JClassObject>());
		Console.WriteLine(IDataType.GetMetadata<JStringObject>());
		Console.WriteLine(IDataType.GetMetadata<JEnumObject>());
		Console.WriteLine(IDataType.GetMetadata<JNumberObject>());
		Console.WriteLine(IDataType.GetMetadata<JThrowableObject>());
		Console.WriteLine(IDataType.GetMetadata<JStackTraceElementObject>());
		Console.WriteLine(IDataType.GetMetadata<JExceptionObject>());
		Console.WriteLine(IDataType.GetMetadata<JRuntimeExceptionObject>());
		Console.WriteLine(IDataType.GetMetadata<JErrorObject>());

		Console.WriteLine("====== Wrapper types ======");
		Console.WriteLine(IDataType.GetMetadata<JVoidObject>());
		Console.WriteLine(IDataType.GetMetadata<JBooleanObject>());
		Console.WriteLine(IDataType.GetMetadata<JByteObject>());
		Console.WriteLine(IDataType.GetMetadata<JDoubleObject>());
		Console.WriteLine(IDataType.GetMetadata<JFloatObject>());
		Console.WriteLine(IDataType.GetMetadata<JIntegerObject>());
		Console.WriteLine(IDataType.GetMetadata<JCharacterObject>());
		Console.WriteLine(IDataType.GetMetadata<JLongObject>());
		Console.WriteLine(IDataType.GetMetadata<JShortObject>());

		Console.WriteLine("====== Array types ======");
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JBoolean>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JByte>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JChar>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JShort>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JInt>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLong>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JFloat>>());
		Console.WriteLine(IDataType.GetMetadata<JArrayObject<JDouble>>());

		Console.WriteLine("====== Interfaces types ======");
		Console.WriteLine(IDataType.GetMetadata<JCharSequenceObject>());
		Console.WriteLine(IDataType.GetMetadata<JCloneableObject>());
		Console.WriteLine(IDataType.GetMetadata<JComparableObject>());
		Console.WriteLine(IDataType.GetMetadata<JSerializableObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotatedElementObject>());
		Console.WriteLine(IDataType.GetMetadata<JGenericDeclarationObject>());
		Console.WriteLine(IDataType.GetMetadata<JTypeObject>());
		Console.WriteLine(IDataType.GetMetadata<JAnnotationObject>());
	}
}