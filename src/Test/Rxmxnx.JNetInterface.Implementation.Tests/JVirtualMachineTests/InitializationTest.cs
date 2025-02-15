namespace Rxmxnx.JNetInterface.Tests;

public partial class JVirtualMachineTests
{
	[Theory]
	[InlineData(MainClass.None, ClassLoadingError.None)]
	[InlineData(MainClass.None, ClassLoadingError.FindClass)]
	[InlineData(MainClass.None, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.None, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.Class, ClassLoadingError.None)]
	[InlineData(MainClass.Class, ClassLoadingError.FindClass)]
	[InlineData(MainClass.Class, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.Class, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.Throwable, ClassLoadingError.None)]
	[InlineData(MainClass.Throwable, ClassLoadingError.FindClass)]
	[InlineData(MainClass.Throwable, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.Throwable, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.StackTraceElement, ClassLoadingError.None)]
	[InlineData(MainClass.StackTraceElement, ClassLoadingError.FindClass)]
	[InlineData(MainClass.StackTraceElement, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.StackTraceElement, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.VoidObject, ClassLoadingError.None)]
	[InlineData(MainClass.VoidObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.VoidObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.VoidObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.VoidPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.VoidPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.VoidPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.VoidPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.BooleanObject, ClassLoadingError.None)]
	[InlineData(MainClass.BooleanObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.BooleanObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.BooleanObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.BooleanPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.BooleanPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.BooleanPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.BooleanPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.ByteObject, ClassLoadingError.None)]
	[InlineData(MainClass.ByteObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.ByteObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.ByteObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.BytePrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.BytePrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.BytePrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.BytePrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.CharacterObject, ClassLoadingError.None)]
	[InlineData(MainClass.CharacterObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.CharacterObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.CharacterObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.CharPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.CharPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.CharPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.CharPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.DoubleObject, ClassLoadingError.None)]
	[InlineData(MainClass.DoubleObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.DoubleObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.DoubleObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.DoublePrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.DoublePrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.DoublePrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.DoublePrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.FloatObject, ClassLoadingError.None)]
	[InlineData(MainClass.FloatObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.FloatObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.FloatObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.FloatPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.FloatPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.FloatPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.FloatPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.IntegerObject, ClassLoadingError.None)]
	[InlineData(MainClass.IntegerObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.IntegerObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.IntegerObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.IntPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.IntPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.IntPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.IntPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.LongObject, ClassLoadingError.None)]
	[InlineData(MainClass.LongObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.LongObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.LongObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.LongPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.LongPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.LongPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.LongPrimitive, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.ShortObject, ClassLoadingError.None)]
	[InlineData(MainClass.ShortObject, ClassLoadingError.FindClass)]
	[InlineData(MainClass.ShortObject, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.ShortObject, ClassLoadingError.CreateGlobal)]
	[InlineData(MainClass.ShortPrimitive, ClassLoadingError.None)]
	[InlineData(MainClass.ShortPrimitive, ClassLoadingError.FindClass)]
	[InlineData(MainClass.ShortPrimitive, ClassLoadingError.TypeIdError)]
	[InlineData(MainClass.ShortPrimitive, ClassLoadingError.CreateGlobal)]
	internal void InitializationTest(MainClass mainClass = MainClass.None,
		ClassLoadingError error = ClassLoadingError.None)
	{
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		IFixedPointer.IDisposable typePtr = NativeFunctionSetImpl.PrimitiveTypeDefinition.Information.GetFixedPointer();
		JDataTypeMetadata? dataTypeMetadata = JVirtualMachineTests.mainMetadata.GetValueOrDefault(mainClass);
		String? className = dataTypeMetadata is not null ?
			ClassNameHelper.GetClassName(dataTypeMetadata.Signature) :
			default;
		JDataTypeMetadata? auxTypeMedata =
			JVirtualMachineTests.mainMetadata.GetValueOrDefault(
				JVirtualMachineTests.mainWrapper.GetValueOrDefault(mainClass));
		String? auxClassName = auxTypeMedata is not null ?
			ClassNameHelper.GetClassName(auxTypeMedata.Signature) :
			default;

		Boolean noThrows = mainClass is MainClass.None || error is ClassLoadingError.None ||
			(error == ClassLoadingError.TypeIdError && dataTypeMetadata?.Kind == JTypeKind.Primitive) ||
			(error == ClassLoadingError.TypeIdError && auxTypeMedata is null) ||
			// By default java.lang.Void is not a main class.
			(error == ClassLoadingError.CreateGlobal && mainClass is MainClass.VoidObject);
		Dictionary<MainClass, IFixedPointer.IDisposable> mainPointer = JVirtualMachineTests.GetMainNamePointer();
		Dictionary<MainClass, JFieldId> mainTypeField = JVirtualMachineTests.GetTypeField(proxyEnv);
		Dictionary<MainClass, JClassLocalRef> mainClassRef = JVirtualMachineTests.GetMainLocalRef(proxyEnv);
		Dictionary<MainClass, JGlobalRef> mainGlobalRef = JVirtualMachineTests.GetMainGlobalRef(proxyEnv);

		try
		{
			proxyEnv.UseDefaultClassRef = false;
			proxyEnv.FindClass(Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				IntPtr ptr = (ReadOnlyValPtr<Byte>)c[0];
				MainClass mClass = mainClassRef.Keys.First(mClass => mainPointer[mClass].Pointer == ptr);
				if (error != ClassLoadingError.FindClass || mClass != mainClass)
					return mainClassRef[mClass];
				return default;
			});
			proxyEnv.GetStaticFieldId(Arg.Any<JClassLocalRef>(), Arg.Any<ReadOnlyValPtr<Byte>>(),
			                          Arg.Any<ReadOnlyValPtr<Byte>>()).Returns(c =>
			{
				JClassLocalRef classRef = (JClassLocalRef)c[0];
				IntPtr ptr = (ReadOnlyValPtr<Byte>)c[1];
				if (ptr != typePtr.Pointer) return default;
				MainClass mClass = mainClassRef.Keys.First(mClass => mainClassRef[mClass] == classRef ||
					                                           mainGlobalRef[mClass].Value == classRef.Value);
				if (error != ClassLoadingError.TypeIdError || mClass != mainClass)
					return mainTypeField[mClass];
				return default;
			});
			proxyEnv.GetStaticObjectField(Arg.Any<JClassLocalRef>(), Arg.Any<JFieldId>()).Returns(c =>
			{
				JClassLocalRef classRef = (JClassLocalRef)c[0];
				JFieldId fieldId = (JFieldId)c[1];
				MainClass mClass = mainClassRef.Keys.First(
					mClass => JVirtualMachineTests.mainWrapper.TryGetValue(mClass, out MainClass wClass) &&
						(mainClassRef[wClass] == classRef || mainGlobalRef[wClass].Value == classRef.Value) &&
						mainTypeField[wClass] == fieldId);
				if (error != ClassLoadingError.FindClass || mClass != mainClass)
					return mainClassRef[mClass].Value;
				return default;
			});
			proxyEnv.NewGlobalRef(Arg.Any<JObjectLocalRef>()).Returns(c =>
			{
				JObjectLocalRef localRef = (JObjectLocalRef)c[0];
				MainClass mClass = mainClassRef.Keys.First(mClass => mainClassRef[mClass].Value == localRef);
				if (error != ClassLoadingError.CreateGlobal || mClass != mainClass)
					return mainGlobalRef[mClass];
				return default;
			});

			IVirtualMachine vm = JVirtualMachine.GetVirtualMachine(proxyEnv.VirtualMachine.Reference);
			IEnvironment? env = vm.GetEnvironment()!;
			Assert.Equal(proxyEnv.VirtualMachine.Reference, vm.Reference);

			// Fundamental classes
			Assert.Equal(mainGlobalRef[MainClass.Class].Value, env.ClassFeature.ClassObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.Throwable].Value, env.ClassFeature.ThrowableObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.StackTraceElement].Value,
			             env.ClassFeature.StackTraceElementObject.Reference.Value);

			// Primitive classes
			Assert.Equal(mainGlobalRef[MainClass.VoidPrimitive].Value, env.ClassFeature.VoidPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.BooleanPrimitive].Value,
			             env.ClassFeature.BooleanPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.BytePrimitive].Value, env.ClassFeature.BytePrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.CharPrimitive].Value, env.ClassFeature.CharPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.DoublePrimitive].Value,
			             env.ClassFeature.DoublePrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.FloatPrimitive].Value,
			             env.ClassFeature.FloatPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.IntPrimitive].Value, env.ClassFeature.IntPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.LongPrimitive].Value, env.ClassFeature.LongPrimitive.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.ShortPrimitive].Value,
			             env.ClassFeature.ShortPrimitive.Reference.Value);

			// User main classes (default)
			Assert.Equal(mainGlobalRef[MainClass.NumberObject].Value, env.ClassFeature.NumberObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.BooleanObject].Value, env.ClassFeature.BooleanObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.ByteObject].Value, env.ClassFeature.ByteObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.CharacterObject].Value,
			             env.ClassFeature.CharacterObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.DoubleObject].Value, env.ClassFeature.DoubleObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.FloatObject].Value, env.ClassFeature.FloatObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.IntegerObject].Value, env.ClassFeature.IntegerObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.LongObject].Value, env.ClassFeature.LongObject.Reference.Value);
			Assert.Equal(mainGlobalRef[MainClass.ShortObject].Value, env.ClassFeature.ShortObject.Reference.Value);

			// java.lang.Void is not main by default.
			Assert.True(JObject.IsNullOrDefault(env.ClassFeature.VoidObject));
		}
		catch (Exception ex)
		{
			Assert.IsType<NotSupportedException>(ex);
			IMessageResource resource = IMessageResource.GetInstance();
			switch (error)
			{
				case ClassLoadingError.FindClass when dataTypeMetadata?.Kind != JTypeKind.Primitive:
					Assert.Equal(resource.MainClassUnavailable(className!), ex.Message);
					break;
				case ClassLoadingError.FindClass when dataTypeMetadata?.Kind == JTypeKind.Primitive:
					Assert.Equal(resource.PrimitiveClassUnavailable(className!), ex.Message);
					break;
				case ClassLoadingError.TypeIdError when dataTypeMetadata?.Kind != JTypeKind.Primitive:
					Assert.Equal(resource.PrimitiveClassUnavailable(auxClassName!), ex.Message);
					break;
				case ClassLoadingError.CreateGlobal:
					Assert.Equal(resource.MainClassGlobalError(className!), ex.Message);
					break;
			}
		}
		finally
		{
			proxyEnv.Received(!noThrows ? 1 : 0).ExceptionDescribe();
			proxyEnv.Received(!noThrows ? 1 : 0).ExceptionClear();

			List<IDisposable> disposables = mainPointer.Values.Cast<IDisposable>().ToList();
			disposables.Add(typePtr);
			disposables.ForEach(d => d.Dispose());
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			Assert.Equal(noThrows, JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
	}
}