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
				JArrayLocalRef arrayRef = this.NewObjectArray(length, jClass, initialElement as JReferenceObject);
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
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			return this.GetElementObject<TElement>(arrayRef, index);
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
				this.SetObjectElement(jArray, index, value as JReferenceObject);
			}
		}
		public Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();

			if (metadata is not JPrimitiveTypeMetadata primitiveMetadata)
				return this.IndexOfObject(jArray, item as JReferenceObject);

			Span<Byte> itemSpan = stackalloc Byte[primitiveMetadata.SizeOf];
			item!.CopyTo(itemSpan);
			return this.IndexOfPrimitive(jArray, itemSpan);
		}
		public void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ArgumentNullException.ThrowIfNull(array);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(array.Length);

			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
				this.CopyToPrimitive(jArray, primitiveMetadata.SizeOf, array, arrayIndex);
			else
				this.CopyToObject(jArray, array, arrayIndex);
		}
		public INativeMemoryHandle GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ValidationUtilities.ThrowIfDefault(jArray);
			return this.VirtualMachine.CreateMemoryHandle(jArray, referenceKind, false);
		}
		public INativeMemoryHandle GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ValidationUtilities.ThrowIfDefault(jArray);
			return this.VirtualMachine.CreateMemoryHandle(jArray, referenceKind, true);
		}
		public IntPtr GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IntPtr result = this.GetPrimitiveArrayElements(arrayRef, metadata.Signature[0], out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public ValPtr<Byte> GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef)
		{
			GetPrimitiveArrayCriticalDelegate getPrimitiveArrayCriticalDelegate =
				this.GetDelegate<GetPrimitiveArrayCriticalDelegate>();
			ValPtr<Byte> result = getPrimitiveArrayCriticalDelegate(this.Reference, arrayRef, out _);
			if (result == ValPtr<Byte>.Zero) this.CheckJniError();
			return result;
		}
		public void ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			this.ReleasePrimitiveArrayElements(arrayRef, metadata.Signature[0], pointer, mode);
			this.CheckJniError();
		}
		public void ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
		{
			ReleasePrimitiveArrayCriticalDelegate releasePrimitiveArrayCritical =
				this.GetDelegate<ReleasePrimitiveArrayCriticalDelegate>();
			releasePrimitiveArrayCritical(this.Reference, arrayRef, criticalPtr, JReleaseMode.Abort);
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
		public void SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value)
		{
			ValidationUtilities.ThrowIfDummy(value);
			SetObjectArrayElementDelegate setObjectArrayElement = this.GetDelegate<SetObjectArrayElementDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, value);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			setObjectArrayElement(this.Reference, arrayRef, index, localRef);
			this.CheckJniError();
		}

		/// <summary>
		/// Retrieves the element with <paramref name="index"/> on <paramref name="arrayRef"/>.
		/// </summary>
		/// <typeparam name="TElement">Type of <paramref name="arrayRef"/> element.</typeparam>
		/// <param name="arrayRef">A <see cref="JArrayObject"/> reference.</param>
		/// <param name="index">Element index.</param>
		/// <returns>The element with <paramref name="index"/> on <paramref name="arrayRef"/>.</returns>
		private TElement? GetElementObject<TElement>(JObjectArrayLocalRef arrayRef, Int32 index)
			where TElement : IObject, IDataType<TElement>
		{
			JObjectLocalRef localRef = this.GetObjectArrayElement(arrayRef, index);
			return this.CreateObject<TElement>(localRef, true);
		}
		/// <summary>
		/// Determines the index of a specific item in <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="item">The object to locate in <paramref name="jArray"/>.</param>
		/// <returns>The index of <paramref name="item"/> if found in <paramref name="jArray"/>; otherwise, -1.</returns>
		private Int32 IndexOfObject(JArrayObject jArray, JReferenceObject? item)
		{
			ValidationUtilities.ThrowIfDummy(item);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JArrayLocalRef arrayRef = jniTransaction.Add(jArray);
			JObjectLocalRef localRef = jniTransaction.Add(item);
			JObjectArrayLocalRef objectArrayRef =
				NativeUtilities.Transform<JArrayLocalRef, JObjectArrayLocalRef>(in arrayRef);
			JEnvironment env = this._mainClasses.Environment;
			for (Int32 i = 0; i < jArray.Length; i++)
			{
				JObjectLocalRef itemLocalRef = this.GetObjectArrayElement(objectArrayRef, i);
				if (localRef == itemLocalRef || env.IsSame(localRef, itemLocalRef))
					return i;
			}
			return -1;
		}
		/// <summary>
		/// Determines the index of a specific item in <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="itemSpan">The binary information to locate in <paramref name="jArray"/>.</param>
		/// <returns>The index of <paramref name="itemSpan"/> if found in <paramref name="jArray"/>; otherwise, -1.</returns>
		private Int32 IndexOfPrimitive(JArrayObject jArray, ReadOnlySpan<Byte> itemSpan)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JArrayLocalRef arrayRef = jniTransaction.Add(jArray);
			Int32 binaryLength = jArray.Length * itemSpan.Length;
			ValPtr<Byte> criticalPtr = this.GetPrimitiveCriticalSequence(arrayRef);
			try
			{
				Span<Byte> span = criticalPtr.Pointer.GetUnsafeSpan<Byte>(binaryLength);
				for (Int32 offset = 0; offset < binaryLength; offset += itemSpan.Length)
				{
					if (itemSpan.SequenceEqual(span.Slice(offset, itemSpan.Length)))
						return offset % itemSpan.Length;
				}
			}
			finally
			{
				this.ReleasePrimitiveCriticalSequence(arrayRef, criticalPtr);
			}
			return -1;
		}
		/// <summary>
		/// Copies the elements of the <paramref name="jArray"/> to an <see cref="T:System.Array"/>,
		/// starting at a particular <see cref="T:System.Array"/> index.
		/// </summary>
		/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="sizeOf">Size of primitive value.</param>
		/// <param name="array">
		/// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied
		/// from <paramref name="jArray"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in <paramref name="array"/> at which copying begins.
		/// </param>
		private void CopyToPrimitive<TElement>(JArrayObject jArray, Int32 sizeOf, TElement?[] array, Int32 arrayIndex)
			where TElement : IObject, IDataType<TElement>
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			using MemoryHandle handle = array.AsMemory().Pin();
			Span<Byte> bytes = handle.GetUnsafeSpan<Byte>(sizeOf * array.Length);
			JArrayLocalRef arrayRef = jniTransaction.Add(jArray);
			Int32 offset = sizeOf * arrayIndex;
			ValPtr<Byte> criticalPtr = this.GetPrimitiveCriticalSequence(arrayRef);
			try
			{
				Span<Byte> span = criticalPtr.Pointer.GetUnsafeSpan<Byte>(sizeOf * jArray.Length);
				span[offset..].CopyTo(bytes);
			}
			finally
			{
				this.ReleasePrimitiveCriticalSequence(arrayRef, criticalPtr);
			}
		}
		/// <summary>
		/// Copies the elements of the <paramref name="jArray"/> to an <see cref="T:System.Array"/>,
		/// starting at a particular <see cref="T:System.Array"/> index.
		/// </summary>
		/// <typeparam name="TElement">Type of <paramref name="jArray"/> element.</typeparam>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="array">
		/// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied
		/// from <paramref name="jArray"/>. The <see cref="T:System.Array"/> must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in <paramref name="array"/> at which copying begins.
		/// </param>
		private void CopyToObject<TElement>(JArrayObject jArray, IList<TElement?> array, Int32 arrayIndex)
			where TElement : IObject, IDataType<TElement>
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(array.Count + 1);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			for (Int32 i = 0; i < array.Count; i++)
				array[i] = this.GetElementObject<TElement>(arrayRef, i + arrayIndex);
		}
		/// <summary>
		/// Retrieves <see cref="JObjectLocalRef"/> reference from <paramref name="arrayRef"/> at
		/// <paramref name="index"/>.
		/// </summary>
		/// <param name="arrayRef">A <see cref="JObjectArrayLocalRef"/> reference.</param>
		/// <param name="index">Element index.</param>
		/// <returns>The element with <paramref name="index"/> on <paramref name="arrayRef"/>.</returns>
		/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
		private JObjectLocalRef GetObjectArrayElement(JObjectArrayLocalRef arrayRef, Int32 index)
		{
			GetObjectArrayElementDelegate getObjectArrayElement = this.GetDelegate<GetObjectArrayElementDelegate>();
			JObjectLocalRef localRef = getObjectArrayElement(this.Reference, arrayRef, index);
			if (localRef == default) this.CheckJniError();
			return localRef;
		}
		private IntPtr GetPrimitiveArrayElements(JArrayLocalRef arrayRef, Byte signature, out Byte isCopy)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					GetBooleanArrayElementsDelegate getBooleanArrayElements =
						this.GetDelegate<GetBooleanArrayElementsDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JBooleanArrayLocalRef>(in arrayRef);
					return getBooleanArrayElements(this.Reference, jBooleanArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetByteArrayElementsDelegate getByteArrayElements =
						this.GetDelegate<GetByteArrayElementsDelegate>();
					JByteArrayLocalRef jByteArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JByteArrayLocalRef>(in arrayRef);
					return getByteArrayElements(this.Reference, jByteArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetCharArrayElementsDelegate getCharArrayElements =
						this.GetDelegate<GetCharArrayElementsDelegate>();
					JCharArrayLocalRef jCharArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JCharArrayLocalRef>(in arrayRef);
					return getCharArrayElements(this.Reference, jCharArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetDoubleArrayElementsDelegate getDoubleArrayElements =
						this.GetDelegate<GetDoubleArrayElementsDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JDoubleArrayLocalRef>(in arrayRef);
					return getDoubleArrayElements(this.Reference, jDoubleArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetFloatArrayElementsDelegate getFloatArrayElements =
						this.GetDelegate<GetFloatArrayElementsDelegate>();
					JFloatArrayLocalRef jFloatArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JFloatArrayLocalRef>(in arrayRef);
					return getFloatArrayElements(this.Reference, jFloatArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetIntArrayElementsDelegate getIntArrayElements = this.GetDelegate<GetIntArrayElementsDelegate>();
					JIntArrayLocalRef jIntArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JIntArrayLocalRef>(in arrayRef);
					return getIntArrayElements(this.Reference, jIntArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetLongArrayElementsDelegate getLongArrayElements =
						this.GetDelegate<GetLongArrayElementsDelegate>();
					JLongArrayLocalRef jLongArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JLongArrayLocalRef>(in arrayRef);
					return getLongArrayElements(this.Reference, jLongArrayRef, out isCopy);
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetShortArrayElementsDelegate getShortArrayElements =
						this.GetDelegate<GetShortArrayElementsDelegate>();
					JShortArrayLocalRef jShortArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JShortArrayLocalRef>(in arrayRef);
					return getShortArrayElements(this.Reference, jShortArrayRef, out isCopy);
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		private void ReleasePrimitiveArrayElements(JArrayLocalRef arrayRef, Byte signature, IntPtr pointer,
			JReleaseMode mode)
		{
			switch (signature)
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					ReleaseBooleanArrayElementsDelegate releaseBooleanArrayElements =
						this.GetDelegate<ReleaseBooleanArrayElementsDelegate>();
					JBooleanArrayLocalRef jBooleanArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JBooleanArrayLocalRef>(in arrayRef);
					releaseBooleanArrayElements(this.Reference, jBooleanArrayRef, (ReadOnlyValPtr<Byte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					ReleaseByteArrayElementsDelegate releaseByteArrayElements =
						this.GetDelegate<ReleaseByteArrayElementsDelegate>();
					JByteArrayLocalRef jByteArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JByteArrayLocalRef>(in arrayRef);
					releaseByteArrayElements(this.Reference, jByteArrayRef, (ReadOnlyValPtr<SByte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					ReleaseCharArrayElementsDelegate releaseCharArrayElements =
						this.GetDelegate<ReleaseCharArrayElementsDelegate>();
					JCharArrayLocalRef jCharArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JCharArrayLocalRef>(in arrayRef);
					releaseCharArrayElements(this.Reference, jCharArrayRef, (ReadOnlyValPtr<Char>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					ReleaseDoubleArrayElementsDelegate releaseDoubleArrayElements =
						this.GetDelegate<ReleaseDoubleArrayElementsDelegate>();
					JDoubleArrayLocalRef jDoubleArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JDoubleArrayLocalRef>(in arrayRef);
					releaseDoubleArrayElements(this.Reference, jDoubleArrayRef, (ReadOnlyValPtr<Double>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					ReleaseFloatArrayElementsDelegate releaseFloatArrayElements =
						this.GetDelegate<ReleaseFloatArrayElementsDelegate>();
					JFloatArrayLocalRef jFloatArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JFloatArrayLocalRef>(in arrayRef);
					releaseFloatArrayElements(this.Reference, jFloatArrayRef, (ReadOnlyValPtr<Single>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					ReleaseIntArrayElementsDelegate releaseIntArrayElements =
						this.GetDelegate<ReleaseIntArrayElementsDelegate>();
					JIntArrayLocalRef jIntArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JIntArrayLocalRef>(in arrayRef);
					releaseIntArrayElements(this.Reference, jIntArrayRef, (ReadOnlyValPtr<Int32>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					ReleaseLongArrayElementsDelegate releaseLongArrayElements =
						this.GetDelegate<ReleaseLongArrayElementsDelegate>();
					JLongArrayLocalRef jLongArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JLongArrayLocalRef>(in arrayRef);
					releaseLongArrayElements(this.Reference, jLongArrayRef, (ReadOnlyValPtr<Int64>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					ReleaseShortArrayElementsDelegate releaseShortArrayElements =
						this.GetDelegate<ReleaseShortArrayElementsDelegate>();
					JShortArrayLocalRef jShortArrayRef =
						NativeUtilities.Transform<JArrayLocalRef, JShortArrayLocalRef>(in arrayRef);
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
		/// <param name="jObject">Initializer array element.</param>
		/// <returns>Created array <see cref="JArrayLocalRef"/> reference.</returns>
		private JArrayLocalRef NewObjectArray(Int32 length, JClassObject jClass, JReferenceObject? jObject = default)
		{
			NewObjectArrayDelegate newObjectArray = this.GetDelegate<NewObjectArrayDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JObjectLocalRef initialRef = this.UseObject(jniTransaction, jObject);
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