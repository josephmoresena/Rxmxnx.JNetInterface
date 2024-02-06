namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local interface instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JInterfaceObject : JLocalObject, IInterfaceType
{
	static JTypeKind IDataType.Kind => JTypeKind.Interface;
	static Type IDataType.FamilyType => typeof(JInterfaceObject);

	/// <summary>
	/// Instance metadata.
	/// </summary>
	internal ObjectMetadata ObjectMetadata { get; }

	/// <inheritdoc/>
	private protected JInterfaceObject(IReferenceType.ClassInitializer initializer) : base(initializer)
		=> this.ObjectMetadata = new(initializer.Class!);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	internal JInterfaceObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata), jClass)
		=> this.ObjectMetadata = metadata;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
		=> this.ObjectMetadata = jGlobal.ObjectMetadata;

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected override ObjectMetadata CreateMetadata() => this.ObjectMetadata;
}

/// <summary>
/// This class represents a local interface instance.
/// </summary>
/// <typeparam name="TInterface">Type of <see cref="IInterfaceType"/>.</typeparam>
public abstract class JInterfaceObject<TInterface> : JInterfaceObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	protected JInterfaceObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JInterfaceObject(IReferenceType.GlobalInitializer initializer) : base(
		initializer.Environment, initializer.Global) { }
	/// <inheritdoc/>
	protected JInterfaceObject(IReferenceType.ObjectInitializer initializer) : base(initializer.Instance) { }
}