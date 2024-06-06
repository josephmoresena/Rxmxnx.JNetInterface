namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial record EnvironmentCache : IArrayFeature
	{
		public unsafe JArrayObject<TElement> CreateArray<TElement>(Int32 length)
			where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfInvalidArrayLength(length);
			JArrayLocalRef arrayRef = default;
			if (MetadataHelper.GetMetadata<TElement>() is JPrimitiveTypeMetadata metadata)
			{
				ref readonly ArrayFunctionSet arrayFunctions =
					ref this.GetArrayFunctions(metadata.Signature[0], ArrayFunctionSet.PrimitiveFunction.NewArray);
				arrayRef = metadata.Signature[0] switch
				{
					CommonNames.BooleanSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewBooleanArray
					                                                  .NewArray(this.Reference, length).ArrayValue,
					CommonNames.ByteSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewByteArray
					                                               .NewArray(this.Reference, length).ArrayValue,
					CommonNames.CharSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewCharArray
					                                               .NewArray(this.Reference, length).ArrayValue,
					CommonNames.DoubleSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewDoubleArray
					                                                 .NewArray(this.Reference, length).ArrayValue,
					CommonNames.FloatSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewFloatArray
					                                                .NewArray(this.Reference, length).ArrayValue,
					CommonNames.IntSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewIntArray
					                                              .NewArray(this.Reference, length).ArrayValue,
					CommonNames.LongSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewLongArray
					                                               .NewArray(this.Reference, length).ArrayValue,
					CommonNames.ShortSignatureChar => arrayFunctions.NewPrimitiveArrayFunctions.NewShortArray
					                                                .NewArray(this.Reference, length).ArrayValue,
					_ => arrayRef,
				};
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
		public unsafe TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> buffer = stackalloc Byte[primitiveMetadata.SizeOf];
				fixed (Byte* ptr = &MemoryMarshal.GetReference(buffer))
					this.GetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, new(ptr), index);
				this.CheckJniError();
				return (TElement)primitiveMetadata.CreateInstance(buffer);
			}
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
			return this.GetElementObject<TElement>(arrayRef, index);
		}
		public unsafe void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
			where TElement : IObject, IDataType<TElement>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			JDataTypeMetadata metadata = MetadataHelper.GetMetadata<TElement>();
			if (metadata is JPrimitiveTypeMetadata primitiveMetadata)
			{
				Span<Byte> buffer = stackalloc Byte[primitiveMetadata.SizeOf];
				value!.CopyTo(buffer);
				fixed (Byte* ptr = &MemoryMarshal.GetReference(buffer))
					this.SetPrimitiveArrayRegion(jArray, primitiveMetadata.Signature, new(ptr), index);
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
			IntPtr result = this.GetPrimitiveArrayElements(arrayRef, metadata.Signature[0], out JBoolean isCopyJ);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyJ.Value;
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
		public unsafe void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			fixed (TPrimitive* ptr = &MemoryMarshal.GetReference(elements))
				this.GetPrimitiveArrayRegion(jArray, metadata.Signature, new(ptr), startIndex, elements.Length);
			this.CheckJniError();
		}
		public unsafe void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
			Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			ImplementationValidationUtilities.ThrowIfProxy(jArray);
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			fixed (TPrimitive* ptr = &MemoryMarshal.GetReference(elements))
				this.SetPrimitiveArrayRegion(jArray, metadata.Signature, new(ptr), startIndex, elements.Length);
			this.CheckJniError();
		}
	}
}