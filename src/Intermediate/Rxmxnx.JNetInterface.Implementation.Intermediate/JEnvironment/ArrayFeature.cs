namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : IArrayFeature
	{
		public JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfInvalidArrayLength(length);
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
						throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
				}
				if (arrayRef.IsDefault) this.CheckJniError();
			}
			else
			{
				JClassObject jClass = this.GetClass<TElement>();
				arrayRef = this.NewObjectArray(length, jClass);
			}
			return this.Register<JArrayObject<TElement>>(new(this.GetClass<JArrayObject<TElement>>(), arrayRef,
			                                                 length));
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
				result = this.Register<JArrayObject<TElement>>(new(this.GetClass<JArrayObject<TElement>>(), arrayRef,
				                                                   length));
			}
			return result;
		}
		public unsafe Int32 GetArrayLength(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetArrayLengthInfo);
			Int32 result = nativeInterface.ArrayFunctions.GetArrayLength(this.Reference, jObject.As<JArrayLocalRef>());
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
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
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
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
		public Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item)
			where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
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
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
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
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			ImplementationValidationUtilities.ThrowIfDefault(jArray);
			return this.VirtualMachine.CreateMemoryAdapter(jArray, referenceKind, false);
		}
		public INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			ImplementationValidationUtilities.ThrowIfDefault(jArray);
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
		public unsafe ValPtr<Byte> GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetPrimitiveArrayCriticalInfo);
			ValPtr<Byte> result =
				nativeInterface.PrimitiveArrayCriticalFunctions.GetPrimitiveArrayCritical(
					this.Reference, arrayRef, out _);
			if (result == ValPtr<Byte>.Zero) this.CheckJniError();
			this._criticalCount++;
			return result;
		}
		public void ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
					this.ReleasePrimitiveArrayElements(arrayRef, metadata.Signature[0], pointer, mode);
					this.CheckJniError();
				}
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, true, arrayRef, pointer);
			}
			catch (Exception)
			{
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, false, arrayRef,
				                     pointer);
				throw;
			}
		}
		public unsafe void ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					ref readonly NativeInterface nativeInterface =
						ref this.GetNativeInterface<NativeInterface>(NativeInterface.ReleasePrimitiveArrayCriticalInfo);
					nativeInterface.PrimitiveArrayCriticalFunctions.ReleasePrimitiveArrayCritical(
						this.Reference, arrayRef, criticalPtr, JReleaseMode.Abort);
					this.CheckJniError();
					this._criticalCount--;
				}
				JTrace.ReleaseMemory(true, this._env.IsAttached, this.VirtualMachine.IsAlive, true, arrayRef,
				                     criticalPtr);
			}
			catch (Exception)
			{
				JTrace.ReleaseMemory(true, this._env.IsAttached, this.VirtualMachine.IsAlive, false, arrayRef,
				                     criticalPtr);
				throw;
			}
		}
		public void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			elements.WithSafeFixed((this, jArray, startIndex), EnvironmentCache.GetPrimitiveArrayRegion);
			this.CheckJniError();
		}
		public void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			elements.WithSafeFixed((this, jArray, startIndex), EnvironmentCache.SetPrimitiveArrayRegion);
			this.CheckJniError();
		}
	}
}