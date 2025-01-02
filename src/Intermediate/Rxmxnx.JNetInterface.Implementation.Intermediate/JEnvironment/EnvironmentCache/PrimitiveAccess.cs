namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
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
				case CommonNames.BooleanSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetBooleanField);
					break;
				case CommonNames.ByteSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetByteField);
					break;
				case CommonNames.CharSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetCharField);
					break;
				case CommonNames.DoubleSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetDoubleField);
					break;
				case CommonNames.FloatSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetFloatField);
					break;
				case CommonNames.IntSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetIntField);
					break;
				case CommonNames.LongSignatureChar:
					this.SetPrimitiveStaticField(classRef, bytes, signature, fieldId,
					                             in staticFieldFunctions.SetLongField);
					break;
				case CommonNames.ShortSignatureChar:
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
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
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
				case CommonNames.BooleanSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetBooleanField);
					break;
				case CommonNames.ByteSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetByteField);
					break;
				case CommonNames.CharSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetCharField);
					break;
				case CommonNames.DoubleSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetDoubleField);
					break;
				case CommonNames.FloatSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId,
					                       in instanceFieldFunctions.SetFloatField);
					break;
				case CommonNames.IntSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetIntField);
					break;
				case CommonNames.LongSignatureChar:
					this.SetPrimitiveField(localRef, bytes, signature, fieldId, in instanceFieldFunctions.SetLongField);
					break;
				case CommonNames.ShortSignatureChar:
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
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
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
				case CommonNames.BooleanSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetBooleanField);
					break;
				case CommonNames.ByteSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetByteField);
					break;
				case CommonNames.CharSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetCharField);
					break;
				case CommonNames.DoubleSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetDoubleField);
					break;
				case CommonNames.FloatSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetFloatField);
					break;
				case CommonNames.IntSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetIntField);
					break;
				case CommonNames.LongSignatureChar:
					this.GetPrimitiveStaticField(bytes, classRef, signature, fieldId,
					                             in staticFieldFunctions.GetLongField);
					break;
				case CommonNames.ShortSignatureChar:
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
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
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
				case CommonNames.BooleanSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetBooleanField);
					break;
				case CommonNames.ByteSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetByteField);
					break;
				case CommonNames.CharSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetCharField);
					break;
				case CommonNames.DoubleSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetDoubleField);
					break;
				case CommonNames.FloatSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId,
					                       in instanceFieldFunctions.GetFloatField);
					break;
				case CommonNames.IntSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetIntField);
					break;
				case CommonNames.LongSignatureChar:
					this.GetPrimitiveField(bytes, localRef, signature, fieldId, in instanceFieldFunctions.GetLongField);
					break;
				case CommonNames.ShortSignatureChar:
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
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
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
		private unsafe void CallStaticPrimitiveFunction(Span<Byte> bytes, JFunctionDefinition definition,
			JClassLocalRef classRef, ReadOnlySpan<IObject?> args, INativeTransaction jniTransaction, JMethodId methodId)
		{
			Byte signature = definition.Descriptor[^1];
			ref readonly MethodFunctionSet<JClassLocalRef> staticMethodFunctions =
				ref this.GetStaticMethodFunctions(signature);
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(definition, out Int32 requiredBytes), requiredBytes);
			Span<JValue> buffer = this.CopyAsJValue(jniTransaction, args,
			                                        stackDisposable.UsingStack ?
				                                        stackalloc Byte[requiredBytes] :
				                                        EnvironmentCache.HeapAlloc<Byte>(requiredBytes));
			fixed (JValue* ptr = &MemoryMarshal.GetReference(buffer))
			{
				switch (signature)
				{
					case CommonNames.BooleanSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallBooleanMethod);
						break;
					case CommonNames.ByteSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallByteMethod);
						break;
					case CommonNames.CharSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallCharMethod);
						break;
					case CommonNames.DoubleSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallDoubleMethod);
						break;
					case CommonNames.FloatSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallFloatMethod);
						break;
					case CommonNames.IntSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallIntMethod);
						break;
					case CommonNames.LongSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallLongMethod);
						break;
					case CommonNames.ShortSignatureChar:
						this.CallStaticPrimitiveFunction(bytes, classRef, signature, methodId, ptr,
						                                 staticMethodFunctions.CallShortMethod);
						break;
				}
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
		/// <param name="ptr">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallStaticPrimitiveFunction<TPrimitive>(Span<Byte> bytes, JClassLocalRef classRef,
			Byte signature, JMethodId methodId, JValue* ptr,
			in CallGenericFunction<JClassLocalRef, TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, classRef, methodId, ptr);
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
		/// <param name="ptr">Pointer to call argments array.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private unsafe void CallPrimitiveNonVirtualFunction(Span<Byte> bytes, JObjectLocalRef localRef,
			JClassLocalRef classRef, Byte signature, JMethodId methodId, JValue* ptr)
		{
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(signature, true);
			switch (signature)
			{
				case CommonNames.BooleanSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualBooleanMethod);
					break;
				case CommonNames.ByteSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualByteMethod);
					break;
				case CommonNames.CharSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualCharMethod);
					break;
				case CommonNames.DoubleSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualDoubleMethod);
					break;
				case CommonNames.FloatSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualFloatMethod);
					break;
				case CommonNames.IntSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualIntMethod);
					break;
				case CommonNames.LongSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
					                                     in instanceMethodFunctions.NonVirtualFunctions
						                                     .CallNonVirtualLongMethod);
					break;
				case CommonNames.ShortSignatureChar:
					this.CallPrimitiveNonVirtualFunction(bytes, localRef, classRef, signature, methodId, ptr,
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
		/// <param name="ptr">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallPrimitiveNonVirtualFunction<TPrimitive>(Span<Byte> bytes, JObjectLocalRef localRef,
			JClassLocalRef classRef, Byte signature, JMethodId methodId, JValue* ptr,
			in CallNonVirtualGenericFunction<TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, localRef, classRef, methodId, ptr);
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
		/// <param name="ptr">Pointer to call argments array.</param>
		/// <exception cref="ArgumentException">If signature is not for a primitive type.</exception>
		private unsafe void CallPrimitiveFunction(Span<Byte> bytes, JObjectLocalRef localRef, Byte signature,
			JMethodId methodId, JValue* ptr)
		{
			ref readonly InstanceMethodFunctionSet instanceMethodFunctions =
				ref this.GetInstanceMethodFunctions(signature, false);
			switch (signature)
			{
				case CommonNames.BooleanSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallBooleanMethod);
					break;
				case CommonNames.ByteSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallByteMethod);
					break;
				case CommonNames.CharSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallCharMethod);
					break;
				case CommonNames.DoubleSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallDoubleMethod);
					break;
				case CommonNames.FloatSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallFloatMethod);
					break;
				case CommonNames.IntSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallIntMethod);
					break;
				case CommonNames.LongSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
					                           in instanceMethodFunctions.MethodFunctions.CallLongMethod);
					break;
				case CommonNames.ShortSignatureChar:
					this.CallPrimitiveFunction(bytes, localRef, signature, methodId, ptr,
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
		/// <param name="ptr">Pointer to call argments array.</param>
		/// <param name="methodId">A <see cref="JMethodId"/> identifier.</param>
		/// <param name="callFunction">Function to invoke function.</param>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
		                 Justification = CommonConstants.PrimitiveCallJustification)]
		private unsafe void CallPrimitiveFunction<TPrimitive>(Span<Byte> bytes, JObjectLocalRef localRef,
			Byte signature, JMethodId methodId, JValue* ptr,
			in CallGenericFunction<JObjectLocalRef, TPrimitive> callFunction)
			where TPrimitive : unmanaged, INativeType, IPrimitiveType<TPrimitive>
		{
			TPrimitive result = callFunction.Call(this.Reference, localRef, methodId, ptr);
			MemoryMarshal.AsRef<TPrimitive>(bytes) = result;
			JTrace.CallPrimitiveFunction(localRef, default, signature, methodId, result);
		}
		/// <summary>
		/// Retrieves a <see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.
		/// </summary>
		/// <param name="parameterTypes">A <see cref="JClassObject"/> list.</param>
		/// <returns><see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.</returns>
		private JArgumentMetadata[] GetCallMetadata(JArrayObject<JClassObject> parameterTypes)
		{
			JArgumentMetadata[] args = new JArgumentMetadata[parameterTypes.Length];
			this.GetCallMetadata(parameterTypes, args);
			return args;
		}
		/// <summary>
		/// Fills <paramref name="args"/> with <paramref name="parameterTypes"/> metadata types.
		/// </summary>
		/// <param name="parameterTypes">A <see cref="JClassObject"/> list.</param>
		/// <param name="args">A <see cref="JArgumentMetadata"/> span.</param>
		private void GetCallMetadata(JArrayObject<JClassObject> parameterTypes, Span<JArgumentMetadata> args)
		{
			JObjectArrayLocalRef objectArrayRef = parameterTypes.As<JObjectArrayLocalRef>();
			for (Int32 i = 0; i < parameterTypes.Length; i++)
			{
				JObjectLocalRef localRef = this.GetObjectArrayElement(objectArrayRef, i);
				JClassObject jClass = this.GetClass(JClassLocalRef.FromReference(in localRef), true);
				args[i] = MetadataHelper.GetArgumentMetadata(jClass);
			}
		}
	}
}