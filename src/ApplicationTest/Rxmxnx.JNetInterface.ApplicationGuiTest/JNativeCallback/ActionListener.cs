using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface.ApplicationTest;
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
	private sealed unsafe class ActionListener : JNativeCallback, IClassType<ActionListener>,
		IInterfaceObject<JActionListenerObject>, IInterfaceObject<JEventListenerObject>
	{
		private static readonly JMethodDefinition actionPerformedDef = (JMethodDefinition)IndeterminateCall
			.CreateMethodDefinition("actionListener_actionPerformed"u8, [
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JLong>(),
				JArgumentMetadata.Get<JActionEventObject>(),
			]).Definition;
		private static readonly JClassTypeMetadata<ActionListener> nestedTypeMetadata =
			TypeMetadataBuilder<JNativeCallback>
				.Create<ActionListener>("com/rxmxnx/jnetinterface/NativeCallback$NativeActionListener"u8)
				.Implements<JActionListenerObject>().Build();

		private static Boolean actionPerformedRegistered;

		static JClassTypeMetadata<ActionListener> IClassType<ActionListener>.Metadata
			=> ActionListener.nestedTypeMetadata;

		private ActionListener(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private ActionListener(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private ActionListener(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

		public static void RegisterActionPerformed(IEnvironment env)
		{
			if (ActionListener.actionPerformedRegistered) return;

			using JClassObject jClass = JClassObject.GetClass<JNativeCallback>(env);
			delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JLong, JLong, JObjectLocalRef, void> ptr =
				&ActionListener.ActionPerformed;
			JNativeCallEntry entry = JNativeCallEntry.Create(ActionListener.actionPerformedDef, (IntPtr)ptr);

			jClass.Register(entry);
			ActionListener.actionPerformedRegistered = true;
		}

		[UnmanagedCallersOnly]
		private static void ActionPerformed(JEnvironmentRef environmentRef, JClassLocalRef _, JLong lowValue,
			JLong highValue, JObjectLocalRef localRef)
		{
			Span<JLong> longKey = stackalloc JLong[2];
			longKey[0] = lowValue;
			longKey[1] = highValue;
			if (!JNativeCallback.states.TryGetValue(Unsafe.As<JLong, Guid>(ref longKey[0]), out CallbackState? state) ||
			    state is not IWrapper.IBase<ActionListenerState> ws) return;

			try
			{
				JNativeCallAdapter callAdapter = JNativeCallAdapter.Create(environmentRef)
				                                                   .WithParameter(
					                                                   localRef, out JActionEventObject actionEvent)
				                                                   .Build();
				try
				{
					ws.Value.ActionPerformed(actionEvent);
				}
				finally
				{
					callAdapter.FinalizeCall();
				}
			}
			catch (RunningStateException) { }
		}

		static ActionListener IClassType<ActionListener>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static ActionListener IClassType<ActionListener>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static ActionListener IClassType<ActionListener>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}