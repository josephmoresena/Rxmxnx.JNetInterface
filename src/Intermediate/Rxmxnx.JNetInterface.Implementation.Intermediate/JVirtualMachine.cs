namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements the <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JVirtualMachine : IVirtualMachine, IMainClassSet
{
	/// <summary>
	/// Android API level.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Int32? AndroidApiLevel => AndroidHelper.IsZygote ? AndroidHelper.ApiLevel : default;

	/// <summary>
	/// Indicates whether the current virtual machine remains alive.
	/// </summary>
	public virtual Boolean IsAlive => true;
	/// <inheritdoc cref="IVirtualMachine.Version"/>
	public JRuntimeVersion Version => this._cache.GetVersion(this);
	/// <inheritdoc/>
	public JVirtualMachineRef Reference => this._cache.Reference;

	Boolean IVirtualMachine.NoProxy => true;
	IEnvironment? IVirtualMachine.GetEnvironment() => this.GetEnvironment();
	JRuntimeVersion IVirtualMachine.Version
	{
		get
		{
			if (JavaStandardFeature.GetRuntimeVersion() is { } jre) return jre;
			return AndroidFeature.IsFixedAndroid ? JRuntimeVersion.J6 : this.Version;
		}
	}
	Int32 IVirtualMachine.AndroidApiLevel
	{
		get
		{
			if (JavaStandardFeature.IsFixedRuntimeVersion) return -1;
			if (AndroidFeature.ApiLevel is { } apiLevel) return apiLevel;
			return JVirtualMachine.AndroidApiLevel ?? -1;
		}
	}

	IThread IVirtualMachine.CreateThread(ThreadPurpose purpose)
	{
		if (!this.IsAlive) return new DeadThread(this);
		ThreadCreationArgs args = ThreadCreationArgs.Create(purpose);
		try
		{
			return this.AttachThread(args);
		}
		catch (Exception)
		{
			switch (purpose)
			{
				case ThreadPurpose.ReleaseSequence:
				case ThreadPurpose.RemoveGlobalReference:
				case ThreadPurpose.CheckGlobalReference:
				case ThreadPurpose.CheckAssignability:
				case ThreadPurpose.SynchronizeGlobalReference:
					return new DeadThread(this);
				case ThreadPurpose.ExceptionExecution:
				case ThreadPurpose.CreateGlobalReference:
				case ThreadPurpose.FatalError:
				case ThreadPurpose.GetRuntimeVersion:
				default:
					throw;
			}
		}
	}
	IThread IVirtualMachine.InitializeThread(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.AttachThread(new() { Name = threadName, ThreadGroup = threadGroup, Version = version, });
	IThread IVirtualMachine.InitializeDaemon(CString? threadName, JGlobalBase? threadGroup, Int32 version)
		=> this.AttachThread(new()
		{
			Name = threadName, ThreadGroup = threadGroup, Version = version, IsDaemon = true,
		});

	/// <inheritdoc/>
	public void FatalError(String? message) => this.FatalError((CString?)message);
	/// <inheritdoc/>
	public void FatalError(CString? message)
	{
		ReadOnlySpan<Byte> utf8Message = JEnvironment.GetSafeSpan(message);
		using IThread thread = this.AttachThread(ThreadCreationArgs.Create(ThreadPurpose.FatalError));
		JEnvironment env = this.GetEnvironment(thread.Reference);
		env.FatalError(utf8Message);
	}
}