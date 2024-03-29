namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : IArrayFeature
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
				if (arrayRef.IsDefault) this.CheckJniError();
			}
			else
			{
				JClassObject jClass = this.GetClass<TElement>();
				arrayRef = this.NewObjectArray(length, jClass);
			}
			return new(this._env, arrayRef, length);
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
				JClassObject jClass = this.GetClass<TElement>();
				JArrayLocalRef arrayRef = this.NewObjectArray(length, jClass, initialElement as JReferenceObject);
				result = new(this._env, arrayRef, length);
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
					EnvironmentCache.AllocToFixedContext(stackalloc Byte[primitiveMetadata.SizeOf]);
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
				Span<Byte> buffer = stackalloc Byte[primitiveMetadata.SizeOf];
				using IFixedContext<Byte>.IDisposable fixedBuffer =
					EnvironmentCache.AllocToFixedContext(stackalloc Byte[primitiveMetadata.SizeOf]);
				value!.CopyTo(buffer);
				this.SetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, fixedBuffer, index);
				this.CheckJniError();
			}
			else
			{
				this.SetObjectElement(jArray, index, value as JReferenceObject);
			}
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
		public INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ValidationUtilities.ThrowIfDefault(jArray);
			return this.VirtualMachine.CreateMemoryAdapter(jArray, referenceKind, false);
		}
		public INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ValidationUtilities.ThrowIfDummy(jArray);
			ValidationUtilities.ThrowIfDefault(jArray);
			return this.VirtualMachine.CreateMemoryAdapter(jArray, referenceKind, true);
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
			this._criticalCount++;
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
			this._criticalCount--;
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
	}
}