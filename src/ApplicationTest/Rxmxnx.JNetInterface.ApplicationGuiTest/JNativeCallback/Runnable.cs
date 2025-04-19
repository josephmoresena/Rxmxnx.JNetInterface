using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	private sealed unsafe class Runnable : JNativeCallback, IClassType<Runnable>, IInterfaceObject<JRunnableObject>
	{
		private static readonly JMethodDefinition runDef = (JMethodDefinition)IndeterminateCall.CreateMethodDefinition(
			"runnable_run"u8, [
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JLong>(),
			]).Definition;
		private static readonly JClassTypeMetadata<Runnable> nestedTypeMetadata = TypeMetadataBuilder<JNativeCallback>
			.Create<Runnable>("com/rxmxnx/jnetinterface/NativeCallback$NativeRunnable"u8, JTypeModifier.Final)
			.Implements<JRunnableObject>().Build();

		private static Boolean runRegistered;

		static JClassTypeMetadata<Runnable> IClassType<Runnable>.Metadata => Runnable.nestedTypeMetadata;

		private Runnable(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private Runnable(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private Runnable(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

		public static void RegisterRun(IEnvironment env)
		{
			if (Runnable.runRegistered) return;

			using JClassObject jClass = JClassObject.GetClass<JNativeCallback>(env);
			delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, void> ptr = &Runnable.Run;
			JNativeCallEntry entry = JNativeCallEntry.Create(Runnable.runDef, (IntPtr)ptr);

			jClass.Register(entry);
			Runnable.runRegistered = true;
		}

		[UnmanagedCallersOnly]
		private static void Run(JEnvironmentRef environmentRef, JClassLocalRef __, JLong lowValue, JLong highValue)
		{
			Span<JLong> longKey = stackalloc JLong[2];
			longKey[0] = lowValue;
			longKey[1] = highValue;
			if (!JNativeCallback.states.TryGetValue(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? state) ||
			    state is not IWrapper.IBase<RunnableState> ws) return;

			JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(environmentRef).Build();
			try
			{
				ws.Value.Run(callAdapter.Environment);
			}
			finally
			{
				callAdapter.FinalizeCall();
			}
		}

		static Runnable IClassType<Runnable>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
		static Runnable IClassType<Runnable>.Create(IReferenceType.ObjectInitializer initializer) => new(initializer);
		static Runnable IClassType<Runnable>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
	}
}