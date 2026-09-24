using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Functional;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.PInvoke;
#if NET9_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace Rxmxnx.JNetInterface;

// ReSharper disable once ClassCannotBeInstantiated
public partial class JNativeCallback
{
	public static JRunnableObject CreateRunnable(IEnvironment env, RunnableState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		Runnable.RegisterRun(env);

		return JNativeCallback.Create<Runnable, JRunnableObject>(env, longKey, state);
	}
	public static JActionListenerObject CreateActionListener(IEnvironment env, ActionListenerState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		ActionListener.RegisterActionPerformed(env);

		return JNativeCallback.Create<ActionListener, JActionListenerObject>(env, longKey, state);
	}
	public static JAwtEventListenerObject CreateAwtEventListener(IEnvironment env, AwtEventListenerState state)
	{
		Span<JLong> longKey = stackalloc JLong[2];
		JNativeCallback.CreateKey(env, longKey);
		AwtEventListener.RegisterEventDispatched(env);

		return JNativeCallback.Create<AwtEventListener, JAwtEventListenerObject>(env, longKey, state);
	}

	private static void CreateKey(IEnvironment env, Span<JLong> longKey)
	{
		ref Guid guidKey = ref Unsafe.As<JLong, Guid>(ref longKey[0]);
#if NET9_0_OR_GREATER
		guidKey = Guid.CreateVersion7();
#else
		guidKey = Guid.NewGuid();
#endif
		JNativeCallback.RegisterNatives(env);
	}
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	private static TInterface
		Create<TObject, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>(
			IEnvironment env, ReadOnlySpan<JLong> longKey, CallbackState state)
		where TObject : JNativeCallback, IInterfaceObject<TInterface>, IClassType<TObject>
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		Boolean createState = true;
		try
		{
			LongKeyArg arg = new(longKey);
			return JNativeCallback.constructorDef.NewCall<TObject, LongKeyArg>(env, in arg).CastTo<TInterface>();
		}
		catch (Exception)
		{
			createState = false;
			throw;
		}
		finally
		{
			if (createState)
				JNativeCallback.states.TryAdd(longKey.AsValues<JLong, Guid>()[0], state);
		}
	}

#if !NET9_0_OR_GREATER
	private readonly struct LongKeyArg(ReadOnlySpan<JLong> longKey) : ICallArgument
	{
		private readonly ReadOnlyValPtr<JLong> _ptr = longKey.GetUnsafeValPtr();

		private ReadOnlySpan<JLong> GetKey() => this._ptr.Pointer.GetUnsafeReadOnlySpan<JLong>(2);
#else
	private readonly ref struct LongKeyArg(ReadOnlySpan<JLong> longKey) : ICallArgument
	{
		private readonly ref JLong _ref = ref MemoryMarshal.GetReference(longKey);

		private ReadOnlySpan<JLong> GetKey() => MemoryMarshal.CreateReadOnlySpan(ref this._ref, 2);

		public String ToTraceText() => this.ToString();
#endif

		public void Configure<TSlot>(TSlot slot, JCallDefinition callDefinition) where TSlot : IParameterSlot
			=> slot.SetParameterValues(0, this.GetKey());

		public override String ToString()
		{
			ReadOnlySpan<JLong> longKey = this.GetKey();
			return $"[0: {longKey[0]}, {longKey[1]}]";
		}
	}
}