using System.Collections.Concurrent;

using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback : JLocalObject, IClassType<JNativeCallback>
{
	private static readonly JClassTypeMetadata<JNativeCallback> typeMetadata = TypeMetadataBuilder<JNativeCallback>
	                                                                           .Create(
		                                                                           "com/rxmxnx/jnetinterface/NativeCallback"u8)
	                                                                           .Build();

	private static readonly ConcurrentDictionary<Guid, CallbackState> states = new();
	private static Boolean finalizeRegistered;

	static JClassTypeMetadata<JNativeCallback> IClassType<JNativeCallback>.Metadata => JNativeCallback.typeMetadata;

	private JNativeCallback(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private JNativeCallback(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private JNativeCallback(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JNativeCallback IClassType<JNativeCallback>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNativeCallback IClassType<JNativeCallback>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNativeCallback IClassType<JNativeCallback>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}