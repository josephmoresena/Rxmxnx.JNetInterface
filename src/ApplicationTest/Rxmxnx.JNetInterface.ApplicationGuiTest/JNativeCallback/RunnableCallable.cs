using Rxmxnx.JNetInterface.ApplicationTest.Util.Concurrent;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class JNativeCallback
{
	private sealed partial class RunnableCallable : JNativeCallback, IClassType<RunnableCallable>,
		IInterfaceObject<JRunnableObject>, IInterfaceObject<JCallableObject>, IInterfaceObject<JRunnableFutureObject>
	{
		private new static readonly JClassTypeMetadata<RunnableCallable> typeMetadata =
			TypeMetadataBuilder<JNativeCallback>
				.Create<RunnableCallable>("com/rxmxnx/jnetinterface/NativeRunnableCallable"u8, JTypeModifier.Final)
				.Implements<JRunnableObject>().Implements<JCallableObject>().Implements<JRunnableFutureObject>()
				.Build();

		static JClassTypeMetadata<RunnableCallable> IClassType<RunnableCallable>.Metadata
			=> RunnableCallable.typeMetadata;

		private RunnableCallable(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private RunnableCallable(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private RunnableCallable(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static RunnableCallable IClassType<RunnableCallable>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static RunnableCallable IClassType<RunnableCallable>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static RunnableCallable IClassType<RunnableCallable>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}