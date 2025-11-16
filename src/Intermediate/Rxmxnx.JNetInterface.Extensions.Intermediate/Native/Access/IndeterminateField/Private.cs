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
		JLocalObject? jObject = IndeterminateHelper.GetObjectValue(env, fieldValue, out Boolean newObject);
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
				result = IndeterminateField.GetFieldValue<JBooleanObject>(env, jObject, valueSignature);
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
}