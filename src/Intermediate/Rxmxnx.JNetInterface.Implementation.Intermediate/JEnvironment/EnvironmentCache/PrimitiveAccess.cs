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
			ref readonly FieldFunctionSet<JClassLocalRef> staticFieldFunctions =
				ref this.GetStaticFieldFunctions(signature, false);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetBooleanField);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetByteField);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetCharField);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetDoubleField);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetFloatField);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetIntField);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetLongField);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetShortField);
					break;
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Sets a primitive static field.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="setValue">Action to set value.</param>
		private unsafe void SetPrimitiveStaticField<TPrimitive>(JClassLocalRef classRef, ReadOnlySpan<Byte> bytes,
			Byte signature, JFieldId fieldId, ref readonly SetGenericFieldFunction<JClassLocalRef, TPrimitive> setValue)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive value = MemoryMarshal.AsRef<TPrimitive>(bytes);
			setValue.Set(this.Reference, classRef, fieldId, value);
			JTrace.SetPrimitiveField(default, classRef, signature, fieldId, value);
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
			ref readonly FieldFunctionSet<JObjectLocalRef> instanceFieldFunctions =
				ref this.GetInstanceFieldFunctions(signature, false);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetBooleanField);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetByteField);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetCharField);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetDoubleField);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetFloatField);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetIntField);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetLongField);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetShortField);
					break;
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Sets a primitive field.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="setValue">Action to set value.</param>
		private unsafe void SetPrimitiveField<TPrimitive>(JObjectLocalRef localRef, ReadOnlySpan<Byte> bytes,
			Byte signature, JFieldId fieldId,
			ref readonly SetGenericFieldFunction<JObjectLocalRef, TPrimitive> setValue)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive value = MemoryMarshal.AsRef<TPrimitive>(bytes);
			setValue.Set(this.Reference, localRef, fieldId, value);
			JTrace.SetPrimitiveField(localRef, default, signature, fieldId, value);
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
			ref readonly FieldFunctionSet<JClassLocalRef> staticFieldFunctions =
				ref this.GetStaticFieldFunctions(signature, true);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetBooleanField);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetByteField);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetCharField);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetDoubleField);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetFloatField);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetIntField);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetLongField);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetShortField);
					break;
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a primitive static field.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="getValue">Function to get value.</param>
		private unsafe void GetPrimitiveStaticField<TPrimitive>(Span<Byte> bytes, JClassLocalRef classRef,
			Byte signature, JFieldId fieldId, ref readonly GetGenericFieldFunction<JClassLocalRef, TPrimitive> getValue)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = getValue.Get(this.Reference, classRef, fieldId);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.GetPrimitiveField(default, classRef, signature, fieldId, result);
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
			ref readonly FieldFunctionSet<JObjectLocalRef> instanceFieldFunctions =
				ref this.GetInstanceFieldFunctions(signature, true);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetBooleanField);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetByteField);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetCharField);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetDoubleField);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetFloatField);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetIntField);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetLongField);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetShortField);
					break;
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Retrieves a primitive field.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="bytes">Binary span to hold result.</param>
		/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive char signature.</param>
		/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
		/// <param name="getValue">Function to get value.</param>
		private unsafe void GetPrimitiveField<TPrimitive>(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature,
			JFieldId fieldId, ref readonly GetGenericFieldFunction<JObjectLocalRef, TPrimitive> getValue)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = getValue.Get(this.Reference, localRef, fieldId);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.GetPrimitiveField(localRef, default, signature, fieldId, result);
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
			Byte signature = definition.Descriptor[^1];
			using IFixedContext<Byte>.IDisposable argsMemory = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			this.CopyAsJValue(jniTransaction, args, argsMemory.Values);

			ref readonly MethodFunctionSet<JClassLocalRef> staticMethodFunctions =
				ref this.GetStaticMethodFunctions(signature);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallBooleanMethod);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallByteMethod);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallCharMethod);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallDoubleMethod);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallFloatMethod);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallIntMethod);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallLongMethod);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveStaticFunction(bytes, classRef, signature, methodId, argsMemory.Pointer,
					                                 staticMethodFunctions.CallShortMethod);
					break;
			}
			this.CheckJniError();
		}
		/// <summary>
		/// Invokes a primitive static function.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="bytes">Destination span.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallPrimitiveStaticFunction<TPrimitive>(Span<Byte> bytes, JClassLocalRef classRef,
			Byte signature, JMethodId methodId, IntPtr argsPointer,
			in CallGenericFunction<JClassLocalRef, TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, classRef, methodId,
			                                      (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.CallPrimitiveFunction(default, classRef, signature, methodId, result);
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
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(signature, true);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualBooleanMethod);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualByteMethod);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualCharMethod);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualDoubleMethod);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualFloatMethod);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualIntMethod);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualLongMethod);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId,
					                                     argsMemory.Pointer,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualShortMethod);
					break;
			}
		}
		/// <summary>
		/// Invokes a primitive non-virtual function.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallPrimitiveNonVirtualFunction<TPrimitive>(Span<Byte> bytes, JObjectLocalRef localRef,
			JClassLocalRef classRef, Byte signature, JMethodId methodId, IntPtr argsPointer,
			in CallNonVirtualGenericFunction<TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, localRef, classRef, methodId,
			                                      (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.CallPrimitiveFunction(localRef, classRef, signature, methodId, result);
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
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(signature, false);
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallBooleanMethod);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallByteMethod);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallCharMethod);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallDoubleMethod);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallFloatMethod);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallIntMethod);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallLongMethod);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, argsMemory.Pointer,
					                           in instanceMethodFunctions.MethodFunctions.CallShortMethod);
					break;
			}
		}
		/// <summary>
		/// Invokes a primitive function.
		/// </summary>
		/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
		/// <param name="bytes">Destination span.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="argsPointer">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallPrimitiveFunction<TPrimitive>(Span<Byte> bytes, JObjectLocalRef localRef,
			Byte signature, JMethodId methodId, IntPtr argsPointer,
			in CallGenericFunction<JObjectLocalRef, TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType<TPrimitive>, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, localRef, methodId,
			                                      (ReadOnlyValPtr<JValue>)argsPointer);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.CallPrimitiveFunction(localRef, default, signature, methodId, result);
		}
	}
}