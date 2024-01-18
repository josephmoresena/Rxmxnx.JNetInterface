namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record JEnvironmentCache : IArrayFeature
	{
		public JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfInvalidArrayLength(length);
			JArrayLocalRef arrayRef;
			if (MetadataHelper.GetMetadata<TElement>() is JPrimitiveTypeMetadata metadata)
			{
				switch (metadata.Signature[0])
				{
					case UnicodePrimitiveSignatures.BooleanSignatureChar:
						NewBooleanArrayDelegate newBooleanArray = this.GetDelegate<NewBooleanArrayDelegate>();
						arrayRef = newBooleanArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.ByteSignatureChar:
						NewByteArrayDelegate newByteArray = this.GetDelegate<NewByteArrayDelegate>();
						arrayRef = newByteArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.CharSignatureChar:
						NewCharArrayDelegate newCharArray = this.GetDelegate<NewCharArrayDelegate>();
						arrayRef = newCharArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.DoubleSignatureChar:
						NewDoubleArrayDelegate newDoubleArray = this.GetDelegate<NewDoubleArrayDelegate>();
						arrayRef = newDoubleArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.FloatSignatureChar:
						NewFloatArrayDelegate newFloatArray = this.GetDelegate<NewFloatArrayDelegate>();
						arrayRef = newFloatArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.IntSignatureChar:
						NewIntArrayDelegate newIntArray = this.GetDelegate<NewIntArrayDelegate>();
						arrayRef = newIntArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.LongSignatureChar:
						NewLongArrayDelegate newLongArray = this.GetDelegate<NewLongArrayDelegate>();
						arrayRef = newLongArray(this.Reference, length).ArrayValue;
						break;
					case UnicodePrimitiveSignatures.ShortSignatureChar:
						NewShortArrayDelegate newShortArray = this.GetDelegate<NewShortArrayDelegate>();
						arrayRef = newShortArray(this.Reference, length).ArrayValue;
						break;
					default:
						throw new ArgumentException("Invalid primitive type.");
				}
				if (arrayRef.Value == default) this.CheckJniError();
			}
			else
			{
				JClassObject jClass = this.GetClass<TElement>();
				arrayRef = this.NewObjectArray(length, jClass);
			}
			IEnvironment env = this._mainClasses.Environment;
			return new(env, arrayRef, length);
		}
		public JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
			where TElement : IObject, IDataType<TElement>
		{
			JArrayObject<TElement> result;
			if (MetadataHelper.GetMetadata<TElement>() is JPrimitiveTypeMetadata metadata)
			{
				result = this.CreateArray<TElement>(length);
				this.FillPrimitiveArray(result, metadata, initialElement);
			}
			else
			{
				IEnvironment env = this._mainClasses.Environment;
				JClassObject jClass = this.GetClass<TElement>();
				JLocalObject? initial = initialElement as JLocalObject;
				JArrayLocalRef arrayRef = this.NewObjectArray(length, jClass, initial);
				result = new(env, arrayRef, length);
			}
			return result;
		}
		public Int32 GetArrayLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetArrayLengthDelegate getArrayLength = this.GetDelegate<GetArrayLengthDelegate>();
			Int32 result = getArrayLength(this.Reference, jObject.As<JArrayLocalRef>());
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				using IFixedContext<Byte>.IDisposable fixedBuffer =
					JEnvironmentCache.AllocToFixedContext(stackalloc Byte[primitiveMetadata.SizeOf]);
				this.GetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, fixedBuffer, index);
				this.CheckJniError();
				return (TElement)primitiveMetadata.CreateInstance(fixedBuffer.Values);
			}
			GetObjectArrayElementDelegate getObjectArrayElement = this.GetDelegate<GetObjectArrayElementDelegate>();
			IEnvironment env = this._mainClasses.Environment;
			JObjectLocalRef localRef = getObjectArrayElement(this.Reference, jArray.As<JObjectArrayLocalRef>(), index);
			if (localRef == default) this.CheckJniError();
			return this.Cast<TElement>(new(this.GetClass<TElement>(), localRef));
		}
		public void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Int32 offset = 0;
				Span<Byte> buffer = stackalloc Byte[primitiveMetadata.SizeOf];
				using IFixedContext<Byte>.IDisposable fixedBuffer =
					JEnvironmentCache.AllocToFixedContext(stackalloc Byte[primitiveMetadata.SizeOf]);
				value!.CopyTo(buffer, ref offset);
				this.SetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, fixedBuffer, index);
				this.CheckJniError();
			}
			else
			{
				this.SetObjectElement(jArray, index, value as JLocalObject);
			}
		}
		public void SetObjectElement(JArrayObject jArray, Int32 index, JLocalObject? value)
		{
			ValidationUtilities.ThrowIfDummy(value);
			SetObjectArrayElementDelegate setObjectArrayElement = this.GetDelegate<SetObjectArrayElementDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = jniTransaction.Add(value);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			setObjectArrayElement(this.Reference, arrayRef, index, localRef);
			this.CheckJniError();
		}
		public IntPtr GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, out Boolean isCopy)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IntPtr result = this.GetPrimitiveArrayElements(jArray, metadata.Signature, out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			GetPrimitiveArrayCriticalDelegate getPrimitiveArrayCriticalDelegate =
				this.GetDelegate<GetPrimitiveArrayCriticalDelegate>();
			IntPtr result = getPrimitiveArrayCriticalDelegate(this.Reference, jArray.Reference, out _);
			if (result == IntPtr.Zero) this.CheckJniError();
			return result;
		}
		public void ReleaseSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer, JReleaseMode mode)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			this.ReleasePrimitiveArrayElements(jArray, metadata.Signature, pointer, mode);
			this.CheckJniError();
		}
		public void ReleaseCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ReleasePrimitiveArrayCriticalDelegate releasePrimitiveArrayCritical =
				this.GetDelegate<ReleasePrimitiveArrayCriticalDelegate>();
			releasePrimitiveArrayCritical(this.Reference, jArray.Reference, pointer, JReleaseMode.Abort);
			this.CheckJniError();
		}
		public void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex, Memory<TPrimitive> elements)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			using IFixedContext<TPrimitive>.IDisposable fixedMemory = elements.GetFixedContext();
			this.GetPrimitiveArrayRegion(jArray, metadata.Signature, fixedMemory, startIndex, elements.Length);
			this.CheckJniError();
		}
		public void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			using IReadOnlyFixedContext<TPrimitive>.IDisposable fixedMemory = elements.GetFixedContext();
			this.SetPrimitiveArrayRegion(jArray, metadata.Signature, fixedMemory, startIndex, elements.Length);
			this.CheckJniError();
		}

		/// <summary>
		/// Retrieves a VM managed pointer to primitive array elements.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="isCopy">
		/// Output. If <c>JNI_TRUE</c> indicates output points to a copy of the elements.
		/// </param>
		/// <returns>A VM managed pointer to primitive array elements.</returns>
		/// <exception cref="ArgumentException"/>
		private IntPtr GetPrimitiveArrayElements(JArrayObject jArray, CString signature, out Byte isCopy)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			switch (signature[0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					GetBooleanArrayElementsDelegate getBooleanArrayElements =
						this.GetDelegate<GetBooleanArrayElementsDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					return getBooleanArrayElements(this.Reference, jBooleanArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetByteArrayElementsDelegate getByteArrayElements =
						this.GetDelegate<GetByteArrayElementsDelegate>();
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					return getByteArrayElements(this.Reference, jByteArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetCharArrayElementsDelegate getCharArrayElements =
						this.GetDelegate<GetCharArrayElementsDelegate>();
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					return getCharArrayElements(this.Reference, jCharArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetDoubleArrayElementsDelegate getDoubleArrayElements =
						this.GetDelegate<GetDoubleArrayElementsDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					return getDoubleArrayElements(this.Reference, jDoubleArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetFloatArrayElementsDelegate getFloatArrayElements =
						this.GetDelegate<GetFloatArrayElementsDelegate>();
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					return getFloatArrayElements(this.Reference, jFloatArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetIntArrayElementsDelegate getIntArrayElements = this.GetDelegate<GetIntArrayElementsDelegate>();
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					return getIntArrayElements(this.Reference, jIntArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetLongArrayElementsDelegate getLongArrayElements =
						this.GetDelegate<GetLongArrayElementsDelegate>();
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					return getLongArrayElements(this.Reference, jLongArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetShortArrayElementsDelegate getShortArrayElements =
						this.GetDelegate<GetShortArrayElementsDelegate>();
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					return getShortArrayElements(this.Reference, jShortArrayRef, out isCopy);
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Releases <paramref name="pointer"/> from VM.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="pointer">A VM managed pointer to primitive array elements.</param>
		/// <param name="mode">A <see cref="JReleaseMode"/> value.</param>
		/// <exception cref="ArgumentException"/>
		private void ReleasePrimitiveArrayElements(JArrayObject jArray, CString signature, IntPtr pointer,
			JReleaseMode mode)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			switch (signature[0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					ReleaseBooleanArrayElementsDelegate releaseBooleanArrayElements =
						this.GetDelegate<ReleaseBooleanArrayElementsDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					releaseBooleanArrayElements(this.Reference, jBooleanArrayRef, (ReadOnlyValPtr<Byte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					ReleaseByteArrayElementsDelegate releaseByteArrayElements =
						this.GetDelegate<ReleaseByteArrayElementsDelegate>();
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					releaseByteArrayElements(this.Reference, jByteArrayRef, (ReadOnlyValPtr<SByte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					ReleaseCharArrayElementsDelegate releaseCharArrayElements =
						this.GetDelegate<ReleaseCharArrayElementsDelegate>();
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					releaseCharArrayElements(this.Reference, jCharArrayRef, (ReadOnlyValPtr<Char>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					ReleaseDoubleArrayElementsDelegate releaseDoubleArrayElements =
						this.GetDelegate<ReleaseDoubleArrayElementsDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					releaseDoubleArrayElements(this.Reference, jDoubleArrayRef, (ReadOnlyValPtr<Double>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					ReleaseFloatArrayElementsDelegate releaseFloatArrayElements =
						this.GetDelegate<ReleaseFloatArrayElementsDelegate>();
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					releaseFloatArrayElements(this.Reference, jFloatArrayRef, (ReadOnlyValPtr<Single>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					ReleaseIntArrayElementsDelegate releaseIntArrayElements =
						this.GetDelegate<ReleaseIntArrayElementsDelegate>();
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					releaseIntArrayElements(this.Reference, jIntArrayRef, (ReadOnlyValPtr<Int32>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					ReleaseLongArrayElementsDelegate releaseLongArrayElements =
						this.GetDelegate<ReleaseLongArrayElementsDelegate>();
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					releaseLongArrayElements(this.Reference, jLongArrayRef, (ReadOnlyValPtr<Int64>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					ReleaseShortArrayElementsDelegate releaseShortArrayElements =
						this.GetDelegate<ReleaseShortArrayElementsDelegate>();
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					releaseShortArrayElements(this.Reference, jShortArrayRef, (ReadOnlyValPtr<Int16>)pointer, mode);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Copies a primitive array region in <paramref name="fixedBuffer"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="fixedBuffer">A <see cref="IFixedPointer"/> instance.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private void GetPrimitiveArrayRegion(JArrayObject jArray, CString signature, IFixedPointer fixedBuffer,
			Int32 index, Int32 count = 1)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			switch (signature[0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					GetBooleanArrayRegionDelegate getBooleanArrayRegion =
						this.GetDelegate<GetBooleanArrayRegionDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					getBooleanArrayRegion(this.Reference, jBooleanArrayRef, index, count,
					                      (ValPtr<Byte>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetByteArrayRegionDelegate getByteArrayRegion = this.GetDelegate<GetByteArrayRegionDelegate>();
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					getByteArrayRegion(this.Reference, jByteArrayRef, index, count, (ValPtr<SByte>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetCharArrayRegionDelegate getCharArrayRegion = this.GetDelegate<GetCharArrayRegionDelegate>();
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					getCharArrayRegion(this.Reference, jCharArrayRef, index, count, (ValPtr<Char>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetDoubleArrayRegionDelegate getDoubleArrayRegion =
						this.GetDelegate<GetDoubleArrayRegionDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					getDoubleArrayRegion(this.Reference, jDoubleArrayRef, index, count,
					                     (ValPtr<Double>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetFloatArrayRegionDelegate getFloatArrayRegion = this.GetDelegate<GetFloatArrayRegionDelegate>();
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					getFloatArrayRegion(this.Reference, jFloatArrayRef, index, count,
					                    (ValPtr<Single>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetIntArrayRegionDelegate getIntArrayRegion = this.GetDelegate<GetIntArrayRegionDelegate>();
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					getIntArrayRegion(this.Reference, jIntArrayRef, index, count, (ValPtr<Int32>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetLongArrayRegionDelegate getLongArrayRegion = this.GetDelegate<GetLongArrayRegionDelegate>();
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					getLongArrayRegion(this.Reference, jLongArrayRef, index, count, (ValPtr<Int64>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetShortArrayRegionDelegate getShortArrayRegion = this.GetDelegate<GetShortArrayRegionDelegate>();
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					getShortArrayRegion(this.Reference, jShortArrayRef, index, count,
					                    (ValPtr<Int16>)fixedBuffer.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Copies <paramref name="fixedBuffer"/> in a primitive array region.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="fixedBuffer">A <see cref="IReadOnlyFixedMemory"/> instance.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private void SetPrimitiveArrayRegion(JArrayObject jArray, CString signature, IFixedPointer fixedBuffer,
			Int32 index, Int32 count = 1)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			switch (signature[0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					SetBooleanArrayRegionDelegate setBooleanArrayRegion =
						this.GetDelegate<SetBooleanArrayRegionDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					setBooleanArrayRegion(this.Reference, jBooleanArrayRef, index, count,
					                      (ReadOnlyValPtr<Byte>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					SetByteArrayRegionDelegate setByteArrayRegion = this.GetDelegate<SetByteArrayRegionDelegate>();
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					setByteArrayRegion(this.Reference, jByteArrayRef, index, count,
					                   (ReadOnlyValPtr<SByte>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					SetCharArrayRegionDelegate setCharArrayRegion = this.GetDelegate<SetCharArrayRegionDelegate>();
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					setCharArrayRegion(this.Reference, jCharArrayRef, index, count,
					                   (ReadOnlyValPtr<Char>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					SetDoubleArrayRegionDelegate setDoubleArrayRegion =
						this.GetDelegate<SetDoubleArrayRegionDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					setDoubleArrayRegion(this.Reference, jDoubleArrayRef, index, count,
					                     (ReadOnlyValPtr<Double>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					SetFloatArrayRegionDelegate setFloatArrayRegion = this.GetDelegate<SetFloatArrayRegionDelegate>();
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					setFloatArrayRegion(this.Reference, jFloatArrayRef, index, count,
					                    (ReadOnlyValPtr<Single>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					SetIntArrayRegionDelegate setIntArrayRegion = this.GetDelegate<SetIntArrayRegionDelegate>();
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					setIntArrayRegion(this.Reference, jIntArrayRef, index, count,
					                  (ReadOnlyValPtr<Int32>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					SetLongArrayRegionDelegate setLongArrayRegion = this.GetDelegate<SetLongArrayRegionDelegate>();
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					setLongArrayRegion(this.Reference, jLongArrayRef, index, count,
					                   (ReadOnlyValPtr<Int64>)fixedBuffer.Pointer);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					SetShortArrayRegionDelegate setShortArrayRegion = this.GetDelegate<SetShortArrayRegionDelegate>();
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					setShortArrayRegion(this.Reference, jShortArrayRef, index, count,
					                    (ReadOnlyValPtr<Int16>)fixedBuffer.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Creates a new object array.
		/// </summary>
		/// <param name="length">Array length.</param>
		/// <param name="jClass">Array class.</param>
		/// <param name="jLocal">Initializer array element.</param>
		/// <returns>Created array <see cref="JArrayLocalRef"/> reference.</returns>
		private JArrayLocalRef NewObjectArray(Int32 length, JClassObject jClass, JLocalObject? jLocal = default)
		{
			NewObjectArrayDelegate newObjectArray = this.GetDelegate<NewObjectArrayDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JObjectLocalRef initialRef = jniTransaction.Add(jLocal);
			JObjectArrayLocalRef arrayRef = newObjectArray(this.Reference, length, classRef, initialRef);
			if (arrayRef.Value == default) this.CheckJniError();
			return arrayRef.ArrayValue;
		}
		/// <summary>
		/// Fills <paramref name="jArray"/> with <paramref name="initialElement"/> value.
		/// </summary>
		/// <typeparam name="TElement">A <see cref="IDataType"/> primitive type.</typeparam>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="initialElement">A <see cref="IDataType"/> primitive value.</param>
		/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
		private void FillPrimitiveArray<TElement>(JArrayObject jArray, JDataTypeMetadata metadata,
			TElement initialElement) where TElement : IObject, IDataType<TElement>
		{
			Int32 requiredBytes = metadata.SizeOf * jArray.Length;
			Boolean useStackAlloc = this.UseStackAlloc(requiredBytes);
			using IFixedContext<Byte>.IDisposable arrayRegion = requiredBytes == 0 ?
				ValPtr<Byte>.Zero.GetUnsafeFixedContext(0) :
				useStackAlloc ? JEnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
					new Byte[requiredBytes].AsMemory().GetFixedContext();
			Int32 offset = 0;
			while (offset < requiredBytes)
				initialElement.CopyTo(arrayRegion.Bytes, ref offset);
			this.SetPrimitiveArrayRegion(jArray, metadata.Signature, arrayRegion, 0, jArray.Length);
		}
	}
}