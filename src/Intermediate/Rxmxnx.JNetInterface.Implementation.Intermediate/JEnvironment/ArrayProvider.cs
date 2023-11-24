namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IArrayProvider
	{
		public Int32 GetArrayLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetArrayLengthDelegate getArrayLength = this.GetDelegate<GetArrayLengthDelegate>();
			return getArrayLength(this.Reference, jObject.As<JArrayLocalRef>());
		}
		public TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JDataTypeMetadata metadata = IDataType.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> buffer = stackalloc Byte[primitiveMetadata.SizeOf];
				this.GetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, buffer, index);
				return (TElement)primitiveMetadata.CreateInstance(buffer);
			}
			GetObjectArrayElementDelegate getObjectArrayElement = this.GetDelegate<GetObjectArrayElementDelegate>();
			IEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			JObjectLocalRef localRef = getObjectArrayElement(this.Reference, jArray.As<JArrayLocalRef>(), index);
			return this.Cast<TElement>(new(env, localRef, false, false, this.GetClass<TElement>()));
		}
		public void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
			where TElement : IObject, IDataType<TElement>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JDataTypeMetadata metadata = IDataType.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Int32 offset = 0;
				Span<Byte> span = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(span, ref offset);
				this.SetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, span, index);
			}
			else
			{
				ValidationUtilities.ThrowIfDummy(value as JReferenceObject);
				SetObjectArrayElementDelegate setObjectArrayElement = this.GetDelegate<SetObjectArrayElementDelegate>();
				JObjectLocalRef localRef = (value as JReferenceObject)?.As<JObjectLocalRef>() ?? default;
				setObjectArrayElement(this.Reference, jArray.As<JArrayLocalRef>(), index, localRef);
			}
		}
		public IntPtr GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, out Boolean isCopy)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IntPtr result = this.GetPrimitiveArrayElements(jArray, metadata.Signature, out Byte isCopyByte);
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			GetPrimitiveArrayCriticalDelegate getPrimitiveArrayCriticalDelegate =
				this.GetDelegate<GetPrimitiveArrayCriticalDelegate>();
			return getPrimitiveArrayCriticalDelegate(this.Reference, jArray.As<JArrayLocalRef>(), out _);
		}
		public void ReleaseSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer, JReleaseMode mode)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			this.ReleasePrimitiveArrayElements(jArray, metadata.Signature, pointer, mode);
		}
		public void ReleaseCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray, IntPtr pointer)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ReleasePrimitiveArrayCriticalDelegate releasePrimitiveArrayCritical =
				this.GetDelegate<ReleasePrimitiveArrayCriticalDelegate>();
			releasePrimitiveArrayCritical(this.Reference, jArray.As<JArrayLocalRef>(), pointer, JReleaseMode.Abort);
		}
		public void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex, Span<TPrimitive> elements)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			this.GetPrimitiveArrayRegion(jArray, metadata.Signature, elements.AsBytes(), startIndex, elements.Length);
		}
		public void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			this.SetPrimitiveArrayRegion(jArray, metadata.Signature, elements.AsBytes(), startIndex, elements.Length);
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
					return NativeUtilities.GetUnsafeIntPtr(
						in getBooleanArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x66: //B
					GetByteArrayElementsDelegate getByteArrayElements =
						this.GetDelegate<GetByteArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getByteArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x67: //C
					GetCharArrayElementsDelegate getCharArrayElements =
						this.GetDelegate<GetCharArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getCharArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x68: //D
					GetDoubleArrayElementsDelegate getDoubleArrayElements =
						this.GetDelegate<GetDoubleArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getDoubleArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x70: //F
					GetFloatArrayElementsDelegate getFloatArrayElements =
						this.GetDelegate<GetFloatArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getFloatArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x73: //I
					GetIntArrayElementsDelegate getIntArrayElements = this.GetDelegate<GetIntArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getIntArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x74: //J
					GetLongArrayElementsDelegate getLongArrayElements =
						this.GetDelegate<GetLongArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getLongArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
				case 0x83: //S
					GetShortArrayElementsDelegate getShortArrayElements =
						this.GetDelegate<GetShortArrayElementsDelegate>();
					return NativeUtilities.GetUnsafeIntPtr(
						in getShortArrayElements(this.Reference, jArray.As<JArrayLocalRef>(), out isCopy));
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
					releaseBooleanArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                            in pointer.GetUnsafeReadOnlyReference<Byte>(), mode);
					break;
				case 0x66: //B
					ReleaseByteArrayElementsDelegate releaseByteArrayElements =
						this.GetDelegate<ReleaseByteArrayElementsDelegate>();
					releaseByteArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                         in pointer.GetUnsafeReadOnlyReference<SByte>(), mode);
					break;
				case 0x67: //C
					ReleaseCharArrayElementsDelegate releaseCharArrayElements =
						this.GetDelegate<ReleaseCharArrayElementsDelegate>();
					releaseCharArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                         in pointer.GetUnsafeReadOnlyReference<Char>(), mode);
					break;
				case 0x68: //D
					ReleaseDoubleArrayElementsDelegate releaseDoubleArrayElements =
						this.GetDelegate<ReleaseDoubleArrayElementsDelegate>();
					releaseDoubleArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                           in pointer.GetUnsafeReadOnlyReference<Double>(), mode);
					break;
				case 0x70: //F
					ReleaseFloatArrayElementsDelegate releaseFloatArrayElements =
						this.GetDelegate<ReleaseFloatArrayElementsDelegate>();
					releaseFloatArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                          in pointer.GetUnsafeReadOnlyReference<Single>(), mode);
					break;
				case 0x73: //I
					ReleaseIntArrayElementsDelegate releaseIntArrayElements =
						this.GetDelegate<ReleaseIntArrayElementsDelegate>();
					releaseIntArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                        in pointer.GetUnsafeReadOnlyReference<Int32>(), mode);
					break;
				case 0x74: //J
					ReleaseLongArrayElementsDelegate releaseLongArrayElements =
						this.GetDelegate<ReleaseLongArrayElementsDelegate>();
					releaseLongArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                         in pointer.GetUnsafeReadOnlyReference<Int64>(), mode);
					break;
				case 0x83: //S
					ReleaseShortArrayElementsDelegate releaseShortArrayElements =
						this.GetDelegate<ReleaseShortArrayElementsDelegate>();
					releaseShortArrayElements(this.Reference, jArray.As<JArrayLocalRef>(),
					                          in pointer.GetUnsafeReadOnlyReference<Int16>(), mode);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Copies a primitive array region in <paramref name="buffer"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="buffer">A binary span to hold region.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private void GetPrimitiveArrayRegion(JArrayObject jArray, CString signature, Span<Byte> buffer, Int32 index,
			Int32 count = 1)
		{
			switch (signature[0])
			{
				case 0x90: //Z
					GetBooleanArrayRegionDelegate getBooleanArrayRegion =
						this.GetDelegate<GetBooleanArrayRegionDelegate>();
					getBooleanArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                      ref buffer.AsValue<Byte>());
					break;
				case 0x66: //B
					GetByteArrayRegionDelegate getByteArrayRegion = this.GetDelegate<GetByteArrayRegionDelegate>();
					getByteArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   ref buffer.AsValue<SByte>());
					break;
				case 0x67: //C
					GetCharArrayRegionDelegate getCharArrayRegion = this.GetDelegate<GetCharArrayRegionDelegate>();
					getCharArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   ref buffer.AsValue<Char>());
					break;
				case 0x68: //D
					GetDoubleArrayRegionDelegate getDoubleArrayRegion =
						this.GetDelegate<GetDoubleArrayRegionDelegate>();
					getDoubleArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                     ref buffer.AsValue<Double>());
					break;
				case 0x70: //F
					GetFloatArrayRegionDelegate getFloatArrayRegion = this.GetDelegate<GetFloatArrayRegionDelegate>();
					getFloatArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                    ref buffer.AsValue<Single>());
					break;
				case 0x73: //I
					GetIntArrayRegionDelegate getIntArrayRegion = this.GetDelegate<GetIntArrayRegionDelegate>();
					getIntArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                  ref buffer.AsValue<Int32>());
					break;
				case 0x74: //J
					GetLongArrayRegionDelegate getLongArrayRegion = this.GetDelegate<GetLongArrayRegionDelegate>();
					getLongArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   ref buffer.AsValue<Int64>());
					break;
				case 0x83: //S
					GetShortArrayRegionDelegate getShortArrayRegion = this.GetDelegate<GetShortArrayRegionDelegate>();
					getShortArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                    ref buffer.AsValue<Int16>());
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		/// <summary>
		/// Copies <paramref name="buffer"/> in a primitive array region.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="signature">Primitive signature.</param>
		/// <param name="buffer">A binary read-only span to copy to.</param>
		/// <param name="index">Region start index.</param>
		/// <param name="count">Number of elements in region.</param>
		/// <exception cref="ArgumentException"/>
		private void SetPrimitiveArrayRegion(JArrayObject jArray, CString signature, ReadOnlySpan<Byte> buffer,
			Int32 index, Int32 count = 1)
		{
			switch (signature[0])
			{
				case 0x90: //Z
					SetBooleanArrayRegionDelegate setBooleanArrayRegion =
						this.GetDelegate<SetBooleanArrayRegionDelegate>();
					setBooleanArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                      in buffer.AsValue<Byte>());
					break;
				case 0x66: //B
					SetByteArrayRegionDelegate setByteArrayRegion = this.GetDelegate<SetByteArrayRegionDelegate>();
					setByteArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   in buffer.AsValue<SByte>());
					break;
				case 0x67: //C
					SetCharArrayRegionDelegate setCharArrayRegion = this.GetDelegate<SetCharArrayRegionDelegate>();
					setCharArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   in buffer.AsValue<Char>());
					break;
				case 0x68: //D
					SetDoubleArrayRegionDelegate setDoubleArrayRegion =
						this.GetDelegate<SetDoubleArrayRegionDelegate>();
					setDoubleArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                     in buffer.AsValue<Double>());
					break;
				case 0x70: //F
					SetFloatArrayRegionDelegate setFloatArrayRegion = this.GetDelegate<SetFloatArrayRegionDelegate>();
					setFloatArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                    in buffer.AsValue<Single>());
					break;
				case 0x73: //I
					SetIntArrayRegionDelegate setIntArrayRegion = this.GetDelegate<SetIntArrayRegionDelegate>();
					setIntArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                  in buffer.AsValue<Int32>());
					break;
				case 0x74: //J
					SetLongArrayRegionDelegate setLongArrayRegion = this.GetDelegate<SetLongArrayRegionDelegate>();
					setLongArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                   in buffer.AsValue<Int64>());
					break;
				case 0x83: //S
					SetShortArrayRegionDelegate setShortArrayRegion = this.GetDelegate<SetShortArrayRegionDelegate>();
					setShortArrayRegion(this.Reference, jArray.As<JArrayLocalRef>(), index, count,
					                    in buffer.AsValue<Int16>());
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
	}
}