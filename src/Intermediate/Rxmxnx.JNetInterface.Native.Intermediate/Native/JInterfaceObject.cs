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
	/// <see cref="JLocalObject"/> metadata.
	/// </summary>
	private readonly ObjectMetadata _objectMetadata;

	/// <summary>
	/// Instance metadata.
	/// </summary>
	internal ObjectMetadata ObjectMetadata => this._objectMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	internal JInterfaceObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out ObjectMetadata metadata), jClass)
		=> this._objectMetadata = metadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
		=> this._objectMetadata = jGlobal.ObjectMetadata;

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	protected override ObjectMetadata CreateMetadata() => this._objectMetadata;
}

/// <summary>
/// This class represents a local interface instance.
/// </summary>
/// <typeparam name="TInterface">Type of <see cref="IInterfaceType"/>.</typeparam>
public abstract class JInterfaceObject<TInterface> : JInterfaceObject
	where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
{
	/// <inheritdoc/>
	protected JInterfaceObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	protected JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}