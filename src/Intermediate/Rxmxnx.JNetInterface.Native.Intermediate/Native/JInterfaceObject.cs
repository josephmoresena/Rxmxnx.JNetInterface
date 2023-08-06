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
	private readonly JObjectMetadata _metadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected JInterfaceObject(JLocalObject jLocal) : base(
		JInterfaceObject.Load(jLocal, out JClassObject jClass, out JObjectMetadata metadata), jClass)
		=> this._metadata = metadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	protected JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
		=> this._metadata = jGlobal.ObjectMetadata;

	/// <inheritdoc cref="JLocalObject.CreateMetadata()"/>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override JObjectMetadata CreateMetadata() => this._metadata;

	/// <inheritdoc cref="JLocalObject.ProcessMetadata(JObjectMetadata)"/>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected new void ProcessMetadata(JObjectMetadata instanceMetadata) => base.ProcessMetadata(instanceMetadata);

	/// <summary>
	/// Loads <paramref name="jLocal"/> instance to be created as <see cref="JInterfaceObject"/> instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">Output. Loaded class from <paramref name="jLocal"/> instance.</param>
	/// <param name="metadata">Output. Metadata from <paramref name="jLocal"/>.</param>
	/// <returns><paramref name="jLocal"/> instance.</returns>
	private static JLocalObject Load(JLocalObject jLocal, out JClassObject jClass, out JObjectMetadata metadata)
	{
		metadata = JLocalObject.CreateMetadata(jLocal);
		jClass = jLocal.Class;
		return jLocal;
	}
}