namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <summary>
	/// Internal <see cref="JPrimitiveObject"/> implementation.
	/// </summary>
	/// <typeparam name="TValue">A <see langword="unmanaged"/> type.</typeparam>
	public class Generic<TValue> : JPrimitiveObject, IWrapper<TValue>, IPrimitiveType
		where TValue : unmanaged, IEquatable<TValue>, IComparable, IConvertible
	{
		/// <summary>
		/// Internal value.
		/// </summary>
		private readonly TValue _value;

		/// <inheritdoc cref="IObject.ObjectClassName"/>
		public override CString ObjectClassName
		{
			get
			{
				{
					Type typeofT = typeof(TValue);
					if (typeofT == typeof(Byte))
						return UnicodeClassNames.JBooleanObjectClassName;
					if (typeofT == typeof(SByte))
						return UnicodeClassNames.JByteObjectClassName;
					if (typeofT == typeof(Char))
						return UnicodeClassNames.JCharacterObjectClassName;
					if (typeofT == typeof(Double))
						return UnicodeClassNames.JDoubleObjectClassName;
					if (typeofT == typeof(Single))
						return UnicodeClassNames.JFloatObjectClassName;
					if (typeofT == typeof(Int32))
						return UnicodeClassNames.JIntegerObjectClassName;
					return typeofT == typeof(Int64) ?
						UnicodeClassNames.JLongObjectClassName :
						UnicodeClassNames.JShortObjectClassName;
				}
			}
		}
		/// <inheritdoc cref="IObject.ObjectSignature"/>
		public override CString ObjectSignature
		{
			get
			{
				{
					Type typeofT = typeof(TValue);
					if (typeofT == typeof(Byte))
						return UnicodePrimitiveSignatures.JBooleanSignature;
					if (typeofT == typeof(SByte))
						return UnicodePrimitiveSignatures.JByteSignature;
					if (typeofT == typeof(Char))
						return UnicodePrimitiveSignatures.JCharSignature;
					if (typeofT == typeof(Double))
						return UnicodePrimitiveSignatures.JDoubleSignature;
					if (typeofT == typeof(Single))
						return UnicodePrimitiveSignatures.JFloatSignature;
					if (typeofT == typeof(Int32))
						return UnicodePrimitiveSignatures.JIntSignature;
					return typeofT == typeof(Int64) ?
						UnicodePrimitiveSignatures.JLongSignature :
						UnicodePrimitiveSignatures.JShortSignature;
				}
			}
		}
		/// <summary>
		/// Size of current type in bytes.
		/// </summary>
		public override Int32 SizeOf => NativeUtilities.SizeOf<TValue>();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="value">Internal wrapper.</param>
		public Generic(TValue value) => this._value = value;
		/// <summary>
		/// Internal primitive value.
		/// </summary>
		public TValue Value => this._value;

		/// <inheritdoc cref="IEquatable{TValue}.Equals(TValue)"/>
		public Boolean Equals(TValue other) => this._value.Equals(other);
		/// <inheritdoc/>
		public override Boolean Equals(JObject? other)
			=> other is Generic<TValue> jPrimitive && this._value.Equals(jPrimitive._value);
		/// <inheritdoc/>
		public override Boolean Equals(Object? obj) => obj is Generic<TValue> jPrimitive && this.Equals(jPrimitive);

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override Int32 GetHashCode() => this._value.GetHashCode();
		/// <inheritdoc/>
		public override String? ToString() => this._value.ToString();
		/// <inheritdoc/>
		public override Byte ToByte() => NativeUtilities.AsBytes(in this._value)[0];
		/// <inheritdoc/>
		public void CopyTo(Span<Byte> span)
		{
			Int32 offset = 0;
			this.CopyTo(span, ref offset);
		}

		Int32 IComparable.CompareTo(Object? obj) => this.Value.CompareTo(obj);
		TypeCode IConvertible.GetTypeCode() => this.Value.GetTypeCode();
		Boolean IConvertible.ToBoolean(IFormatProvider? provider) => this.Value.ToBoolean(provider);
		Byte IConvertible.ToByte(IFormatProvider? provider) => this.Value.ToByte(provider);
		Char IConvertible.ToChar(IFormatProvider? provider) => this.Value.ToChar(provider);
		DateTime IConvertible.ToDateTime(IFormatProvider? provider) => this.Value.ToDateTime(provider);
		Decimal IConvertible.ToDecimal(IFormatProvider? provider) => this.Value.ToDecimal(provider);
		Double IConvertible.ToDouble(IFormatProvider? provider) => this.Value.ToDouble(provider);
		Int16 IConvertible.ToInt16(IFormatProvider? provider) => this.Value.ToInt16(provider);
		Int32 IConvertible.ToInt32(IFormatProvider? provider) => this.Value.ToInt32(provider);
		Int64 IConvertible.ToInt64(IFormatProvider? provider) => this.Value.ToInt64(provider);
		SByte IConvertible.ToSByte(IFormatProvider? provider) => this.Value.ToSByte(provider);
		Single IConvertible.ToSingle(IFormatProvider? provider) => this.Value.ToSingle(provider);
		String IConvertible.ToString(IFormatProvider? provider) => this.Value.ToString(provider);
		Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
			=> this.Value.ToType(conversionType, provider);
		UInt16 IConvertible.ToUInt16(IFormatProvider? provider) => this.Value.ToUInt16(provider);
		UInt64 IConvertible.ToUInt64(IFormatProvider? provider) => this.Value.ToUInt64(provider);
		UInt32 IConvertible.ToUInt32(IFormatProvider? provider) => this.Value.ToUInt32(provider);

		/// <inheritdoc cref="IObject.CopyTo(Span{JValue}, Int32)"/>
		internal override void CopyTo(Span<JValue> span, Int32 index)
			=> NativeUtilities.AsBytes(this._value).CopyTo(span[index].AsBytes());
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override void CopyTo(Span<Byte> span, ref Int32 offset)
		{
			NativeUtilities.AsBytes(this._value).CopyTo(span[offset..]);
			offset += this.SizeOf;
		}
	}
}