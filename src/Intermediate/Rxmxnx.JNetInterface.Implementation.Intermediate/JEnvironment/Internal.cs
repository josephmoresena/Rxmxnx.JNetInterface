namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
partial class JEnvironment
{
#if !PACKAGE
	/// <summary>
	/// Local cache.
	/// </summary>
	internal LocalCache LocalCache => this._m.LocalCache;
	/// <inheritdoc cref="IClassFeature.ClassObject"/>
	internal JClassObject ClassObject => this._m.Core.ClassObject;
#endif

	/// <summary>
	/// Sends JNI fatal error signal to VM.
	/// </summary>
	/// <param name="errorMessage">Error message.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal void FatalError(ReadOnlySpan<Byte> errorMessage) => EnvironmentCore.FatalError(this._m.Core, errorMessage);

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	internal static JEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		JVirtualMachineRef vmRef = EnvironmentCore.GetVirtualMachineRef(reference);
		JVirtualMachine vm = (JVirtualMachine)JVirtualMachine.GetVirtualMachine(vmRef);
		return vm.GetEnvironment(reference);
	}
}