using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Native.References;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.JNetInterface.Util;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface;

public partial class JNativeCallback
{
	private sealed unsafe class AwtEventListener : JNativeCallback, IClassType<AwtEventListener>,
		IInterfaceObject<JAwtEventListenerObject>, IInterfaceObject<JEventListenerObject>
	{
		private static readonly JMethodDefinition eventDispatchedDef = (JMethodDefinition)IndeterminateCall
			.CreateMethodDefinition("awtEventListener_eventDispatched"u8, [
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JAwtEventObject>(),
			]).Definition;
		private static readonly JClassTypeMetadata<AwtEventListener> nestedTypeMetadata =
			TypeMetadataBuilder<JNativeCallback>
				.Create<AwtEventListener>("com/rxmxnx/jnetinterface/NativeCallback$NativeAWTEventListener"u8)
				.Implements<JAwtEventListenerObject>().Build();

		private static Boolean eventDispatchedRegistered;

		static JClassTypeMetadata<AwtEventListener> IClassType<AwtEventListener>.Metadata
			=> AwtEventListener.nestedTypeMetadata;

		private AwtEventListener(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private AwtEventListener(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private AwtEventListener(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

		public static void RegisterEventDispatched(IEnvironment env)
		{
			if (AwtEventListener.eventDispatchedRegistered) return;

			using JClassObject jClass = JClassObject.GetClass<JNativeCallback>(env);
			delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, JObjectLocalRef, void> ptr =
				&AwtEventListener.EventDispatched;
			JNativeCallEntry entry = JNativeCallEntry.Create(AwtEventListener.eventDispatchedDef, (IntPtr)ptr);

			jClass.Register(entry);
			AwtEventListener.eventDispatchedRegistered = true;
		}

		[UnmanagedCallersOnly]
		private static void EventDispatched(JEnvironmentRef environmentRef, JClassLocalRef _, JLong lowValue,
			JLong highValue, JObjectLocalRef localRef)
		{
			Span<JLong> longKey = stackalloc JLong[2];
			longKey[0] = lowValue;
			longKey[1] = highValue;
			if (!JNativeCallback.states.TryGetValue(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? state) ||
			    state is not IWrapper.IBase<AwtEventListenerState> ws) return;

			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(environmentRef)
				                                                   .WithParameter(
					                                                   localRef, out JAwtEventObject awtEvent).Build();

				try
				{
					ws.Value.EventDispatched(awtEvent);
				}
				finally
				{
					callAdapter.FinalizeCall();
				}
			}
			catch (RunningStateException) { }
		}

		static AwtEventListener IClassType<AwtEventListener>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static AwtEventListener IClassType<AwtEventListener>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static AwtEventListener IClassType<AwtEventListener>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}