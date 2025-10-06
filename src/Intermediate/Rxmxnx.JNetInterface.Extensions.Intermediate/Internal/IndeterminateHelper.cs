namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper class for indeterminate members.
/// </summary>
internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Retrieves a <see cref="JBoolean"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JBoolean"/> value.</returns>
	public static JBoolean GetBooleanValue(JLocalObject? jLocal)
	{
		if (jLocal is null) return false;

		IEnvironment env = jLocal.Environment;
		IClassFeature classFeature = env.ClassFeature;
		switch (jLocal)
		{
			case IWrapper.IBase<JBoolean> z: return z.Value;
			case IWrapper.IBase<JByte> b: return b.Value != default;
			case IWrapper.IBase<JChar> c: return c.Value != default;
			case IWrapper.IBase<JDouble> d: return d.Value != default;
			case IWrapper.IBase<JFloat> f: return f.Value != default;
			case IWrapper.IBase<JInt> i: return i.Value != default;
			case IWrapper.IBase<JLong> j: return j.Value != default;
			case IWrapper.IBase<JShort> s: return s.Value != default;
			case JNumberObject n: return n.GetValue<JDouble>() != default;
			default:
				if (classFeature.IsInstanceOf(jLocal, classFeature.BooleanObject))
					return NativeFunctionSetImpl.BooleanValueDefinition.Invoke(jLocal, classFeature.BooleanObject);
				if (classFeature.IsInstanceOf(jLocal, classFeature.CharacterObject))
					return NativeFunctionSetImpl.CharValueDefinition.Invoke(jLocal, classFeature.CharacterObject) !=
						default;
				if (classFeature.IsInstanceOf(jLocal, classFeature.NumberObject))
					return NativeFunctionSetImpl.DoubleValueDefinition.Invoke(jLocal, classFeature.NumberObject) !=
						default;
				return true;
		}
	}
	/// <summary>
	/// Retrieves a <see cref="JChar"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JChar"/> value.</returns>
	public static JChar? GetCharValue(JLocalObject? jLocal)
	{
		if (jLocal is null) return default;

		IEnvironment env = jLocal.Environment;
		IClassFeature classFeature = env.ClassFeature;
		switch (jLocal)
		{
			case IWrapper.IBase<JBoolean> z: return z.Value.Value ? JChar.One : JChar.Zero;
			case IWrapper.IBase<JByte> b: return (JChar)b.Value;
			case IWrapper.IBase<JChar> c: return c.Value;
			case IWrapper.IBase<JDouble> d: return (JChar)d.Value;
			case IWrapper.IBase<JFloat> f: return (JChar)f.Value;
			case IWrapper.IBase<JInt> i: return (JChar)i.Value;
			case IWrapper.IBase<JLong> j: return (JChar)j.Value;
			case IWrapper.IBase<JShort> s: return (JChar)s.Value;
			case JNumberObject n: return (JChar)n.GetValue<JShort>();

			default:
				if (classFeature.IsInstanceOf(jLocal, classFeature.CharacterObject))
					return NativeFunctionSetImpl.CharValueDefinition.Invoke(jLocal, classFeature.CharacterObject);
				if (classFeature.IsInstanceOf(jLocal, classFeature.NumberObject))
					return (JChar)NativeFunctionSetImpl.ShortValueDefinition.Invoke(jLocal, classFeature.NumberObject);
				if (classFeature.IsInstanceOf(jLocal, classFeature.BooleanObject))
					return (Char)NativeFunctionSetImpl.BooleanValueDefinition.Invoke(jLocal, classFeature.BooleanObject)
					                            .ByteValue;
				return default;
		}
	}
	/// <summary>
	/// Retrieves a <typeparamref name="TNumber"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <typeparam name="TNumber">Destination number type.</typeparam>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <typeparamref name="TNumber"/> value.</returns>
	public static TNumber? GetNumericValue<TNumber>(JLocalObject? jLocal)
		where TNumber : unmanaged, IPrimitiveType<TNumber>, INativeDataType<TNumber>, ISignedNumber<TNumber>,
		IBinaryNumber<TNumber>
	{
		if (jLocal is null) return default;

		IEnvironment env = jLocal.Environment;
		IClassFeature classFeature = env.ClassFeature;
		switch (jLocal)
		{
			case IWrapper.IBase<JBoolean> z: return z.Value.Value ? TNumber.One : TNumber.Zero;
			case IWrapper.IBase<JByte> b: return (TNumber)b.Value.Value;
			case IWrapper.IBase<JChar> c: return (TNumber)c.Value.Value;
			case IWrapper.IBase<JDouble> d: return (TNumber)d.Value.Value;
			case IWrapper.IBase<JFloat> f: return (TNumber)f.Value.Value;
			case IWrapper.IBase<JInt> i: return (TNumber)i.Value.Value;
			case IWrapper.IBase<JLong> j: return (TNumber)j.Value.Value;
			case IWrapper.IBase<JShort> s: return (TNumber)s.Value.Value;
			case JNumberObject n: return n.GetValue<TNumber>();
			default:
				if (classFeature.IsInstanceOf(jLocal, classFeature.NumberObject))
					return IndeterminateHelper.GetNumericValue<TNumber>(jLocal, classFeature.NumberObject);
				if (classFeature.IsInstanceOf(jLocal, classFeature.BooleanObject))
					return NativeFunctionSetImpl.BooleanValueDefinition.Invoke(jLocal, classFeature.BooleanObject)
					                            .Value ?
						TNumber.One :
						TNumber.Zero;
				if (classFeature.IsInstanceOf(jLocal, classFeature.CharacterObject))
					return (TNumber)NativeFunctionSetImpl.CharValueDefinition
					                                     .Invoke(jLocal, classFeature.CharacterObject).Value;
				return default;
		}
	}
	/// <summary>
	/// Retrieves a <see cref="JLocalObject"/> from <paramref name="value"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="value">A <see cref="IndeterminateResult"/> instance.</param>
	/// <param name="newObject">Output. Indicates whether resulting instance is created from primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JLocalObject? GetObjectValue(IEnvironment env, IndeterminateResult value, out Boolean newObject)
	{
		newObject = false;
		if (value.Object is not null)
			return value.Object;

		JLocalObject? result = value.Signature[0] switch
		{
			CommonNames.BooleanSignatureChar => JBooleanObject.Create(env, value.BooleanValue),
			CommonNames.ByteSignatureChar => JNumberObject<JByte, JByteObject>.Create(env, value.ByteValue),
			CommonNames.CharSignatureChar => JCharacterObject.Create(env, value.CharValue),
			CommonNames.DoubleSignatureChar => JNumberObject<JDouble, JDoubleObject>.Create(env, value.DoubleValue),
			CommonNames.FloatSignatureChar => JNumberObject<JFloat, JFloatObject>.Create(env, value.FloatValue),
			CommonNames.IntSignatureChar => JNumberObject<JInt, JIntegerObject>.Create(env, value.IntValue),
			CommonNames.LongSignatureChar => JNumberObject<JLong, JLongObject>.Create(env, value.LongValue),
			CommonNames.ShortSignatureChar => JNumberObject<JShort, JShortObject>.Create(env, value.ShortValue),
			_ => default,
		};
		newObject = result is not null;
		return result;
	}
}