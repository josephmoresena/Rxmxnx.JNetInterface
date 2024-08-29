namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JReferenceObject"/> instance.</param>
		/// <param name="index">Element index.</param>
		/// <param name="value">Object instance.</param>
		private unsafe void SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value)
		{
			ImplementationValidationUtilities.ThrowIfProxy(value);
			jArray.ValidateObjectElement(value);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.SetObjectArrayElementInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JObjectLocalRef localRef = this.UseObject(jniTransaction, value);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			nativeInterface.ArrayFunctions.ObjectArrayFunctions.SetObjectArrayElement(
				this.Reference, arrayRef, index, localRef);
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
			where TElement : IDataType<TElement>
		{
			JObjectLocalRef localRef = this.GetObjectArrayElement(arrayRef, index);
			return this.CreateObject<TElement>(localRef, true, MetadataHelper.IsFinalType<TElement>());
		}
		/// <summary>
		/// Determines the index of a specific item in <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="item">The object to locate in <paramref name="jArray"/>.</param>
		/// <returns>The index of <paramref name="item"/> if found in <paramref name="jArray"/>; otherwise, -1.</returns>
		private Int32 IndexOfObject(JArrayObject jArray, JReferenceObject? item)
		{
			ImplementationValidationUtilities.ThrowIfProxy(item);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JArrayLocalRef arrayRef = jniTransaction.Add(jArray);
			JObjectLocalRef localRef = jniTransaction.Add(item);
			JObjectArrayLocalRef objectArrayRef = JObjectArrayLocalRef.FromReference(in arrayRef);
			using LocalFrame _ = new(this._env, IVirtualMachine.IndexOfObjectCapacity);
			for (Int32 i = 0; i < jArray.Length; i++)
			{
				JObjectLocalRef itemLocalRef = this.GetObjectArrayElement(objectArrayRef, i);
				if (localRef == itemLocalRef || this._env.IsSame(localRef, itemLocalRef))
					return i;
				this._env.DeleteLocalRef(localRef);
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
			where TElement : IDataType<TElement>
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
			where TElement : IDataType<TElement>
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
		private unsafe JObjectLocalRef GetObjectArrayElement(JObjectArrayLocalRef arrayRef, Int32 index)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetObjectArrayElementInfo);
			JObjectLocalRef localRef =
				nativeInterface.ArrayFunctions.ObjectArrayFunctions.GetObjectArrayElement(
					this.Reference, arrayRef, index);
			if (localRef == default) this.CheckJniError();
			return localRef;
		}
		private unsafe IntPtr GetPrimitiveArrayElements(JArrayLocalRef arrayRef, Byte signature, out JBoolean isCopyJ)
		{
			ref readonly ArrayFunctionSet arrayFunctions =
				ref this.GetArrayFunctions(signature, ArrayFunctionSet.PrimitiveFunction.GetElements);
			IntPtr result = default;
			switch (signature)
			{
				case CommonNames.BooleanSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetBooleanArrayElements.Get(
						this.Reference, JBooleanArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.ByteSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetByteArrayElements.Get(
						this.Reference, JByteArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.CharSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetCharArrayElements.Get(
						this.Reference, JCharArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.DoubleSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetDoubleArrayElements.Get(
						this.Reference, JDoubleArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.FloatSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetFloatArrayElements.Get(
						this.Reference, JFloatArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.IntSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetIntArrayElements.Get(
						this.Reference, JIntArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.LongSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetLongArrayElements.Get(
						this.Reference, JLongArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				case CommonNames.ShortSignatureChar:
					result = arrayFunctions.GetElementsFunctions.GetShortArrayElements.Get(
						this.Reference, JShortArrayLocalRef.FromReference(in arrayRef), out isCopyJ);
					break;
				default:
					isCopyJ = false;
					break;
			}
			return result;
		}
		private unsafe void ReleasePrimitiveArrayElements(JArrayLocalRef arrayRef, Byte signature, IntPtr pointer,
			JReleaseMode mode)
		{
			ref readonly ArrayFunctionSet arrayFunctions =
				ref this.GetArrayFunctions(signature, ArrayFunctionSet.PrimitiveFunction.ReleaseElements);
			switch (signature)
			{
				case CommonNames.BooleanSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseBooleanArrayElements.Release(
						this.Reference, JBooleanArrayLocalRef.FromReference(in arrayRef),
						(ReadOnlyValPtr<JBoolean>)pointer, mode);
					break;
				case CommonNames.ByteSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseByteArrayElements.Release(
						this.Reference, JByteArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JByte>)pointer,
						mode);
					break;
				case CommonNames.CharSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseCharArrayElements.Release(
						this.Reference, JCharArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JChar>)pointer,
						mode);
					break;
				case CommonNames.DoubleSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseDoubleArrayElements.Release(
						this.Reference, JDoubleArrayLocalRef.FromReference(in arrayRef),
						(ReadOnlyValPtr<JDouble>)pointer, mode);
					break;
				case CommonNames.FloatSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseFloatArrayElements.Release(
						this.Reference, JFloatArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JFloat>)pointer,
						mode);
					break;
				case CommonNames.IntSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseIntArrayElements.Release(
						this.Reference, JIntArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JInt>)pointer,
						mode);
					break;
				case CommonNames.LongSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseLongArrayElements.Release(
						this.Reference, JLongArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JLong>)pointer,
						mode);
					break;
				case CommonNames.ShortSignatureChar:
					arrayFunctions.ReleaseElementsFunctions.ReleaseShortArrayElements.Release(
						this.Reference, JShortArrayLocalRef.FromReference(in arrayRef), (ReadOnlyValPtr<JShort>)pointer,
						mode);
					break;
			}
		}
		/// <summary>
		/// Copies a primitive array region in <paramref name="bufferPtr"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="bufferPtr">Buffer memory address.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private unsafe void GetPrimitiveArrayRegion(JArrayObject jArray, CString signature, IntPtr bufferPtr,
			Int32 index, Int32 count = 1)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			ref readonly ArrayFunctionSet arrayFunctions =
				ref this.GetArrayFunctions(signature[0], ArrayFunctionSet.PrimitiveFunction.GetRegion);
			switch (signature[0])
			{
				case CommonNames.BooleanSignatureChar:
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetBooleanArrayRegion.Get(
						this.Reference, jBooleanArrayRef, index, count, (ValPtr<JBoolean>)bufferPtr);
					break;
				case CommonNames.ByteSignatureChar:
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetByteArrayRegion.Get(
						this.Reference, jByteArrayRef, index, count, (ValPtr<JByte>)bufferPtr);
					break;
				case CommonNames.CharSignatureChar:
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetCharArrayRegion.Get(
						this.Reference, jCharArrayRef, index, count, (ValPtr<JChar>)bufferPtr);
					break;
				case CommonNames.DoubleSignatureChar:
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetDoubleArrayRegion.Get(
						this.Reference, jDoubleArrayRef, index, count, (ValPtr<JDouble>)bufferPtr);
					break;
				case CommonNames.FloatSignatureChar:
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetFloatArrayRegion.Get(this.Reference, jFloatArrayRef, index, count,
					                                                       (ValPtr<JFloat>)bufferPtr);
					break;
				case CommonNames.IntSignatureChar:
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetIntArrayRegion.Get(
						this.Reference, jIntArrayRef, index, count, (ValPtr<JInt>)bufferPtr);
					break;
				case CommonNames.LongSignatureChar:
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetLongArrayRegion.Get(
						this.Reference, jLongArrayRef, index, count, (ValPtr<JLong>)bufferPtr);
					break;
				case CommonNames.ShortSignatureChar:
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.GetShortArrayRegion.Get(this.Reference, jShortArrayRef, index, count,
					                                                       (ValPtr<JShort>)bufferPtr);
					break;
			}
		}
		/// <summary>
		/// Copies <paramref name="bufferPtr"/> in a primitive array region.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="bufferPtr">Buffer memory address.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private unsafe void SetPrimitiveArrayRegion(JArrayObject jArray, CString signature, IntPtr bufferPtr,
			Int32 index, Int32 count = 1)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			ref readonly ArrayFunctionSet arrayFunctions =
				ref this.GetArrayFunctions(signature[0], ArrayFunctionSet.PrimitiveFunction.SetRegion);
			switch (signature[0])
			{
				case CommonNames.BooleanSignatureChar:
					JBooleanArrayLocalRef jBooleanArrayRef = jniTransaction.Add<JBooleanArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetBooleanArrayRegion.Set(
						this.Reference, jBooleanArrayRef, index, count, (ReadOnlyValPtr<JBoolean>)bufferPtr);
					break;
				case CommonNames.ByteSignatureChar:
					JByteArrayLocalRef jByteArrayRef = jniTransaction.Add<JByteArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetByteArrayRegion.Set(this.Reference, jByteArrayRef, index, count,
					                                                      (ReadOnlyValPtr<JByte>)bufferPtr);
					break;
				case CommonNames.CharSignatureChar:
					JCharArrayLocalRef jCharArrayRef = jniTransaction.Add<JCharArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetCharArrayRegion.Set(this.Reference, jCharArrayRef, index, count,
					                                                      (ReadOnlyValPtr<JChar>)bufferPtr);
					break;
				case CommonNames.DoubleSignatureChar:
					JDoubleArrayLocalRef jDoubleArrayRef = jniTransaction.Add<JDoubleArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetDoubleArrayRegion.Set(
						this.Reference, jDoubleArrayRef, index, count, (ReadOnlyValPtr<JDouble>)bufferPtr);
					break;
				case CommonNames.FloatSignatureChar:
					JFloatArrayLocalRef jFloatArrayRef = jniTransaction.Add<JFloatArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetFloatArrayRegion.Set(this.Reference, jFloatArrayRef, index, count,
					                                                       (ReadOnlyValPtr<JFloat>)bufferPtr);
					break;
				case CommonNames.IntSignatureChar:
					JIntArrayLocalRef jIntArrayRef = jniTransaction.Add<JIntArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetIntArrayRegion.Set(this.Reference, jIntArrayRef, index, count,
					                                                     (ReadOnlyValPtr<JInt>)bufferPtr);
					break;
				case CommonNames.LongSignatureChar:
					JLongArrayLocalRef jLongArrayRef = jniTransaction.Add<JLongArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetLongArrayRegion.Set(this.Reference, jLongArrayRef, index, count,
					                                                      (ReadOnlyValPtr<JLong>)bufferPtr);
					break;
				case CommonNames.ShortSignatureChar:
					JShortArrayLocalRef jShortArrayRef = jniTransaction.Add<JShortArrayLocalRef>(jArray);
					arrayFunctions.RegionFunctions.SetShortArrayRegion.Set(this.Reference, jShortArrayRef, index, count,
					                                                       (ReadOnlyValPtr<JShort>)bufferPtr);
					break;
			}
		}
		/// <summary>
		/// Creates a new object array.
		/// </summary>
		/// <param name="length">Array length.</param>
		/// <param name="jClass">Array class.</param>
		/// <param name="jObject">Initializer array element.</param>
		/// <returns>Created array <see cref="JArrayLocalRef"/> reference.</returns>
		private unsafe JArrayLocalRef NewObjectArray(Int32 length, JClassObject jClass,
			JReferenceObject? jObject = default)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewArrayObjectInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(this.ReloadClass(jClass));
			JObjectLocalRef initialRef = this.UseObject(jniTransaction, jObject);
			JObjectArrayLocalRef arrayRef =
				nativeInterface.ArrayFunctions.ObjectArrayFunctions.NewObjectArray(
					this.Reference, length, classRef, initialRef);
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
		private unsafe void FillPrimitiveArray<TElement>(JArrayObject jArray, JDataTypeMetadata metadata,
			TElement initialElement) where TElement : IDataType<TElement>
		{
			Int32 requiredBytes = metadata.SizeOf * jArray.Length;
			using StackDisposable stackDisposable =
				this.GetStackDisposable(this.UseStackAlloc(requiredBytes), requiredBytes);
			Span<Byte> buffer = stackDisposable.UsingStack ?
				stackalloc Byte[requiredBytes] :
				EnvironmentCache.HeapAlloc<Byte>(requiredBytes);
			Int32 offset = 0;
			while (offset < requiredBytes)
				initialElement.CopyTo(buffer, ref offset);
			fixed (Byte* ptr = &MemoryMarshal.GetReference(buffer))
				this.SetPrimitiveArrayRegion(jArray, metadata.Signature, new(ptr), 0, jArray.Length);
		}
	}
}