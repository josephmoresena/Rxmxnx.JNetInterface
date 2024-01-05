namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI reference provider instance.
/// </summary>
public partial interface IReferenceProvider
{
	/// <summary>
	/// Creates a <typeparamref name="TGlobal"/> instance loaded for given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// A new <typeparamref name="TGlobal"/> instance loaded for <paramref name="jLocal"/>.
	/// </returns>
	TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	/// <summary>
	/// Unloads the local reference of <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	Boolean Unload(JLocalObject jLocal);
	/// <summary>
	/// Unloads the global reference of <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	Boolean Unload(JGlobalBase jGlobal);
	/// <summary>
	/// Indicates whether current instance is a parameter.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jLocal"/> is a JNI parameter; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsParameter(JLocalObject jLocal);
}