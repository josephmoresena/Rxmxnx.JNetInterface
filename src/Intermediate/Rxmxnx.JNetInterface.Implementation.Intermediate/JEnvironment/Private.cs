namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
partial class JEnvironment
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly EnvironmentCore _core;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="core">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(EnvironmentCore core) => this._core = core;

	/// <summary>
	/// Retrieves the <see cref="ThrowableException"/> pending exception.
	/// </summary>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? GetThrown()
	{
		ThrowableException? jniException = this._core.Thrown as ThrowableException;
		if (jniException is not null || this._core.Thrown is null) return jniException;
		if (!this._core.JniSecure(JniSafetyLevels.ErrorSafe) && this._core.HasPendingException())
			// Do not throw if not pending JNI exception.
			throw this._core.Thrown;
		return this.ParseException(this._core.GetPendingException());
	}
	/// <summary>
	/// Parses <paramref name="throwableRef"/> to a <see cref="ThrowableException"/> instance.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	private ThrowableException? ParseException(JThrowableLocalRef throwableRef)
	{
		if (throwableRef == default) return default;
		ThrowableException jniException = this._core.CreateThrowableException(throwableRef);
		this._core.ThrowJniException(jniException, false);
		return jniException;
	}
	/// <summary>
	/// Sets <paramref name="throwableException"/> as pending exception.
	/// </summary>
	/// <param name="throwableException">A <see cref="ThrowableException"/> instance.</param>
	private void SetThrown(ThrowableException? throwableException)
	{
		if (throwableException is not null && Object.ReferenceEquals(CriticalException.Instance, this._core.Thrown) &&
		    this._core.HasPendingException())
			// Do not throw if there is no pending JNI exception or exception in the process of being cleared.
			throw this._core.Thrown;
		this._core.ThrowJniException(throwableException, false);
	}

	/// <summary>
	/// Indicates whether validation of <paramref name="jGlobal"/> can be avoided.
	/// </summary>
	/// <param name="cache">A <see cref="EnvironmentCore"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jGlobal"/> validation can be avoided;
	/// otherwise, <see langword="false"/>;
	/// </returns>
	private static Boolean IsValidationAvoidable(EnvironmentCore? cache, JGlobalBase jGlobal)
	{
		if (cache is null || !cache.Host.MemoryManager.SecureRemove(jGlobal.As<JObjectLocalRef>())) return true;
		Boolean isWeak = jGlobal is JWeak;
		if (!isWeak && LocalMainClasses.IsMainGlobal(jGlobal as JGlobal))
			return true;
		return Random.Shared.Next(0, 10) > (!isWeak ? 5 : 2);
	}
	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
#pragma warning disable CA1859
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
#pragma warning restore CA1859
}