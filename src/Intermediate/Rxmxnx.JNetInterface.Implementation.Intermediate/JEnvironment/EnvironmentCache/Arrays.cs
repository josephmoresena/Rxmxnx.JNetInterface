namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache
	{
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
			JObjectArrayLocalRef objectArrayRef = JObjectArrayLocalRef.FromReference(in arrayRef);
			for (Int32 i = 0; i < jArray.Length; i++)
			{
				JObjectLocalRef itemLocalRef = this.GetObjectArrayElement(objectArrayRef, i);
				if (localRef == itemLocalRef || this._env.IsSame(localRef, itemLocalRef))
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
					return getBooleanArrayElements(this.Reference, JBooleanArrayLocalRef.FromReference(in arrayRef),
					                               out isCopy);
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					GetByteArrayElementsDelegate getByteArrayElements =
						this.GetDelegate<GetByteArrayElementsDelegate>();
					return getByteArrayElements(this.Reference, JByteArrayLocalRef.FromReference(in arrayRef),
					                            out isCopy);
				case UnicodePrimitiveSignatures.CharSignatureChar:
					GetCharArrayElementsDelegate getCharArrayElements =
						this.GetDelegate<GetCharArrayElementsDelegate>();
					return getCharArrayElements(this.Reference, JCharArrayLocalRef.FromReference(in arrayRef),
					                            out isCopy);
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					GetDoubleArrayElementsDelegate getDoubleArrayElements =
						this.GetDelegate<GetDoubleArrayElementsDelegate>();
					return getDoubleArrayElements(this.Reference, JDoubleArrayLocalRef.FromReference(in arrayRef),
					                              out isCopy);
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					GetFloatArrayElementsDelegate getFloatArrayElements =
						this.GetDelegate<GetFloatArrayElementsDelegate>();
					return getFloatArrayElements(this.Reference, JFloatArrayLocalRef.FromReference(in arrayRef),
					                             out isCopy);
				case UnicodePrimitiveSignatures.IntSignatureChar:
					GetIntArrayElementsDelegate getIntArrayElements = this.GetDelegate<GetIntArrayElementsDelegate>();
					return getIntArrayElements(this.Reference, JIntArrayLocalRef.FromReference(in arrayRef),
					                           out isCopy);
				case UnicodePrimitiveSignatures.LongSignatureChar:
					GetLongArrayElementsDelegate getLongArrayElements =
						this.GetDelegate<GetLongArrayElementsDelegate>();
					return getLongArrayElements(this.Reference, JLongArrayLocalRef.FromReference(in arrayRef),
					                            out isCopy);
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					GetShortArrayElementsDelegate getShortArrayElements =
						this.GetDelegate<GetShortArrayElementsDelegate>();
					return getShortArrayElements(this.Reference, JShortArrayLocalRef.FromReference(in arrayRef),
					                             out isCopy);
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
					releaseBooleanArrayElements(this.Reference, JBooleanArrayLocalRef.FromReference(in arrayRef),
					                            (ReadOnlyValPtr<Byte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					ReleaseByteArrayElementsDelegate releaseByteArrayElements =
						this.GetDelegate<ReleaseByteArrayElementsDelegate>();
					releaseByteArrayElements(this.Reference, JByteArrayLocalRef.FromReference(in arrayRef),
					                         (ReadOnlyValPtr<SByte>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					ReleaseCharArrayElementsDelegate releaseCharArrayElements =
						this.GetDelegate<ReleaseCharArrayElementsDelegate>();
					releaseCharArrayElements(this.Reference, JCharArrayLocalRef.FromReference(in arrayRef),
					                         (ReadOnlyValPtr<Char>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					ReleaseDoubleArrayElementsDelegate releaseDoubleArrayElements =
						this.GetDelegate<ReleaseDoubleArrayElementsDelegate>();
					releaseDoubleArrayElements(this.Reference, JDoubleArrayLocalRef.FromReference(in arrayRef),
					                           (ReadOnlyValPtr<Double>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					ReleaseFloatArrayElementsDelegate releaseFloatArrayElements =
						this.GetDelegate<ReleaseFloatArrayElementsDelegate>();
					releaseFloatArrayElements(this.Reference, JFloatArrayLocalRef.FromReference(in arrayRef),
					                          (ReadOnlyValPtr<Single>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					ReleaseIntArrayElementsDelegate releaseIntArrayElements =
						this.GetDelegate<ReleaseIntArrayElementsDelegate>();
					releaseIntArrayElements(this.Reference, JIntArrayLocalRef.FromReference(in arrayRef),
					                        (ReadOnlyValPtr<Int32>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					ReleaseLongArrayElementsDelegate releaseLongArrayElements =
						this.GetDelegate<ReleaseLongArrayElementsDelegate>();
					releaseLongArrayElements(this.Reference, JLongArrayLocalRef.FromReference(in arrayRef),
					                         (ReadOnlyValPtr<Int64>)pointer, mode);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					ReleaseShortArrayElementsDelegate releaseShortArrayElements =
						this.GetDelegate<ReleaseShortArrayElementsDelegate>();
					releaseShortArrayElements(this.Reference, JShortArrayLocalRef.FromReference(in arrayRef),
					                          (ReadOnlyValPtr<Int16>)pointer, mode);
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
			if (arrayRef.IsDefault) this.CheckJniError();
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
			using IFixedContext<Byte>.IDisposable arrayRegion = useStackAlloc && requiredBytes > 0 ?
				EnvironmentCache.AllocToFixedContext(stackalloc Byte[requiredBytes], this) :
				EnvironmentCache.AllocToFixedContext<Byte>(requiredBytes);
			Int32 offset = 0;
			while (offset < requiredBytes)
				initialElement.CopyTo(arrayRegion.Bytes, ref offset);
			this.SetPrimitiveArrayRegion(jArray, metadata.Signature, arrayRegion, 0, jArray.Length);
		}
	}
}