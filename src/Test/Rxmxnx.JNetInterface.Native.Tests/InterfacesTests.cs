namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class InterfacesTests
{
	[Fact]
	internal void EnvironmentTest()
	{
		JNativeInterface jni = default;
		IntPtr jniPtr = NativeUtilities.GetUnsafeIntPtr(in jni);
		JEnvironmentValue envValue = jniPtr.Transform<IntPtr, JEnvironmentValue>();
		IntPtr valPtr = NativeUtilities.GetUnsafeIntPtr(in envValue);
		JEnvironmentRef envRef = valPtr.Transform<IntPtr, JEnvironmentRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		env.Reference.Returns(envRef);

		Assert.Equal(envRef, (env as IWrapper<JEnvironmentRef>).Value);
	}
	[Fact]
	internal void VirtualMachineTest()
	{
		JInvokeInterface jii = default;
		IntPtr jiiPtr = NativeUtilities.GetUnsafeIntPtr(in jii);
		JVirtualMachineValue vmValue = jiiPtr.Transform<IntPtr, JVirtualMachineValue>();
		IntPtr valPtr = NativeUtilities.GetUnsafeIntPtr(in vmValue);
		JVirtualMachineRef vmRef = valPtr.Transform<IntPtr, JVirtualMachineRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		env.VirtualMachine.Reference.Returns(vmRef);

		Assert.Equal(vmRef, (env.VirtualMachine as IWrapper<JVirtualMachineRef>).Value);

		foreach (ThreadPurpose purpose in Enum.GetValues<ThreadPurpose>())
		{
			String threadName = $"{Enum.GetName(purpose)}-{Environment.CurrentManagedThreadId}";
			using IThread thread = (env.VirtualMachine as IVirtualMachine).CreateThread(purpose);
			env.VirtualMachine.Received(1).InitializeThread(Arg.Is<CString?>(c => c!.ToString() == threadName));
		}
	}
	[Fact]
#pragma warning disable CA1859
	internal void ClassesTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		IClassFeature classFeature = env.ClassFeature;
		using JClassObject jClassClass = new(env);
		Dictionary<Type, JClassObject> classDictionary = new()
		{
			{ typeof(JBufferObject), new(jClassClass, IDataType.GetMetadata<JBufferObject>()) },
			{ typeof(JClassLoaderObject), new(jClassClass, IDataType.GetMetadata<JClassLoaderObject>()) },
			{ typeof(JConstructorObject), new(jClassClass, IDataType.GetMetadata<JConstructorObject>()) },
			{ typeof(JEnumObject), new(jClassClass, IDataType.GetMetadata<JEnumObject>()) },
			{ typeof(JErrorObject), new(jClassClass, IDataType.GetMetadata<JErrorObject>()) },
			{ typeof(JExceptionObject), new(jClassClass, IDataType.GetMetadata<JExceptionObject>()) },
			{ typeof(JFieldObject), new(jClassClass, IDataType.GetMetadata<JFieldObject>()) },
			{ typeof(JMethodObject), new(jClassClass, IDataType.GetMetadata<JMethodObject>()) },
			{ typeof(JNumberObject), new(jClassClass, IDataType.GetMetadata<JNumberObject>()) },
			{ typeof(JLocalObject), new(jClassClass, IDataType.GetMetadata<JLocalObject>()) },
			{
				typeof(JStackTraceElementObject),
				new(jClassClass, IDataType.GetMetadata<JStackTraceElementObject>())
			},
			{ typeof(JStringObject), new(jClassClass, IDataType.GetMetadata<JStringObject>()) },
			{ typeof(JThrowableObject), new(jClassClass, IDataType.GetMetadata<JThrowableObject>()) },
		};
		env.ClassFeature.UseNonGeneric = true;
		env.ClassFeature.GetNonGenericClass(Arg.Any<Type>()).Returns(c => classDictionary[(Type)c[0]]);

		Assert.Equal(classDictionary[typeof(JBufferObject)], classFeature.BufferObject);
		Assert.Equal(classDictionary[typeof(JClassLoaderObject)], classFeature.ClassLoaderObject);
		Assert.Equal(classDictionary[typeof(JConstructorObject)], classFeature.ConstructorObject);
		Assert.Equal(classDictionary[typeof(JEnumObject)], classFeature.EnumObject);
		Assert.Equal(classDictionary[typeof(JErrorObject)], classFeature.ErrorObject);
		Assert.Equal(classDictionary[typeof(JExceptionObject)], classFeature.ExceptionObject);
		Assert.Equal(classDictionary[typeof(JFieldObject)], classFeature.FieldObject);
		Assert.Equal(classDictionary[typeof(JMethodObject)], classFeature.MethodObject);
		Assert.Equal(classDictionary[typeof(JNumberObject)], classFeature.NumberObject);
		Assert.Equal(classDictionary[typeof(JLocalObject)], classFeature.Object);
		Assert.Equal(classDictionary[typeof(JStackTraceElementObject)], classFeature.StackTraceElementObject);
		Assert.Equal(classDictionary[typeof(JStringObject)], classFeature.StringObject);
		Assert.Equal(classDictionary[typeof(JThrowableObject)], classFeature.ThrowableObject);

		foreach (JClassObject jClass in classDictionary.Values)
			jClass.Dispose();
	}
#pragma warning restore CA1859
	[Fact]
	internal void CriticalExceptionTest()
	{
		CriticalException ex = CriticalException.Instance;
		Assert.Equal(JResult.Error, ex.Result);
		Assert.Equal(IMessageResource.GetInstance().CriticalExceptionMessage, ex.Message);
	}
	[Fact]
	internal void LocalViewObjectTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		Assert.Null(ILocalViewObject.GetObject(default));
		LocalView localView = new(jClassClass);
		for (Int32 i = 0; i < Random.Shared.Next(1, 10); i++)
		{
			Assert.Equal(jClassClass, ILocalViewObject.GetObject(localView));
			localView = new(localView);
		}
	}

	private sealed record LocalView(ILocalObject Object) : ILocalViewObject
	{
		public IEnvironment Environment => this.Object.Environment;
		public JObjectLocalRef LocalReference => this.Object.LocalReference;
		public TReference CastTo<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
			=> this.Object.CastTo<TReference>();
	}
}