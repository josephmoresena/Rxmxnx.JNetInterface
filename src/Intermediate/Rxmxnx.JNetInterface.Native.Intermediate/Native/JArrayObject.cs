namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a local array instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JArrayObject : JLocalObject, IInterfaceObject<JSerializableObject>,
	IInterfaceObject<JCloneableObject>
{
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

	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is a valid element for the current array instance.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> is a valid element for the current array instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal abstract void ValidateObjectElement(JReferenceObject? jObject);

	/// <summary>
	/// Retrieves array element class name.
	/// </summary>
	/// <param name="arraySignature">A JNI array signature.</param>
	/// <param name="dimension">Output. Array dimension.</param>
	/// <returns>Element class name.</returns>
	protected static CString GetElementName(ReadOnlySpan<Byte> arraySignature, out Int32 dimension)
	{
		dimension = JClassObject.GetArrayDimension(arraySignature);
		return new(arraySignature[dimension] switch
		{
			UnicodePrimitiveSignatures.BooleanSignatureChar => UnicodeClassNames.BooleanPrimitive(),
			UnicodePrimitiveSignatures.ByteSignatureChar => UnicodeClassNames.BytePrimitive(),
			UnicodePrimitiveSignatures.CharSignatureChar => UnicodeClassNames.CharPrimitive(),
			UnicodePrimitiveSignatures.DoubleSignatureChar => UnicodeClassNames.DoublePrimitive(),
			UnicodePrimitiveSignatures.FloatSignatureChar => UnicodeClassNames.FloatPrimitive(),
			UnicodePrimitiveSignatures.IntSignatureChar => UnicodeClassNames.IntPrimitive(),
			UnicodePrimitiveSignatures.LongSignatureChar => UnicodeClassNames.LongPrimitive(),
			UnicodePrimitiveSignatures.ShortSignatureChar => UnicodeClassNames.ShortPrimitive(),
			_ => arraySignature[(dimension + 1)..^1],
		});
	}
}

/// <summary>
/// This class represents a local array instance.
/// </summary>
/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed partial class JArrayObject<TElement> : JLocalObject.ArrayView
	where TElement : IObject, IDataType<TElement>
{
	/// <summary>
	/// Metadata array instance.
	/// </summary>
	public static JArrayTypeMetadata Metadata => ArrayTypeMetadata.Instance;

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
	/// Defines an implicit conversion of a given <see cref="JArrayObject{TElement}"/> to <see cref="JArrayObject"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject{TElement}"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(jArray))]
	public static implicit operator JLocalObject?(JArrayObject<TElement>? jArray) => jArray?.Object;
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JArrayObject{TElement}"/> to <see cref="JArrayObject"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject{TElement}"/> to implicitly convert.</param>
	[return: NotNullIfNotNull(nameof(jArray))]
	public static implicit operator JArrayObject?(JArrayObject<TElement>? jArray) => jArray?.Object;
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JArrayObject"/> to <see cref="JArrayObject{TElement}"/>.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> to explicitly convert.</param>
	[return: NotNullIfNotNull(nameof(jArray))]
	public static explicit operator JArrayObject<TElement>?(JArrayObject? jArray)
	{
		if (jArray is null) return default;
		if (jArray is not IArrayObject<TElement>)
			JLocalObject.Validate<JArrayObject<TElement>>(jArray);
		return new(jArray);
	}
	/// <summary>
	/// Defines an explicit conversion of a given <see cref="JLocalObject"/> to <see cref="JArrayObject{TElement}"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> to explicitly convert.</param>
	[return: NotNullIfNotNull(nameof(jLocal))]
	public static explicit operator JArrayObject<TElement>?(JLocalObject? jLocal)
		=> (JArrayObject<TElement>?)(JArrayObject?)jLocal;

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