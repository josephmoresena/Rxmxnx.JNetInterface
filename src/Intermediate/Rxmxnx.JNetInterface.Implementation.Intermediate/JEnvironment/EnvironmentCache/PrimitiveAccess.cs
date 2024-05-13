namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Sets a primitive static field.
		/// </summary>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetPrimitiveStaticField(JClassLocalRef classRef, ReadOnlySpan<Byte> bytes, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.SetPrimitiveStaticField<Byte, SetStaticBooleanFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.SetPrimitiveStaticField<SByte, SetStaticByteFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.SetPrimitiveStaticField<Char, SetStaticCharFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.SetPrimitiveStaticField<Double, SetStaticDoubleFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.SetPrimitiveStaticField<Single, SetStaticFloatFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.SetPrimitiveStaticField<Int32, SetStaticIntFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.SetPrimitiveStaticField<Int64, SetStaticLongFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.SetPrimitiveStaticField<Int16, SetStaticShortFieldDelegate>(
						classRef, bytes, signature, fieldId, (d, e, c, f, v) => d(e, c, f, v));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Sets a primitive static field.
		/// </summary>
		/// <typeparam name="TValue">A <see langword="unmanaged"/> type.</typeparam>
		/// <typeparam name="TDelegate">A <see cref="Delegate"/> to set static field.</typeparam>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="setValue">Action to set value.</param>
		/// <param name="formatValue">Trace format value function.</param>
		private void SetPrimitiveStaticField<TValue, TDelegate>(JClassLocalRef classRef, ReadOnlySpan<Byte> bytes,
			Byte signature, JFieldId fieldId,
			Action<TDelegate, JEnvironmentRef, JClassLocalRef, JFieldId, TValue> setValue,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TValue value = MemoryMarshal.AsRef<TValue>(bytes);
			TDelegate setStaticField = this.GetDelegate<TDelegate>();
			setValue(setStaticField, this.Reference, classRef, fieldId, value);
			JTrace.SetPrimitiveField(default, classRef, signature, fieldId, value, formatValue);
		}
		/// <summary>
		/// Sets a primitive field.
		/// </summary>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void SetPrimitiveField(JObjectLocalRef localRef, ReadOnlySpan<Byte> bytes, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.SetPrimitiveField<Byte, SetBooleanFieldDelegate>(
						localRef, bytes, signature, fieldId, (d, e, o, f, v) => d(e, o, f, v),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.SetPrimitiveField<SByte, SetByteFieldDelegate>(localRef, bytes, signature, fieldId,
					                                                    (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.SetPrimitiveField<Char, SetCharFieldDelegate>(localRef, bytes, signature, fieldId,
					                                                   (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.SetPrimitiveField<Double, SetDoubleFieldDelegate>(
						localRef, bytes, signature, fieldId, (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.SetPrimitiveField<Single, SetFloatFieldDelegate>(
						localRef, bytes, signature, fieldId, (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.SetPrimitiveField<Int32, SetIntFieldDelegate>(localRef, bytes, signature, fieldId,
					                                                   (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.SetPrimitiveField<Int64, SetLongFieldDelegate>(localRef, bytes, signature, fieldId,
					                                                    (d, e, o, f, v) => d(e, o, f, v));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.SetPrimitiveField<Int16, SetShortFieldDelegate>(
						localRef, bytes, signature, fieldId, (d, e, o, f, v) => d(e, o, f, v));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Sets a primitive field.
		/// </summary>
		/// <typeparam name="TValue">A <see langword="unmanaged"/> type.</typeparam>
		/// <typeparam name="TDelegate">A <see cref="Delegate"/> to set field.</typeparam>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="setValue">Action to set value.</param>
		/// <param name="formatValue">Function to format value.</param>
		private void SetPrimitiveField<TValue, TDelegate>(JObjectLocalRef localRef, ReadOnlySpan<Byte> bytes,
			Byte signature, JFieldId fieldId,
			Action<TDelegate, JEnvironmentRef, JObjectLocalRef, JFieldId, TValue> setValue,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TValue value = MemoryMarshal.AsRef<TValue>(bytes);
			TDelegate setField = this.GetDelegate<TDelegate>();
			setValue(setField, this.Reference, localRef, fieldId, value);
			JTrace.SetPrimitiveField(localRef, default, signature, fieldId, value, formatValue);
		}
		/// <summary>
		/// Retrieves a primitive static field.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void GetPrimitiveStaticField(Span<Byte> bytes, JClassLocalRef classRef, Byte signature,
			JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.GetPrimitiveStaticField<Byte, GetStaticBooleanFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.GetPrimitiveStaticField<SByte, GetStaticByteFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.GetPrimitiveStaticField<Char, GetStaticCharFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.GetPrimitiveStaticField<Double, GetStaticDoubleFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.GetPrimitiveStaticField<Single, GetStaticFloatFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.GetPrimitiveStaticField<Int32, GetStaticIntFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.GetPrimitiveStaticField<Int64, GetStaticLongFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.GetPrimitiveStaticField<Int16, GetStaticShortFieldDelegate>(
						bytes, classRef, signature, fieldId, (d, e, c, f) => d(e, c, f));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a primitive static field.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="getValue">Function to get value.</param>
		/// <param name="formatValue">Function to format value.</param>
		private void GetPrimitiveStaticField<TValue, TDelegate>(Span<Byte> bytes, JClassLocalRef classRef,
			Byte signature, JFieldId fieldId,
			Func<TDelegate, JEnvironmentRef, JClassLocalRef, JFieldId, TValue> getValue,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TDelegate getStaticField = this.GetDelegate<TDelegate>();
			TValue result = getValue(getStaticField, this.Reference, classRef, fieldId);
			MemoryMarshal.AsRef<TValue>(bytes) = result;
			JTrace.GetPrimitiveField(default, classRef, signature, fieldId, result, formatValue);
		}
		/// <summary>
		/// Retrieves a primitive field.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		private void GetPrimitiveField(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature, JFieldId fieldId)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.GetPrimitiveField<Byte, GetBooleanFieldDelegate>(
						bytes, localRef, signature, fieldId, (d, e, o, f) => d(e, o, f),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.GetPrimitiveField<SByte, GetByteFieldDelegate>(bytes, localRef, signature, fieldId,
					                                                    (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.GetPrimitiveField<Char, GetCharFieldDelegate>(bytes, localRef, signature, fieldId,
					                                                   (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.GetPrimitiveField<Double, GetDoubleFieldDelegate>(
						bytes, localRef, signature, fieldId, (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.GetPrimitiveField<Single, GetFloatFieldDelegate>(
						bytes, localRef, signature, fieldId, (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.GetPrimitiveField<Int32, GetIntFieldDelegate>(bytes, localRef, signature, fieldId,
					                                                   (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.GetPrimitiveField<Int64, GetLongFieldDelegate>(bytes, localRef, signature, fieldId,
					                                                    (d, e, o, f) => d(e, o, f));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.GetPrimitiveField<Int16, GetShortFieldDelegate>(
						bytes, localRef, signature, fieldId, (d, e, o, f) => d(e, o, f));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a primitive field.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="getValue">Function to get value.</param>
		/// <param name="formatValue">Function to format value.</param>
		private void GetPrimitiveField<TValue, TDelegate>(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature,
			JFieldId fieldId, Func<TDelegate, JEnvironmentRef, JObjectLocalRef, JFieldId, TValue> getValue,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TDelegate getStaticField = this.GetDelegate<TDelegate>();
			TValue result = getValue(getStaticField, this.Reference, localRef, fieldId);
			MemoryMarshal.AsRef<TValue>(bytes) = result;
			JTrace.GetPrimitiveField(localRef, default, signature, fieldId, result, formatValue);
		}
		/// <summary>
		/// Invokes a primitive static function.
		/// </summary>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
		/// <param name="args">The <see cref="IObject"/> array with call arguments.</param>
		/// <param name="jniTransaction"><see cref="INativeTransaction"/> instance.</param>
		/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
		private void CallPrimitiveStaticFunction(Span<Byte> bytes, JFunctionDefinition definition,
			JClassLocalRef classRef, IObject?[] args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			Boolean useStackAlloc = this.UseStackAlloc(definition, out Int32 requiredBytes);
			Byte signature = definition.Information[1][^1];
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveStaticFunction<Byte, CallStaticBooleanMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveStaticFunction<SByte, CallStaticByteMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveStaticFunction<Char, CallStaticCharMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveStaticFunction<Double, CallStaticDoubleMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveStaticFunction<Single, CallStaticFloatMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveStaticFunction<Int32, CallStaticIntMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveStaticFunction<Int64, CallStaticLongMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveStaticFunction<Int16, CallStaticShortMethodADelegate>(
						bytes, classRef, signature, methodId, argsMemory.Pointer, (d, e, c, m, a) => d(e, c, m, a));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Invokes a primitive static function.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		/// <param name="formatValue">Function to format value.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private void CallPrimitiveStaticFunction<TValue, TDelegate>(Span<Byte> bytes, JClassLocalRef classRef,
			Byte signature, JMethodId methodId, IntPtr argsPointer,
			Func<TDelegate, JEnvironmentRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>, TValue> callFunction,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TDelegate callStaticFunction = this.GetDelegate<TDelegate>();
			TValue result = callFunction(callStaticFunction, this.Reference, classRef, methodId,
			                             (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TValue>(bytes) = result;
			JTrace.CallPrimitiveFunction(default, classRef, signature, methodId, result, formatValue);
		}
		/// <summary>
		/// Invokes a primitive non-virtual function.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallPrimitiveNonVirtualFunction(Span<Byte> bytes, JObjectLocalRef localRef,
			JClassLocalRef classRef, Byte signature, JMethodId methodId, IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Byte, CallNonVirtualBooleanMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a), v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveNonVirtualFunction<SByte, CallNonVirtualByteMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Char, CallNonVirtualCharMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Double, CallNonVirtualDoubleMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Single, CallNonVirtualFloatMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Int32, CallNonVirtualIntMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Int64, CallNonVirtualLongMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveNonVirtualFunction<Int16, CallNonVirtualShortMethodADelegate>(
						bytes, localRef, classRef, signature, methodId, argsMemory.Pointer,
						(d, e, o, c, m, a) => d(e, o, c, m, a));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
		}
		/// <summary>
		/// Invokes a primitive non-virtual function.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		/// <param name="formatValue">Function to format value.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private void CallPrimitiveNonVirtualFunction<TValue, TDelegate>(Span<Byte> bytes, JObjectLocalRef localRef,
			JClassLocalRef classRef, Byte signature, JMethodId methodId, IntPtr argsPointer,
			Func<TDelegate, JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>, TValue>
				callFunction, Func<TValue, String>? formatValue = default)
			where TValue : unmanaged where TDelegate : Delegate
		{
			TDelegate callNonVirtualFunction = this.GetDelegate<TDelegate>();
			TValue result = callFunction(callNonVirtualFunction, this.Reference, localRef, classRef, methodId,
			                             (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TValue>(bytes) = result;
			JTrace.CallPrimitiveFunction(localRef, classRef, signature, methodId, result, formatValue);
		}
		/// <summary>
		/// Invokes a primitive function.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="argsMemory">Fixed memory with parameters.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private void CallPrimitiveFunction(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature,
			JMethodId methodId, IFixedPointer argsMemory)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveFunction<Byte, CallBooleanMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a),
						v => $"{v == JBoolean.TrueValue}");
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveFunction<SByte, CallByteMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveFunction<Char, CallCharMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveFunction<Double, CallDoubleMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveFunction<Single, CallFloatMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveFunction<Int32, CallIntMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveFunction<Int64, CallLongMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveFunction<Int16, CallShortMethodADelegate>(
						bytes, localRef, signature, methodId, argsMemory.Pointer, (d, e, o, m, a) => d(e, o, m, a));
					break;
				default:
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			}
		}
		/// <summary>
		/// Invokes a primitive function.
		/// </summary>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		/// <param name="formatValue">Function to format value.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private void CallPrimitiveFunction<TValue, TDelegate>(Span<Byte> bytes, JObjectLocalRef localRef,
			Byte signature, JMethodId methodId, IntPtr argsPointer,
			Func<TDelegate, JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, TValue> callFunction,
			Func<TValue, String>? formatValue = default) where TValue : unmanaged where TDelegate : Delegate
		{
			TDelegate callNonVirtualFunction = this.GetDelegate<TDelegate>();
			TValue result = callFunction(callNonVirtualFunction, this.Reference, localRef, methodId,
			                             (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TValue>(bytes) = result;
			JTrace.CallPrimitiveFunction(localRef, default, signature, methodId, result, formatValue);
		}
	}
}