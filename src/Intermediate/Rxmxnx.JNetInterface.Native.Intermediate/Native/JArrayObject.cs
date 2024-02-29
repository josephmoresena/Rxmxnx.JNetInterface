namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local array instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JArrayObject : JLocalObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(ArrayObjectMetadata);

	/// <summary>
	/// Array length.
	/// </summary>
	public Int32 Length => this._length ??= this.Environment.ArrayFeature.GetArrayLength(this);

	/// <summary>
	/// JNI array reference.
	/// </summary>
	internal JArrayLocalRef Reference => base.As<JArrayLocalRef>();
	/// <summary>
	/// Array type metadata.
	/// </summary>
	internal abstract JArrayTypeMetadata TypeMetadata { get; }

	/// <inheritdoc/>
	protected override ObjectMetadata CreateMetadata()
		=> new ArrayObjectMetadata(base.CreateMetadata()) { Length = this.Length, };
	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not ArrayObjectMetadata arrayMetadata)
			return;
		this._length = arrayMetadata.Length;
	}

	/// <inheritdoc cref="JReferenceObject.As{TValue}()"/>
	internal new ref readonly TValue As<TValue>() where TValue : unmanaged, IArrayReferenceType<TValue>
		=> ref base.As<TValue>();
}

/// <summary>
/// This class represents a local array instance.
/// </summary>
/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
public sealed partial class JArrayObject<TElement> : JArrayObject, IInterfaceObject<JSerializableObject>,
	IInterfaceObject<JCloneableObject> where TElement : IObject, IDataType<TElement>
{
	/// <summary>
	/// Metadata array instance.
	/// </summary>
	public static JArrayTypeMetadata Metadata => ArrayTypeMetadata.Instance;

	/// <inheritdoc/>
	internal override JArrayTypeMetadata TypeMetadata => IArrayType.GetMetadata<JArrayObject<TElement>>();

	/// <summary>
	/// Gets or sets the element of <paramref name="index"/>.
	/// </summary>
	/// <param name="index">Element index.</param>
	[IndexerName("Value")]
	public TElement? this[Int32 index]
	{
		get => this.Environment.ArrayFeature.GetElement(this, index);
		set => this.Environment.ArrayFeature.SetElement(this, index, value);
	}

	/// <summary>
	/// Creates an empty <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="length">New array length.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	public static JArrayObject<TElement> Create(IEnvironment env, Int32 length)
		=> env.ArrayFeature.CreateArray<TElement>(length);
	/// <summary>
	/// Creates a <paramref name="initialElement"/> filled <see cref="JArrayObject{TElement}"/> instance.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="length">New array length.</param>
	/// <param name="initialElement">Instance to set each array element.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance.</returns>
	public static JArrayObject<TElement> Create(IEnvironment env, Int32 length, TElement initialElement)
		=> env.ArrayFeature.CreateArray(length, initialElement);
}