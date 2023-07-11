namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a JNI reference provider instance.
/// </summary>
public interface IReferenceProvider
{
	/// <summary>
	/// Reloads the local reference of given object if the current local reference is not loaded but has a
	/// global reference.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the local reference was loaded; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean ReloadObject(JLocalObject jLocal);
	/// <summary>
	/// Reloads the global reference of given global class object.
	/// </summary>
	/// <typeparam name="TGlobalClass">A global class object.</typeparam>
	/// <param name="jGlobalClass">A <typeparamref name="TGlobalClass"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the global reference was loaded; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean ReloadGlobalClass<TGlobalClass>(TGlobalClass jGlobalClass) where TGlobalClass : JGlobalBase, IClass;
	/// <summary>
	/// Creates a <typeparamref name="TGlobal"/> instance loaded for given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>
	/// A new <typeparamref name="TGlobal"/> instance loaded for <paramref name="jLocal"/>.
	/// </returns>
	TGlobal? Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase;
	/// <summary>
	/// Unloads the local reference of <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	Boolean Unload(JLocalObject jLocal);
	/// <summary>
	/// Unloads the global reference of <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	void Unload(JGlobalBase jGlobal);
}