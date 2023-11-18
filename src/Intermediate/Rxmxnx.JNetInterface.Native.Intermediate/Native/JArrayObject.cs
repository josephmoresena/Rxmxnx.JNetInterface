namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local array instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JArrayObject : JLocalObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(JArrayObjectMetadata);

	/// <summary>
	/// Array length.
	/// </summary>
	public Int32 Length => this._length ??= this.Environment.ArrayProvider.GetArrayLength(this);

	/// <summary>
	/// JNI array reference.
	/// </summary>
	internal JArrayLocalRef Reference => this.As<JArrayLocalRef>();
	/// <summary>
	/// Array type metadata.
	/// </summary>
	internal abstract JArrayTypeMetadata TypeMetadata { get; }

	/// <inheritdoc/>
	protected override JObjectMetadata CreateMetadata()
		=> new JArrayObjectMetadata(base.CreateMetadata()) { Length = this.Length, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(JObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not JArrayObjectMetadata arrayMetadata)
			return;
		this._length = arrayMetadata.Length;
	}
}

/// <summary>
/// This class represents a local array instance.
/// </summary>
/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
public sealed partial class JArrayObject<TElement> : JArrayObject, IArrayType<JArrayObject<TElement>>,
	IInterfaceImplementation<JArrayObject<TElement>, JSerializableObject>,
	IInterfaceImplementation<JArrayObject<TElement>, JCloneableObject> where TElement : IDataType<TElement>
{
	/// <summary>
	/// Gets or sets the element of <paramref name="index"/>.
	/// </summary>
	/// <param name="index">Element index.</param>
	[IndexerName("Value")]
	public TElement? this[Int32 index]
	{
		get => this.Environment.ArrayProvider.GetElement(this, index);
		set => this.Environment.ArrayProvider.SetElement(this, index, value);
	}

	/// <inheritdoc/>
	internal override JArrayTypeMetadata TypeMetadata => IArrayType.GetMetadata<JArrayObject<TElement>>();

	/// <inheritdoc/>
	public static JArrayObject<TElement>? Create(JLocalObject? jLocal)
	{
		if (JObject.IsNullOrDefault(jLocal))
			return default;

		JDataTypeMetadata elementMetadata = IDataType.GetMetadata<TElement>();
		if (jLocal is not JArrayObject jArray || jArray.TypeMetadata.ElementMetadata.Kind == JTypeKind.Primitive ||
		    elementMetadata.Kind == JTypeKind.Primitive)
			return new(JLocalObject.Validate<JArrayObject<TElement>>(jLocal));

		//TODO: Implement java array casting.
		return new(jLocal);
	}
	/// <inheritdoc/>
	public static JArrayObject<TElement>? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JArrayObject<TElement>>(jGlobal, env)) :
			default;
}