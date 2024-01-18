namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <inheritdoc/>
	internal JEnumObject(InternalClassInitializer initializer) : base(
		IReferenceType.ClassInitializer.FromInternal(initializer)) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JEnumObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JEnumObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }
}