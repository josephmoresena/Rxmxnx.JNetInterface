namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface represents a function pointer based-struct replacement for <see cref="JInvokeInterface"/> type.
/// </summary>
/// <typeparam name="TNativeInterface">A <see cref="INativeInterface{TNativeInterface}"/> type.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2743,
                 Justification = CommonConstants.StaticAbstractPropertyUseJustification)]
internal interface INativeInterface<TNativeInterface>
	where TNativeInterface : unmanaged, INativeInterface<TNativeInterface>
{
	/// <summary>
	/// Required version for current interface.
	/// </summary>
	static abstract Int32 RequiredVersion { get; }
}