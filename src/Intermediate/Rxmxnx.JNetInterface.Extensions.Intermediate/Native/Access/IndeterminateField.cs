namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores the definition of an indeterminate java field.
/// </summary>
public sealed partial class IndeterminateField : IWrapper<JFieldDefinition>
{
	/// <summary>
	/// Field type signature.
	/// </summary>
	public CString FieldType { get; }
	/// <summary>
	/// Internal field definition.
	/// </summary>
	public JFieldDefinition Definition { get; }

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	JFieldDefinition IWrapper<JFieldDefinition>.Value => this.Definition;

	/// <summary>
	/// Creates a <see cref="IndeterminateField"/> instance for a java field.
	/// </summary>
	/// <param name="returnType">Return type metadata.</param>
	/// <param name="fieldName">UTF-8 field name.</param>
	/// <returns>A new <see cref="IndeterminateField"/> instance.</returns>
	public static IndeterminateField Create(JArgumentMetadata returnType, ReadOnlySpan<Byte> fieldName)
	{
		JFieldDefinition definition = returnType.Signature.Length == 1 ?
			IndeterminateField.CreatePrimitiveField(fieldName, returnType.Signature) :
			new JNonTypedFieldDefinition(fieldName, returnType.Signature);
		return new(definition, returnType.Signature);
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateField"/> instance for a java field.
	/// </summary>
	/// <typeparam name="TResult">Return <see cref="IDataType{TResult}"/> type.</typeparam>
	/// <param name="fieldName">UTF-8 field name.</param>
	/// <returns>A new <see cref="IndeterminateField"/> instance.</returns>
	public static IndeterminateField Create<TResult>(ReadOnlySpan<Byte> fieldName) where TResult : IDataType<TResult>
	{
		JDataTypeMetadata typeMetadata = IDataType.GetMetadata<TResult>();
		JFieldDefinition definition = typeMetadata is not JReferenceTypeMetadata referenceTypeMetadata ?
			IndeterminateField.CreatePrimitiveField(fieldName, typeMetadata.Signature) :
			referenceTypeMetadata.CreateFieldDefinition(fieldName);
		return new(definition, typeMetadata.Signature);
	}

	/// <inheritdoc cref="IndeterminateHelper.ReflectedGet(JFieldObject, JLocalObject)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedGet(JFieldObject jField, JLocalObject jLocal)
		=> IndeterminateHelper.ReflectedGet(jField, jLocal);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedStaticGet(JFieldObject)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IndeterminateResult ReflectedStaticGet(JFieldObject jField)
		=> IndeterminateHelper.ReflectedStaticGet(jField);

	/// <inheritdoc cref="IndeterminateHelper.ReflectedSet{TValue}(JFieldObject, JLocalObject, TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedSet<TValue>(JFieldObject jField, JLocalObject jLocal, TValue? value)
		where TValue : IObject
		=> IndeterminateHelper.ReflectedSet(jField, jLocal, value);
	/// <inheritdoc cref="IndeterminateHelper.ReflectedStaticSet{TValue}(JFieldObject, TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ReflectedStaticSet<TValue>(JFieldObject jField, TValue? value) where TValue : IObject
		=> IndeterminateHelper.ReflectedStaticSet(jField, value);

	/// <summary>
	/// Creates a <see cref="IndeterminateResult"/> from <paramref name="value"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="value">A <typeparamref name="TValue"/> instance.</param>
	/// <param name="fieldTypeSignature">Field type signature.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	internal static IndeterminateResult GetFieldValue<TValue>(IEnvironment env, TValue? value,
		ReadOnlySpan<Byte> fieldTypeSignature) where TValue : IObject
	{
		if (value is null) return new(default(JLocalObject), fieldTypeSignature);

		ReadOnlySpan<Byte> valueSignature = value.ObjectSignature;
		if (valueSignature.Length == 1)
		{
			Span<JValue.PrimitiveValue> pValue = stackalloc JValue.PrimitiveValue[1];
			value.CopyTo(pValue.AsBytes());
			return new(pValue[0], valueSignature);
		}

		JReferenceObject jObject = (JReferenceObject)(Object)value; // Should be.
		return jObject switch
		{
			JBooleanObject jBoolean => new(jBoolean, valueSignature),
			JByteObject jByte => new(jByte, valueSignature),
			JCharacterObject jCharacter => new(jCharacter, valueSignature),
			JDoubleObject jDouble => new(jDouble, valueSignature),
			JFloatObject jFloat => new(jFloat, valueSignature),
			JIntegerObject jInteger => new(jInteger, valueSignature),
			JLongObject jLong => new(jLong, valueSignature),
			JShortObject jShort => new(jShort, valueSignature),
			JNumberObject jNumber => new(jNumber, valueSignature),
			// Unknown JObject instance.
			_ => IndeterminateField.GetFieldValue(env, jObject, valueSignature, fieldTypeSignature[0]),
		};
	}
}