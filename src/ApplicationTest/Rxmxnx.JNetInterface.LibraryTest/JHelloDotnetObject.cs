using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// .NET representation of <c>com.rxmxnx.dotnet.test.HelloDotnet</c> java class.
/// </summary>
public sealed partial class JHelloDotnetObject : JLocalObject, IClassType<JHelloDotnetObject>
{
	public static JClassTypeMetadata<JHelloDotnetObject> Metadata { get; } = TypeMetadataBuilder<JHelloDotnetObject>
	                                                                         .Create(
		                                                                         "com/rxmxnx/dotnet/test/HelloDotnet"u8)
	                                                                         .Build();

	private JHelloDotnetObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private JHelloDotnetObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private JHelloDotnetObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JClassObject LoadClass<TManaged>(IEnvironment env, Byte[] classByteCode, TManaged managed)
		where TManaged : IManagedCallback
	{
		managed.Writer.WriteLine($"Loading bytecode... {classByteCode.LongLength / 1024.0:0.00} KiB");
		JClassObject result = JClassObject.LoadClass<JHelloDotnetObject>(env, classByteCode);
		JniCallback.RegisterNativeMethods(result, managed);
		return result;
	}
	public static void SetCallback<TManaged>(IEnvironment env, TManaged managed) where TManaged : IManagedCallback
	{
		using JClassObject jClass = JClassObject.GetClass<JHelloDotnetObject>(env);
		JniCallback.RegisterNativeMethods(jClass, managed);
		JHelloDotnetObject.GetObject(jClass);
	}
	public static void GetObject(JClassObject helloJniClass)
	{
		JInt count = new JFieldDefinition<JInt>("COUNT"u8).StaticGet(helloJniClass);
		for (JInt i = 0; i < count; i++)
		{
			using JLocalObject? jLocal = GetObjectDefinition.Instance.Invoke(helloJniClass, i);
			Console.WriteLine($"getObject({i}) -> {jLocal}");
		}
	}

	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}