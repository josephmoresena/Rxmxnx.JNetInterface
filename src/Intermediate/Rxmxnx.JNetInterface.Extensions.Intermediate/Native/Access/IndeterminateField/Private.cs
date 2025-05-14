namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="definition">Internal definition.</param>
	/// <param name="fieldType">Field type signature.</param>
	private IndeterminateField(JFieldDefinition definition, CString fieldType)
	{
		this.Definition = definition;
		this.FieldType = fieldType;
	}

	/// <summary>
	/// Creates a primitive <see cref="JFieldDefinition"/> instance.
	/// </summary>
	/// <param name="fieldName">UTF-8 function name.</param>
	/// <param name="fieldTypeSignature">Field type signature.</param>
	/// <returns>A <see cref="JFieldDefinition"/> instance.</returns>
	private static JFieldDefinition CreatePrimitiveField(ReadOnlySpan<Byte> fieldName,
		ReadOnlySpan<Byte> fieldTypeSignature)
		=> fieldTypeSignature[0] switch
		{
			CommonNames.BooleanSignatureChar => new JFieldDefinition<JBoolean>(fieldName),
			CommonNames.ByteSignatureChar => new JFieldDefinition<JByte>(fieldName),
			CommonNames.CharSignatureChar => new JFieldDefinition<JChar>(fieldName),
			CommonNames.DoubleSignatureChar => new JFieldDefinition<JDouble>(fieldName),
			CommonNames.FloatSignatureChar => new JFieldDefinition<JFloat>(fieldName),
			CommonNames.IntSignatureChar => new JFieldDefinition<JInt>(fieldName),
			CommonNames.LongSignatureChar => new JFieldDefinition<JLong>(fieldName),
			_ => new JFieldDefinition<JShort>(fieldName),
		};
	/// <summary>
	/// Copies the primitive value of a reflected field instance to <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="jField">Reflected field object.</param>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static void CopyReflectedPrimitiveFieldValue<TPrimitive>(Span<Byte> bytes, JFieldObject jField,
		JLocalObject? jLocal = default) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jField.Environment;
		TPrimitive result = jLocal is not null ?
			env.AccessFeature.GetField<TPrimitive>(jField, jLocal, jField.Definition) :
			env.AccessFeature.GetStaticField<TPrimitive>(jField, jField.Definition);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Sets the value
	/// </summary>
	/// <param name="jClass"></param>
	/// <param name="definition"></param>
	/// <param name="fieldValue"></param>
	/// <param name="jLocal"></param>
	private static void SetFieldObject(JClassObject jClass, JFieldDefinition definition, IndeterminateResult fieldValue,
		JLocalObject? jLocal = default)
	{
		IEnvironment env = jClass.Environment;
		JLocalObject? jObject = IndeterminateField.CreateObject(env, fieldValue, out Boolean newObject);
		try
		{
			if (jLocal is not null)
				env.AccessFeature.SetField(jLocal, jClass, definition, jObject);
			else
				env.AccessFeature.SetStaticField(jClass, definition, jObject);
		}
		finally
		{
			if (newObject)
				jObject?.Dispose();
		}
	}
	/// <summary>
	/// Sets the value of a reflected field instance from <paramref name="fieldValue"/> instance.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="fieldValue">Value to set to.</param>
	/// <param name="jLocal">Target object.</param>
	private static void SetReflectedFieldObject(JFieldObject jField, IndeterminateResult fieldValue,
		JLocalObject? jLocal = default)
	{
		IEnvironment env = jField.Environment;
		JLocalObject? jObject = IndeterminateField.CreateObject(env, fieldValue, out Boolean newObject);
		try
		{
			if (jLocal is not null)
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, jObject);
			else
				env.AccessFeature.SetStaticField(jField, jField.Definition, jObject);
		}
		finally
		{
			if (newObject)
				jObject?.Dispose();
		}
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateResult"/> from <paramref name="value"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="value">A <typeparamref name="TValue"/> instance.</param>
	/// <param name="fieldTypeSignature">Field type signature.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static IndeterminateResult GetFieldValue<TValue>(IEnvironment env, TValue? value,
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
	/// <summary>
	/// Creates a <see cref="IndeterminateResult"/> from <paramref name="jObject"/> instance.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="valueSignature">Value signature instance.</param>
	/// <param name="fieldTypeSignaturePrefix">Field signature prefix.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static IndeterminateResult GetFieldValue(IEnvironment env, JReferenceObject jObject,
		ReadOnlySpan<Byte> valueSignature, Byte fieldTypeSignaturePrefix)
	{
		IndeterminateResult result = IndeterminateResult.Empty;
		if (fieldTypeSignaturePrefix is CommonNames.ObjectSignaturePrefixChar or CommonNames.ArraySignaturePrefixChar)
		{
			result = IndeterminateField.GetFieldValue<JLocalObject>(env, jObject, valueSignature);
		}
		else
		{
			InfoObjectQuery infoQuery = new() { PrimitiveSignature = fieldTypeSignaturePrefix, Object = jObject, };
			InfoObjectResult infoResult = env.WithFrame(IVirtualMachine.GetObjectClassCapacity, infoQuery,
			                                            IndeterminateField.GetWrapperInfo);
			if (infoResult.IsBoolean)
				result = IndeterminateField.GetFieldValue<JByteObject>(env, jObject, valueSignature);
			else if (infoResult.IsNumber)
				result = IndeterminateField.GetFieldValue<JNumberObject>(env, jObject, valueSignature);
			else if (infoResult.IsCharacter)
				result = IndeterminateField.GetFieldValue<JCharacterObject>(env, jObject, valueSignature);
			else
				CommonValidationUtilities.ThrowInvalidCastToPrimitive(fieldTypeSignaturePrefix);
		}
		return result;
	}
	/// <summary>
	/// Retrieves the wrapper information from <paramref name="query"/>.
	/// </summary>
	/// <param name="query">A <see cref="InfoObjectQuery"/> value.</param>
	/// <returns>A <see cref="InfoObjectResult"/> instance.</returns>
	private static InfoObjectResult GetWrapperInfo(InfoObjectQuery query)
	{
		Boolean isBoolean = query.PrimitiveSignature is CommonNames.BooleanSignatureChar &&
			query.Object.InstanceOf<JBooleanObject>();
		Boolean isNumber = !isBoolean && query.Object.InstanceOf<JNumberObject>();
		Boolean isCharacter = !isBoolean && !isNumber && query.Object.InstanceOf<JCharacterObject>();
		return new() { IsBoolean = isBoolean, IsCharacter = isCharacter, IsNumber = isNumber, };
	}
	/// <summary>
	/// Creates a <see cref="IndeterminateResult"/> from <paramref name="jObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="valueSignature">Value signature instance.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static IndeterminateResult
		GetFieldValue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(IEnvironment env,
			JReferenceObject jObject, ReadOnlySpan<Byte> valueSignature)
		where TObject : JLocalObject, IClassType<TObject>
		=> jObject switch
		{
			TObject objValue => new(objValue, valueSignature),
			JLocalObject jLocalObject => new(jLocalObject.Weak.AsLocal<TObject>(env), valueSignature), // Special case.
			JGlobalBase jGlobal => new(jGlobal.AsLocal<TObject>(env), valueSignature),
			_ => new(((ILocalObject)jObject).CastTo<TObject>(), valueSignature),
		};
	/// <summary>
	/// Retrieves a <see cref="JLocalObject"/> from <paramref name="value"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="value">A <see cref="IndeterminateResult"/> instance.</param>
	/// <param name="newObject">Output. Indicates whether resulting instance is created from primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	private static JLocalObject? CreateObject(IEnvironment env, IndeterminateResult value, out Boolean newObject)
	{
		newObject = false;
		if (value.Object is not null)
			return value.Object;

		JLocalObject? result = value.Signature[0] switch
		{
			CommonNames.BooleanSignatureChar => JBooleanObject.Create(env, value.BooleanValue),
			CommonNames.ByteSignatureChar => JByteObject.Create(env, value.ByteValue),
			CommonNames.CharSignatureChar => JCharacterObject.Create(env, value.CharValue),
			CommonNames.DoubleSignatureChar => JDoubleObject.Create(env, value.DoubleValue),
			CommonNames.FloatSignatureChar => JFloatObject.Create(env, value.FloatValue),
			CommonNames.IntSignatureChar => JIntegerObject.Create(env, value.IntValue),
			CommonNames.LongSignatureChar => JLongObject.Create(env, value.LongValue),
			CommonNames.ShortSignatureChar => JShortObject.Create(env, value.ShortValue),
			_ => default,
		};
		newObject = result is not null;
		return result;
	}
	/// <summary>
	/// Retrieves the field type signature from <paramref name="definition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <returns>Field type definition.</returns>
	private static ReadOnlySpan<Byte> GetFieldType(JFieldDefinition definition)
	{
		ReadOnlySpan<Byte> descriptorSpan = definition.Descriptor.AsSpan();
		Int32 offset = descriptorSpan.IndexOf(CommonNames.MethodParameterSuffixChar) + 1;
		return definition.Descriptor.AsSpan()[offset..];
	}
}