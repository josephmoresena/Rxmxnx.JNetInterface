namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Represents a reference type token used for processing reference-type entities.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct JReferenceTypeToken
{
	/// <summary>
	/// JNI reference to the underlying Java object.
	/// </summary>
	public JObjectLocalRef Reference { get; private init; }
	/// <summary>
	/// Represents the native type associated with the JNI reference.
	/// </summary>
	public JNativeType NativeType { get; init; }

	/// <summary>
	/// Points to a function responsible for retrieving metadata of the reference type.
	/// </summary>
	private delegate*<JReferenceTypeMetadata> GetMetadataPointer { get; init; }

	/// <summary>
	/// Retrieves the metadata associated with the current token.
	/// </summary>
	/// <returns>The <see cref="JReferenceTypeMetadata"/> instance associated with the current token.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public JReferenceTypeMetadata? GetMetadata()
		=> this.GetMetadataPointer != default ? this.GetMetadataPointer() : default;

	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JObjectLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JObjectLocalRef localRef)
		=> new() { Reference = localRef, GetMetadataPointer = default, NativeType = JNativeType.JObject, };
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JClassLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JClassLocalRef classRef)
		=> new()
		{
			Reference = classRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JClassObject>,
			NativeType = JNativeType.JClass,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JStringLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="stringRef">A <see cref="JStringLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JStringLocalRef stringRef)
		=> new()
		{
			Reference = stringRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JStringObject>,
			NativeType = JNativeType.JString,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JThrowableLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JThrowableLocalRef throwableRef)
		=> new()
		{
			Reference = throwableRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JThrowableObject>,
			NativeType = JNativeType.JThrowable,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JArrayLocalRef arrayRef)
		=> new() { Reference = arrayRef.Value, GetMetadataPointer = default, NativeType = JNativeType.JArray, };
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JObjectArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JObjectArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JObjectArrayLocalRef arrayRef)
		=> new() { Reference = arrayRef.Value, GetMetadataPointer = default, NativeType = JNativeType.JObjectArray, };
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JBooleanArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JBooleanArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JBooleanArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JBoolean>>,
			NativeType = JNativeType.JBooleanArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JByteArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JByteArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JByteArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JByte>>,
			NativeType = JNativeType.JByteArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JCharArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JCharArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JCharArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JChar>>,
			NativeType = JNativeType.JCharArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JDoubleArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JDoubleArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JDoubleArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JDouble>>,
			NativeType = JNativeType.JDoubleArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JFloatArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JFloatArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JFloatArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JFloat>>,
			NativeType = JNativeType.JFloatArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JIntArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JIntArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JIntArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JInt>>,
			NativeType = JNativeType.JIntArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JLongArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JLongArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JLongArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JLong>>,
			NativeType = JNativeType.JLongArray,
		};
	/// <summary>
	/// Defines an implicit conversion of a given <see cref="JShortArrayLocalRef"/> to
	/// <see cref="JReferenceTypeToken"/>.
	/// </summary>
	/// <param name="arrayRef">A <see cref="JShortArrayLocalRef"/> to implicitly convert.</param>
	public static implicit operator JReferenceTypeToken(JShortArrayLocalRef arrayRef)
		=> new()
		{
			Reference = arrayRef.Value,
			GetMetadataPointer = &IReferenceType.GetMetadata<JArrayObject<JShort>>,
			NativeType = JNativeType.JShortArray,
		};

	/// <summary>
	/// Creates an instance of <see cref="JReferenceTypeToken"/> from the specified <see cref="JObjectLocalRef"/>.
	/// </summary>
	/// <typeparam name="TReference">Type of java reference type.</typeparam>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> to be converted into a <see cref="JReferenceTypeToken"/>.</param>
	/// <returns>
	/// A new instance of <see cref="JReferenceTypeToken"/> initialized with the provided <paramref name="localRef"/>
	/// and metadata for <typeparamref name="TReference"/>.
	/// </returns>
	public static JReferenceTypeToken
		Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>(
			JObjectLocalRef localRef)
		where TReference : JReferenceObject, IReferenceType<TReference>
		=> new()
		{
			Reference = localRef,
			GetMetadataPointer = &IReferenceType.GetMetadata<TReference>,
			NativeType = JReferenceTypeToken.GetNativeType<TReference>(),
		};

	/// <summary>
	/// Determines the corresponding <see cref="JNativeType"/> for the specified reference type parameter.
	/// </summary>
	/// <typeparam name="TReference">The reference type for which the corresponding native type is resolved. It must inherit from <see cref="JReferenceObject"/> and implement <see cref="IReferenceType{TReference}"/>.</typeparam>
	/// <returns>The <see cref="JNativeType"/> value that represents the native type associated with the given reference type.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JNativeType GetNativeType<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TReference>()
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		JNativeType nativeType = JNativeType.JObject;
		Type typeofT = typeof(TReference);
		if (typeofT == typeof(JClassObject))
			nativeType = JNativeType.JClass;
		else if (typeofT == typeof(JStringObject))
			nativeType = JNativeType.JString;
		else if (TReference.FamilyType == typeof(JThrowableObject))
			nativeType = JNativeType.JThrowable;
		else if (TReference.FamilyType == typeof(JArrayObject))
		{
			if (typeofT == typeof(JArrayObject<JBoolean>))
				nativeType = JNativeType.JBooleanArray;
			else if (typeofT == typeof(JArrayObject<JByte>))
				nativeType = JNativeType.JByteArray;
			else if (typeofT == typeof(JArrayObject<JChar>))
				nativeType = JNativeType.JCharArray;
			else if (typeofT == typeof(JArrayObject<JDouble>))
				nativeType = JNativeType.JDoubleArray;
			else if (typeofT == typeof(JArrayObject<JFloat>))
				nativeType = JNativeType.JFloatArray;
			else if (typeofT == typeof(JArrayObject<JInt>))
				nativeType = JNativeType.JIntArray;
			else if (typeofT == typeof(JArrayObject<JLong>))
				nativeType = JNativeType.JLongArray;
			else if (typeofT == typeof(JArrayObject<JShort>))
				nativeType = JNativeType.JShortArray;
			else
				nativeType = JNativeType.JObjectArray;
		}
		return nativeType;
	}
}