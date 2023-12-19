namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IArrayProvider
	{
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
			JObjectLocalRef localRef = getObjectArrayElement(this.Reference, jArray.Reference, index);
			if (localRef == default) this.CheckJniError();
			return this.Cast<TElement>(new(env, localRef, false, this.GetClass<TElement>()));
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
			}
			else
			{
				ValidationUtilities.ThrowIfDummy(value as JReferenceObject);
				SetObjectArrayElementDelegate setObjectArrayElement = this.GetDelegate<SetObjectArrayElementDelegate>();
				JObjectLocalRef localRef = (value as JReferenceObject)?.As<JObjectLocalRef>() ?? default;
				setObjectArrayElement(this.Reference, jArray.Reference, index, localRef);
			}
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
			switch (signature[0])
			{
				case 0x90: //Z
					GetBooleanArrayElementsDelegate getBooleanArrayElements =
						this.GetDelegate<GetBooleanArrayElementsDelegate>();
					return getBooleanArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x66: //B
					GetByteArrayElementsDelegate getByteArrayElements =
						this.GetDelegate<GetByteArrayElementsDelegate>();
					return getByteArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x67: //C
					GetCharArrayElementsDelegate getCharArrayElements =
						this.GetDelegate<GetCharArrayElementsDelegate>();
					return getCharArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x68: //D
					GetDoubleArrayElementsDelegate getDoubleArrayElements =
						this.GetDelegate<GetDoubleArrayElementsDelegate>();
					return getDoubleArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x70: //F
					GetFloatArrayElementsDelegate getFloatArrayElements =
						this.GetDelegate<GetFloatArrayElementsDelegate>();
					return getFloatArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x73: //I
					GetIntArrayElementsDelegate getIntArrayElements = this.GetDelegate<GetIntArrayElementsDelegate>();
					return getIntArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x74: //J
					GetLongArrayElementsDelegate getLongArrayElements =
						this.GetDelegate<GetLongArrayElementsDelegate>();
					return getLongArrayElements(this.Reference, jArray.Reference, out isCopy);
				case 0x83: //S
					GetShortArrayElementsDelegate getShortArrayElements =
						this.GetDelegate<GetShortArrayElementsDelegate>();
					return getShortArrayElements(this.Reference, jArray.Reference, out isCopy);
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
			switch (signature[0])
			{
				case 0x90: //Z
					ReleaseBooleanArrayElementsDelegate releaseBooleanArrayElements =
						this.GetDelegate<ReleaseBooleanArrayElementsDelegate>();
					releaseBooleanArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Byte>)pointer, mode);
					break;
				case 0x66: //B
					ReleaseByteArrayElementsDelegate releaseByteArrayElements =
						this.GetDelegate<ReleaseByteArrayElementsDelegate>();
					releaseByteArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<SByte>)pointer, mode);
					break;
				case 0x67: //C
					ReleaseCharArrayElementsDelegate releaseCharArrayElements =
						this.GetDelegate<ReleaseCharArrayElementsDelegate>();
					releaseCharArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Char>)pointer, mode);
					break;
				case 0x68: //D
					ReleaseDoubleArrayElementsDelegate releaseDoubleArrayElements =
						this.GetDelegate<ReleaseDoubleArrayElementsDelegate>();
					releaseDoubleArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Double>)pointer, mode);
					break;
				case 0x70: //F
					ReleaseFloatArrayElementsDelegate releaseFloatArrayElements =
						this.GetDelegate<ReleaseFloatArrayElementsDelegate>();
					releaseFloatArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Single>)pointer, mode);
					break;
				case 0x73: //I
					ReleaseIntArrayElementsDelegate releaseIntArrayElements =
						this.GetDelegate<ReleaseIntArrayElementsDelegate>();
					releaseIntArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Int32>)pointer, mode);
					break;
				case 0x74: //J
					ReleaseLongArrayElementsDelegate releaseLongArrayElements =
						this.GetDelegate<ReleaseLongArrayElementsDelegate>();
					releaseLongArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Int64>)pointer, mode);
					break;
				case 0x83: //S
					ReleaseShortArrayElementsDelegate releaseShortArrayElements =
						this.GetDelegate<ReleaseShortArrayElementsDelegate>();
					releaseShortArrayElements(this.Reference, jArray.Reference, (ReadOnlyValPtr<Int16>)pointer, mode);
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
			switch (signature[0])
			{
				case 0x90: //Z
					GetBooleanArrayRegionDelegate getBooleanArrayRegion =
						this.GetDelegate<GetBooleanArrayRegionDelegate>();
					getBooleanArrayRegion(this.Reference, jArray.Reference, index, count,
					                      (ValPtr<Byte>)fixedBuffer.Pointer);
					break;
				case 0x66: //B
					GetByteArrayRegionDelegate getByteArrayRegion = this.GetDelegate<GetByteArrayRegionDelegate>();
					getByteArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ValPtr<SByte>)fixedBuffer.Pointer);
					break;
				case 0x67: //C
					GetCharArrayRegionDelegate getCharArrayRegion = this.GetDelegate<GetCharArrayRegionDelegate>();
					getCharArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ValPtr<Char>)fixedBuffer.Pointer);
					break;
				case 0x68: //D
					GetDoubleArrayRegionDelegate getDoubleArrayRegion =
						this.GetDelegate<GetDoubleArrayRegionDelegate>();
					getDoubleArrayRegion(this.Reference, jArray.Reference, index, count,
					                     (ValPtr<Double>)fixedBuffer.Pointer);
					break;
				case 0x70: //F
					GetFloatArrayRegionDelegate getFloatArrayRegion = this.GetDelegate<GetFloatArrayRegionDelegate>();
					getFloatArrayRegion(this.Reference, jArray.Reference, index, count,
					                    (ValPtr<Single>)fixedBuffer.Pointer);
					break;
				case 0x73: //I
					GetIntArrayRegionDelegate getIntArrayRegion = this.GetDelegate<GetIntArrayRegionDelegate>();
					getIntArrayRegion(this.Reference, jArray.Reference, index, count,
					                  (ValPtr<Int32>)fixedBuffer.Pointer);
					break;
				case 0x74: //J
					GetLongArrayRegionDelegate getLongArrayRegion = this.GetDelegate<GetLongArrayRegionDelegate>();
					getLongArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ValPtr<Int64>)fixedBuffer.Pointer);
					break;
				case 0x83: //S
					GetShortArrayRegionDelegate getShortArrayRegion = this.GetDelegate<GetShortArrayRegionDelegate>();
					getShortArrayRegion(this.Reference, jArray.Reference, index, count,
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
		private void SetPrimitiveArrayRegion(JArrayObject jArray, CString signature, IReadOnlyFixedMemory fixedBuffer,
			Int32 index, Int32 count = 1)
		{
			switch (signature[0])
			{
				case 0x90: //Z
					SetBooleanArrayRegionDelegate setBooleanArrayRegion =
						this.GetDelegate<SetBooleanArrayRegionDelegate>();
					setBooleanArrayRegion(this.Reference, jArray.Reference, index, count,
					                      (ReadOnlyValPtr<Byte>)fixedBuffer.Pointer);
					break;
				case 0x66: //B
					SetByteArrayRegionDelegate setByteArrayRegion = this.GetDelegate<SetByteArrayRegionDelegate>();
					setByteArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ReadOnlyValPtr<SByte>)fixedBuffer.Pointer);
					break;
				case 0x67: //C
					SetCharArrayRegionDelegate setCharArrayRegion = this.GetDelegate<SetCharArrayRegionDelegate>();
					setCharArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ReadOnlyValPtr<Char>)fixedBuffer.Pointer);
					break;
				case 0x68: //D
					SetDoubleArrayRegionDelegate setDoubleArrayRegion =
						this.GetDelegate<SetDoubleArrayRegionDelegate>();
					setDoubleArrayRegion(this.Reference, jArray.Reference, index, count,
					                     (ReadOnlyValPtr<Double>)fixedBuffer.Pointer);
					break;
				case 0x70: //F
					SetFloatArrayRegionDelegate setFloatArrayRegion = this.GetDelegate<SetFloatArrayRegionDelegate>();
					setFloatArrayRegion(this.Reference, jArray.Reference, index, count,
					                    (ReadOnlyValPtr<Single>)fixedBuffer.Pointer);
					break;
				case 0x73: //I
					SetIntArrayRegionDelegate setIntArrayRegion = this.GetDelegate<SetIntArrayRegionDelegate>();
					setIntArrayRegion(this.Reference, jArray.Reference, index, count,
					                  (ReadOnlyValPtr<Int32>)fixedBuffer.Pointer);
					break;
				case 0x74: //J
					SetLongArrayRegionDelegate setLongArrayRegion = this.GetDelegate<SetLongArrayRegionDelegate>();
					setLongArrayRegion(this.Reference, jArray.Reference, index, count,
					                   (ReadOnlyValPtr<Int64>)fixedBuffer.Pointer);
					break;
				case 0x83: //S
					SetShortArrayRegionDelegate setShortArrayRegion = this.GetDelegate<SetShortArrayRegionDelegate>();
					setShortArrayRegion(this.Reference, jArray.Reference, index, count,
					                    (ReadOnlyValPtr<Int16>)fixedBuffer.Pointer);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
	}
}