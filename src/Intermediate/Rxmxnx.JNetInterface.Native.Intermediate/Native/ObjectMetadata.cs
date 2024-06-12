namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JLocalObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record ObjectMetadata
{
	/// <inheritdoc cref="IObject.ObjectClassName"/>
	public CString ObjectClassName { get; }
	/// <inheritdoc cref="IObject.ObjectSignature"/>
	public CString ObjectSignature { get; }

	/// <summary>
	/// Class type metadata.
	/// </summary>
	internal JReferenceTypeMetadata? TypeMetadata { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="typeMetadata"><see cref="JReferenceTypeMetadata"/> instance.</param>
	internal ObjectMetadata(JClassObject jClass, JReferenceTypeMetadata? typeMetadata = default)
	{
		this.ObjectClassName = jClass.Name;
		this.ObjectSignature = jClass.ClassSignature;
		if (jClass.Name.AsSpan().SequenceEqual(CommonNames.ClassObject))
			this.TypeMetadata = IClassType.GetMetadata<JClassObject>();
		else
			this.TypeMetadata = typeMetadata ?? jClass.Environment.ClassFeature.GetTypeMetadata(jClass);
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	protected ObjectMetadata(ObjectMetadata metadata)
	{
		this.ObjectClassName = metadata.ObjectClassName;
		this.ObjectSignature = metadata.ObjectSignature;
		this.TypeMetadata = metadata.TypeMetadata;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="objectClassName">Class name of the current instance.</param>
	/// <param name="objectSignature">Class signature of the current instance.</param>
	private protected ObjectMetadata(CString objectClassName, CString objectSignature)
	{
		this.ObjectClassName = objectClassName;
		this.ObjectSignature = objectSignature;
		this.TypeMetadata = IClassType.GetMetadata<JClassObject>();
	}

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	public virtual String ToTraceText() => this.ToString();

	/// <summary>
	/// Retrieves the java class for the current object.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for the current object.</returns>
	internal JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass(this.ObjectClassName);
}