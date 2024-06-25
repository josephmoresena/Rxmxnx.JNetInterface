using System.Diagnostics.CodeAnalysis;

using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// .NET representation of <c>com.rxmxnx.dotnet.test.HelloDotnet</c> java class.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed partial class JHelloDotnetObject : JLocalObject, IClassType<JHelloDotnetObject>
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
		JClassObject result = JClassObject.LoadClass<JHelloDotnetObject>(env, classByteCode);
		JniCallback.RegisterNativeMethods(result, managed);
		return result;
	}

	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IClassType<JHelloDotnetObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}