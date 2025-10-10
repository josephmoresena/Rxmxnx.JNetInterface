namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Helper class for indeterminate members.
/// </summary>
internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Retrieves a <see cref="Boolean"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="objectMetadata">Reference. A <see cref="objectMetadata"/> instance.</param>
	/// <returns>A <see cref="Boolean"/> value.</returns>
	public static Boolean GetBooleanValue(JLocalObject? jLocal, ref PrimitiveWrapperObjectMetadata? objectMetadata)
	{
		if (jLocal is null) return false;
		if (objectMetadata is not null) return objectMetadata.GetValue<JBoolean>().GetValueOrDefault().Value;
		return jLocal switch
		{
			IWrapper.IBase<JBoolean> z => z.Value.Value,
			IWrapper.IBase<JByte> b => b.Value != default,
			IWrapper.IBase<JChar> c => c.Value != default,
			IWrapper.IBase<JDouble> d => d.Value != default,
			IWrapper.IBase<JFloat> f => f.Value != default,
			IWrapper.IBase<JInt> i => i.Value != default,
			IWrapper.IBase<JLong> j => j.Value != default,
			IWrapper.IBase<JShort> s => s.Value != default,
			JNumberObject n => n.GetValue<JDouble>() != default,
			_ => IndeterminateHelper.GetPrimitiveValue<JBoolean>(jLocal, out objectMetadata)?.Value ?? true,
		};
	}
	/// <summary>
	/// Retrieves a <see cref="JChar"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="objectMetadata">Reference. A <see cref="objectMetadata"/> instance.</param>
	/// <returns>A <see cref="JChar"/> value.</returns>
	public static JChar? GetCharValue(JLocalObject? jLocal, ref PrimitiveWrapperObjectMetadata? objectMetadata)
	{
		if (jLocal is null) return default;
		if (objectMetadata is not null) return objectMetadata.GetValue<JChar>().GetValueOrDefault();
		return jLocal switch
		{
			IWrapper.IBase<JBoolean> z => z.Value.Value ? JChar.One : JChar.Zero,
			IWrapper.IBase<JByte> b => (JChar)b.Value,
			IWrapper.IBase<JChar> c => c.Value,
			IWrapper.IBase<JDouble> d => (JChar)d.Value,
			IWrapper.IBase<JFloat> f => (JChar)f.Value,
			IWrapper.IBase<JInt> i => (JChar)i.Value,
			IWrapper.IBase<JLong> j => (JChar)j.Value,
			IWrapper.IBase<JShort> s => (JChar)s.Value,
			JNumberObject n => (JChar)n.GetValue<JShort>(),
			_ => IndeterminateHelper.GetPrimitiveValue<JChar>(jLocal, out objectMetadata).GetValueOrDefault(),
		};
	}
	/// <summary>
	/// Retrieves a <typeparamref name="TNumber"/> value from <paramref name="jLocal"/>.
	/// </summary>
	/// <typeparam name="TNumber">Destination number type.</typeparam>
	/// <param name="objectMetadata">Reference. A <see cref="objectMetadata"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <typeparamref name="TNumber"/> value.</returns>
	public static TNumber?
		GetNumericValue<TNumber>(JLocalObject? jLocal, ref PrimitiveWrapperObjectMetadata? objectMetadata)
		where TNumber : unmanaged, IPrimitiveType<TNumber>, INativeDataType<TNumber>, ISignedNumber<TNumber>,
		IBinaryNumber<TNumber>
	{
		if (jLocal is null) return default;
		if (objectMetadata is not null) return objectMetadata.GetValue<TNumber>().GetValueOrDefault();
		return jLocal switch
		{
			IWrapper.IBase<JBoolean> z => z.Value.Value ? TNumber.One : TNumber.Zero,
			IWrapper.IBase<JByte> b => b.Value.Value,
			IWrapper.IBase<JChar> c => c.Value.Value,
			IWrapper.IBase<JDouble> d => d.Value.Value,
			IWrapper.IBase<JFloat> f => f.Value.Value,
			IWrapper.IBase<JInt> i => i.Value.Value,
			IWrapper.IBase<JLong> j => j.Value.Value,
			IWrapper.IBase<JShort> s => s.Value.Value,
			JNumberObject n => n.GetValue<TNumber>(),
			_ => IndeterminateHelper.GetPrimitiveValue<TNumber>(jLocal, out objectMetadata).GetValueOrDefault(),
		};
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