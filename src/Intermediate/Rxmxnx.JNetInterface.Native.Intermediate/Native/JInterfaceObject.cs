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

	/// <inheritdoc />
	internal JInterfaceObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef)
		=> this.ObjectMetadata = new(jClass);
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
	protected JInterfaceObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }
	/// <inheritdoc/>
	protected JInterfaceObject(JLocalObject jLocal) : base(jLocal) { }
	/// <inheritdoc/>
	protected JInterfaceObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}