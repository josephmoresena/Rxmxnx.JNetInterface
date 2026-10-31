namespace Rxmxnx.JNetInterface;

internal readonly partial struct EnvironmentValue
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="core">A <see cref="EnvironmentCore"/> instance.</param>
	private EnvironmentValue(EnvironmentCore core) => this.Core = core;

	/// <summary>
	/// Retrieves the <see cref="ThrowableException"/> pending exception.
	/// </summary>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? GetThrown()
	{
		ThrowableException? jniException = this.Core.Thrown as ThrowableException;
		if (jniException is not null || this.Core.Thrown is null) return jniException;
		if (!this.Core.JniSecure(JniSafetyLevels.ErrorSafe) && this.Core.HasPendingException())
			// Do not throw if not pending JNI exception.
			throw this.Core.Thrown;
		return EnvironmentCore.ParseException(this.Core, this.Core.GetPendingException());
	}
	/// <summary>
	/// Sets <paramref name="throwableException"/> as pending exception.
	/// </summary>
	/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
	private void SetThrown(ThrowableException? throwableException)
	{
		if (throwableException is not null && Object.ReferenceEquals(CriticalException.Instance, this.Core.Thrown) &&
		    this.Core.HasPendingException())
			// Do not throw if there is no pending JNI exception or exception in the process of being cleared.
			throw this.Core.Thrown;
		this.Core.ThrowJniException(throwableException, false);
	}
}