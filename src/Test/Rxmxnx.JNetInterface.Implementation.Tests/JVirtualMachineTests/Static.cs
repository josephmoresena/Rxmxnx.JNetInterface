namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	private static JGlobal CreateThreadGroup(IVirtualMachine vm, JGlobalRef globalRef)
	{
		EnvironmentProxy proxy = EnvironmentProxy.CreateEnvironment();
		JClassObject jClassClass = new(proxy);
		TypeInfoSequence classInformation = MetadataHelper.GetClassInformation("java/lang/ThreadGroup"u8, false);
		JClassObject classLoaderClass = new(jClassClass, new TypeInformation(classInformation));
		return new(vm, new(classLoaderClass), globalRef);
	}
	private static Task RegisterTestObject()
	{
		TaskCompletionSource tcs = new();
		Thread registrationThread = new(() =>
		{
			Boolean register = JVirtualMachine.Register<JTestObject>();
			if (register)
				Assert.NotNull(JTestObject.GetThreadMetadata());
		});
		Thread wrapperThread = new(() =>
		{
			try
			{
				registrationThread.Start();
				registrationThread.Join();
				tcs.SetResult();
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}
		});
		wrapperThread.Start();
		return tcs.Task;
	}
	private static Dictionary<MainClass, IFixedPointer.IDisposable> GetMainNamePointer()
	{
		Dictionary<MainClass, IFixedPointer.IDisposable> mainPointer =
			JVirtualMachineTests.mainMetadata.ToDictionary(p => p.Key, p => p.Value.Information.GetFixedPointer());
		return mainPointer;
	}
	private static Dictionary<MainClass, JGlobalRef> GetMainGlobalRef(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JGlobalRef> mainGlobalRef = [];
		mainGlobalRef.Add(MainClass.Class, proxyEnv.VirtualMachine.ClassGlobalRef);
		mainGlobalRef.Add(MainClass.Throwable, proxyEnv.VirtualMachine.ThrowableGlobalRef);
		mainGlobalRef.Add(MainClass.StackTraceElement, proxyEnv.VirtualMachine.StackTraceElementGlobalRef);
		mainGlobalRef.Add(MainClass.System, proxyEnv.VirtualMachine.SystemGlobalRef);
		mainGlobalRef.Add(MainClass.NumberObject, proxyEnv.VirtualMachine.NumberGlobalRef);
		mainGlobalRef.Add(MainClass.VoidObject, proxyEnv.VirtualMachine.VoidGlobalRef);
		mainGlobalRef.Add(MainClass.BooleanObject, proxyEnv.VirtualMachine.BooleanGlobalRef);
		mainGlobalRef.Add(MainClass.ByteObject, proxyEnv.VirtualMachine.ByteGlobalRef);
		mainGlobalRef.Add(MainClass.CharacterObject, proxyEnv.VirtualMachine.CharacterGlobalRef);
		mainGlobalRef.Add(MainClass.DoubleObject, proxyEnv.VirtualMachine.DoubleGlobalRef);
		mainGlobalRef.Add(MainClass.FloatObject, proxyEnv.VirtualMachine.FloatGlobalRef);
		mainGlobalRef.Add(MainClass.IntegerObject, proxyEnv.VirtualMachine.IntegerGlobalRef);
		mainGlobalRef.Add(MainClass.LongObject, proxyEnv.VirtualMachine.LongGlobalRef);
		mainGlobalRef.Add(MainClass.ShortObject, proxyEnv.VirtualMachine.ShortGlobalRef);
		mainGlobalRef.Add(MainClass.VoidPrimitive, proxyEnv.VirtualMachine.VoidPGlobalRef);
		mainGlobalRef.Add(MainClass.BooleanPrimitive, proxyEnv.VirtualMachine.BooleanPGlobalRef);
		mainGlobalRef.Add(MainClass.BytePrimitive, proxyEnv.VirtualMachine.BytePGlobalRef);
		mainGlobalRef.Add(MainClass.CharPrimitive, proxyEnv.VirtualMachine.CharPGlobalRef);
		mainGlobalRef.Add(MainClass.DoublePrimitive, proxyEnv.VirtualMachine.DoublePGlobalRef);
		mainGlobalRef.Add(MainClass.FloatPrimitive, proxyEnv.VirtualMachine.FloatPGlobalRef);
		mainGlobalRef.Add(MainClass.IntPrimitive, proxyEnv.VirtualMachine.IntPGlobalRef);
		mainGlobalRef.Add(MainClass.LongPrimitive, proxyEnv.VirtualMachine.LongPGlobalRef);
		mainGlobalRef.Add(MainClass.ShortPrimitive, proxyEnv.VirtualMachine.ShortPGlobalRef);
		return mainGlobalRef;
	}
	private static Dictionary<MainClass, JClassLocalRef> GetMainLocalRef(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JClassLocalRef> mainClassRef = [];
		mainClassRef.Add(MainClass.Class, proxyEnv.ClassLocalRef);
		mainClassRef.Add(MainClass.Throwable, proxyEnv.ThrowableLocalRef);
		mainClassRef.Add(MainClass.StackTraceElement, proxyEnv.StackTraceElementLocalRef);
		mainClassRef.Add(MainClass.System, proxyEnv.SystemLocalRef);
		mainClassRef.Add(MainClass.NumberObject, proxyEnv.NumberObjectLocalRef);

		mainClassRef.Add(MainClass.VoidObject, proxyEnv.VoidObjectLocalRef);
		mainClassRef.Add(MainClass.BooleanObject, proxyEnv.BooleanObjectLocalRef);
		mainClassRef.Add(MainClass.ByteObject, proxyEnv.ByteObjectLocalRef);
		mainClassRef.Add(MainClass.CharacterObject, proxyEnv.CharacterObjectLocalRef);
		mainClassRef.Add(MainClass.DoubleObject, proxyEnv.DoubleObjectLocalRef);
		mainClassRef.Add(MainClass.FloatObject, proxyEnv.FloatObjectLocalRef);
		mainClassRef.Add(MainClass.IntegerObject, proxyEnv.IntegerObjectLocalRef);
		mainClassRef.Add(MainClass.LongObject, proxyEnv.LongObjectLocalRef);
		mainClassRef.Add(MainClass.ShortObject, proxyEnv.ShortObjectLocalRef);

		mainClassRef.Add(MainClass.VoidPrimitive, proxyEnv.VoidPrimitiveLocalRef);
		mainClassRef.Add(MainClass.BooleanPrimitive, proxyEnv.BooleanPrimitiveLocalRef);
		mainClassRef.Add(MainClass.BytePrimitive, proxyEnv.BytePrimitiveLocalRef);
		mainClassRef.Add(MainClass.CharPrimitive, proxyEnv.CharPrimitiveLocalRef);
		mainClassRef.Add(MainClass.DoublePrimitive, proxyEnv.DoublePrimitiveLocalRef);
		mainClassRef.Add(MainClass.FloatPrimitive, proxyEnv.FloatPrimitiveLocalRef);
		mainClassRef.Add(MainClass.IntPrimitive, proxyEnv.IntPrimitiveLocalRef);
		mainClassRef.Add(MainClass.LongPrimitive, proxyEnv.LongPrimitiveLocalRef);
		mainClassRef.Add(MainClass.ShortPrimitive, proxyEnv.ShortPrimitiveLocalRef);
		return mainClassRef;
	}
	private static Dictionary<MainClass, JFieldId> GetTypeField(NativeInterfaceProxy proxyEnv)
	{
		Dictionary<MainClass, JFieldId> mainTypeField = [];
		mainTypeField.Add(MainClass.VoidObject, proxyEnv.VoidTypeFieldId);
		mainTypeField.Add(MainClass.BooleanObject, proxyEnv.BooleanTypeFieldId);
		mainTypeField.Add(MainClass.ByteObject, proxyEnv.ByteTypeFieldId);
		mainTypeField.Add(MainClass.CharacterObject, proxyEnv.CharacterTypeFieldId);
		mainTypeField.Add(MainClass.DoubleObject, proxyEnv.DoubleTypeFieldId);
		mainTypeField.Add(MainClass.FloatObject, proxyEnv.FloatTypeFieldId);
		mainTypeField.Add(MainClass.IntegerObject, proxyEnv.IntegerTypeFieldId);
		mainTypeField.Add(MainClass.LongObject, proxyEnv.LongTypeFieldId);
		mainTypeField.Add(MainClass.ShortObject, proxyEnv.ShortTypeFieldId);
		return mainTypeField;
	}
#pragma warning disable CA1859
	private static readonly IReadOnlyDictionary<MainClass, JDataTypeMetadata> mainMetadata =
		new Dictionary<MainClass, JDataTypeMetadata>
		{
			{ MainClass.Class, IDataType.GetMetadata<JClassObject>() },
			{ MainClass.Throwable, IDataType.GetMetadata<JThrowableObject>() },
			{ MainClass.StackTraceElement, IDataType.GetMetadata<JStackTraceElementObject>() },
			{ MainClass.System, IDataType.GetMetadata<JSystemObject>() },
			{ MainClass.NumberObject, IDataType.GetMetadata<JNumberObject>() },
			{ MainClass.VoidObject, IDataType.GetMetadata<JVoidObject>() },
			{ MainClass.BooleanObject, IDataType.GetMetadata<JBooleanObject>() },
			{ MainClass.ByteObject, IDataType.GetMetadata<JByteObject>() },
			{ MainClass.CharacterObject, IDataType.GetMetadata<JCharacterObject>() },
			{ MainClass.DoubleObject, IDataType.GetMetadata<JDoubleObject>() },
			{ MainClass.FloatObject, IDataType.GetMetadata<JFloatObject>() },
			{ MainClass.IntegerObject, IDataType.GetMetadata<JIntegerObject>() },
			{ MainClass.LongObject, IDataType.GetMetadata<JLongObject>() },
			{ MainClass.ShortObject, IDataType.GetMetadata<JShortObject>() },
			{ MainClass.VoidPrimitive, JPrimitiveTypeMetadata.VoidMetadata },
			{ MainClass.BooleanPrimitive, IDataType.GetMetadata<JBoolean>() },
			{ MainClass.BytePrimitive, IDataType.GetMetadata<JByte>() },
			{ MainClass.CharPrimitive, IDataType.GetMetadata<JChar>() },
			{ MainClass.DoublePrimitive, IDataType.GetMetadata<JDouble>() },
			{ MainClass.FloatPrimitive, IDataType.GetMetadata<JFloat>() },
			{ MainClass.IntPrimitive, IDataType.GetMetadata<JInt>() },
			{ MainClass.LongPrimitive, IDataType.GetMetadata<JLong>() },
			{ MainClass.ShortPrimitive, IDataType.GetMetadata<JShort>() },
		};
	private static readonly IReadOnlyDictionary<MainClass, MainClass> mainWrapper = new Dictionary<MainClass, MainClass>
	{
		{ MainClass.VoidObject, MainClass.VoidPrimitive },
		{ MainClass.BooleanObject, MainClass.BooleanPrimitive },
		{ MainClass.ByteObject, MainClass.BytePrimitive },
		{ MainClass.CharacterObject, MainClass.CharPrimitive },
		{ MainClass.DoubleObject, MainClass.DoublePrimitive },
		{ MainClass.FloatObject, MainClass.FloatPrimitive },
		{ MainClass.IntegerObject, MainClass.IntPrimitive },
		{ MainClass.LongObject, MainClass.LongPrimitive },
		{ MainClass.ShortObject, MainClass.ShortPrimitive },
		{ MainClass.VoidPrimitive, MainClass.VoidObject },
		{ MainClass.BooleanPrimitive, MainClass.BooleanObject },
		{ MainClass.BytePrimitive, MainClass.ByteObject },
		{ MainClass.CharPrimitive, MainClass.CharacterObject },
		{ MainClass.DoublePrimitive, MainClass.DoubleObject },
		{ MainClass.FloatPrimitive, MainClass.FloatObject },
		{ MainClass.IntPrimitive, MainClass.IntegerObject },
		{ MainClass.LongPrimitive, MainClass.LongObject },
		{ MainClass.ShortPrimitive, MainClass.ShortObject },
	};
#pragma warning restore CA1859
}