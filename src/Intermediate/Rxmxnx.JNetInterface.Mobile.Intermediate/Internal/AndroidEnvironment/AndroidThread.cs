#if !NET8_0_OR_GREATER
using System.Reflection;
#endif

namespace Rxmxnx.JNetInterface.Internal;

internal partial class AndroidEnvironment
{
	/// <summary>
	/// Internal implementation of <see cref="IThread"/>.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3011,
	                 Justification = CommonConstants.ReflectionPrivateUseJustification)]
#endif
	public sealed class AndroidThread : AndroidEnvironment, IThread
	{
#if !NET8_0_OR_GREATER
		/// <summary>
		/// Name of JniEnvironment.SetEnvironmentPointer(IntPtr) method.
		/// </summary>
		private const String setEnvironmentPointerName = "SetEnvironmentPointer";
		/// <summary>
		/// Binding flags for JniEnvironment.SetEnvironmentPointer(IntPtr) method.
		/// </summary>
		private const BindingFlags setEnvironmentPointerFlags =
			BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
		/// <summary>
		/// The <see cref="MethodInfo"/> instance for JniEnvironment.SetEnvironmentPointer(IntPtr) method.
		/// </summary>
		private static readonly MethodInfo? setEnvironmentPointerMethod =
			typeof(JniEnvironment).GetMethod(AndroidThread.setEnvironmentPointerName,
			                                 AndroidThread.setEnvironmentPointerFlags);
		/// <summary>
		/// <see cref="IntPtr.Zero"/> for <see cref="setEnvironmentPointerMethod"/> parameter.
		/// </summary>
		private static readonly Object[] setEnvironmentPointerArgs = [IntPtr.Zero,];
#endif

		/// <summary>
		/// <see cref="ThreadValue"/> instance.
		/// </summary>
		private readonly ThreadValue _value;

		/// <inheritdoc/>
		public override Boolean IsAttached => this._value.IsAttached(this._m.Core);

		/// <inheritdoc/>
		public AndroidThread(IVirtualMachineHost host, JEnvironmentRef envRef, ThreadCreationArgs args) :
			base(host, envRef)
			=> this._value = new(args);
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="env">Original env instance.</param>
		/// <param name="newThread">Indicates whether the created thread is new.</param>
		public AndroidThread(AndroidEnvironment env, Boolean newThread) : base(env._m.Core)
			=> this._value = new((env as AndroidThread)?._value, newThread);

		Boolean IThread.Daemon => this._value.IsDaemon;
		CString IThread.Name => this._value.Name;
		Boolean IThread.Attached => this.IsAttached;

		/// <inheritdoc/>
		public void Dispose()
		{
			Boolean hasInteropContext = JniEnvironment.EnvironmentPointer != default;
			if (this._m.Core.IsOwned && this._value.IsDisposable && hasInteropContext)
			{
				JniRuntime.CurrentRuntime.ValueManager.CollectPeers();
				AndroidThread.ResetPointer();
			}
			this._value.FinalizeThread(this._m.Core, this, this._m.Core.IsOwned);
		}

		/// <summary>
		/// Resets the current <c>JNIEnv*</c> value from Java.Interop.
		/// </summary>
		private static void ResetPointer()
#if !NET8_0_OR_GREATER
		{
			if (AndroidThread.setEnvironmentPointerMethod is null)
			{
				IMessageResource resource = IMessageResource.GetInstance();
				throw new InvalidOperationException(resource.MissingSetEnvironmentPointerMethod);
			}
			AndroidThread.setEnvironmentPointerMethod.Invoke(null, AndroidThread.setEnvironmentPointerArgs);
		}
#else
			=> AndroidThread.SetEnvironmentPointer(IntPtr.Zero);
		/// <summary>
		/// Sets the current <c>JNIEnv*</c> value for current thread.
		/// </summary>
		/// <param name="env">A <c>JNIEnv*</c> pointer.</param>
		[UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "SetEnvironmentPointer")]
		private static extern void SetEnvironmentPointer(IntPtr env);
#endif
	}
}