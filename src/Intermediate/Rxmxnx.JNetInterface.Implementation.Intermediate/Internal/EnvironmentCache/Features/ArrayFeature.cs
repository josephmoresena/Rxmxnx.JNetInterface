namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal sealed partial class EnvironmentCache : IArrayFeature
{
	public JArrayObject<TElement> CreateArray<TElement>(Int32 length) where TElement : IDataType<TElement>
	{
		ImplementationValidationUtilities.ThrowIfInvalidArrayLength(length);
		JArrayLocalRef arrayRef = default;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
		{
			Byte primitiveSignature = IDataType.GetMetadata<TElement>().Signature[0];
			ref readonly ArrayFunctionSet arrayFunctions =
				ref this.GetArrayFunctions(primitiveSignature, ArrayFunctionSet.PrimitiveFunction.NewArray);
			arrayRef = primitiveSignature switch
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
			if (arrayRef == default) this.CheckJniError();
		}
		else
		{
			JClassObject jClass = this.GetClass<TElement>();
			arrayRef = this.NewObjectArray(length, jClass);
		}
		return this.Register<JArrayObject<TElement>>(new(this.GetClass<JArrayObject<TElement>>(), arrayRef, length));
	}
	public JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length)
		where TElement : IDataType<TElement>
	{
		this.CheckClassCompatibility<TElement>(jClass, out Boolean sameClass);
		return sameClass ?
			this.CreateArray<TElement>(length) :
			this.CreateObjectArray<TElement>(jClass, length, default);
	}
	public JArrayObject<TElement> CreateArray<TElement>(Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>
	{
		JArrayObject<TElement> result;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
		{
			result = this.CreateArray<TElement>(length);
			if (length > 0 && !initialElement.IsDefault())
				this.FillPrimitiveArray(result, IDataType.GetMetadata<TElement>(), initialElement);
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
	public JArrayObject<TElement> CreateArray<TElement>(JClassObject jClass, Int32 length, TElement initialElement)
		where TElement : IDataType<TElement>
	{
		this.CheckClassCompatibility<TElement>(jClass, out Boolean sameClass);
		return sameClass ?
			this.CreateArray(length, initialElement) :
			this.CreateObjectArray<TElement>(jClass, length, initialElement as JReferenceObject);
	}
	public Int32 GetArrayLength(JReferenceObject jObject)
	{
		ImplementationValidationUtilities.ThrowIfProxy(jObject);
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetArrayLengthInfo);
		Int32 result = nativeInterface.ArrayFunctions.GetArrayLength(this.Reference, jObject.As<JArrayLocalRef>());
		if (result <= 0) this.CheckJniError();
		return result;
	}
	public unsafe TElement? GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
		where TElement : IDataType<TElement>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TElement>();
			Span<Byte> buffer = stackalloc Byte[metadata.SizeOf];
			fixed (Byte* ptr = &MemoryMarshal.GetReference(buffer))
				this.GetPrimitiveArrayRegion(jArray, metadata.Signature[0], new(ptr), index);
			this.CheckJniError();
			return Unsafe.As<Byte, TElement>(ref MemoryMarshal.GetReference(buffer));
		}
		using INativeTransaction jniTransaction = this.Host.MemoryManager.CreateTransaction(1);
		JObjectArrayLocalRef arrayRef = jniTransaction.Add<JObjectArrayLocalRef>(jArray);
		return this.GetElementObject<TElement>(arrayRef, index);
	}
	public unsafe void SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
		where TElement : IDataType<TElement>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
		{
			JDataTypeMetadata metadata = MetadataHelper.GetExactMetadata<TElement>();
			Span<Byte> buffer = stackalloc Byte[metadata.SizeOf];
			value!.CopyTo(buffer);
			fixed (Byte* ptr = &MemoryMarshal.GetReference(buffer))
				this.SetPrimitiveArrayRegion(jArray, metadata.Signature[0], new(ptr), index);
			this.CheckJniError();
		}
		else
		{
			this.SetObjectElement(jArray, index, value as JReferenceObject);
		}
	}
	public Int32 IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item) where TElement : IDataType<TElement>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);

		if (RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
			return this.IndexOfObject(jArray, item as JReferenceObject);

		Span<Byte> itemSpan = stackalloc Byte[IDataType.GetMetadata<TElement>().SizeOf];
		item!.CopyTo(itemSpan);
		return this.IndexOfPrimitive(jArray, itemSpan);
	}
	public void CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
		where TElement : IDataType<TElement>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);
		ImplementationValidationUtilities.ThrowIfInvalidArrayLength(array.Length);

		if (!RuntimeHelpers.IsReferenceOrContainsReferences<TElement>())
			this.CopyToPrimitive(jArray, IDataType.GetMetadata<TElement>().SizeOf, array, arrayIndex);
		else
			this.CopyToObject(jArray, array.AsSpan()[arrayIndex..]); // Offset for destination array
	}
	public INativeMemoryAdapter GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);
		return this.Host.MemoryManager.CreateMemoryAdapter(jArray, referenceKind, false);
	}
	public INativeMemoryAdapter GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);
		return this.Host.MemoryManager.CreateMemoryAdapter(jArray, referenceKind, true);
	}
	public IntPtr GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TPrimitive>();
		IntPtr result = this.GetPrimitiveArrayElements(arrayRef, metadata.Signature[0], out JBoolean isCopyJ);
		if (result == IntPtr.Zero) this.CheckJniError();
		isCopy = isCopyJ.Value;
		return result;
	}
	public IntPtr GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef, out Boolean isCopy)
	{
		ref readonly NativeInterface nativeInterface =
			ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetPrimitiveArrayCriticalInfo);
		IntPtr result =
			nativeInterface.PrimitiveArrayCriticalFunctions.GetPrimitiveArrayCritical(
				this.Reference, arrayRef, out JBoolean isCopyJ);
		if (result == IntPtr.Zero) this.CheckJniError();
		isCopy = isCopyJ.Value;
		this._criticalCount++;
		return result;
	}
	public void ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer, JReleaseMode mode)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		try
		{
			if (this._env.IsAttached && this.Host.IsRunning)
			{
				JDataTypeMetadata metadata = IDataType.GetMetadata<TPrimitive>();
				this.ReleasePrimitiveArrayElements(arrayRef, metadata.Signature[0], pointer, mode);
				this.CheckJniError();
			}
			JTrace.ReleaseMemory(false, this._env.IsAttached, this.Host.IsRunning, true, arrayRef, pointer);
		}
		catch (Exception)
		{
			JTrace.ReleaseMemory(false, this._env.IsAttached, this.Host.IsRunning, false, arrayRef, pointer);
			throw;
		}
	}
	public void ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, IntPtr criticalPtr, JReleaseMode mode)
	{
		try
		{
			if (this._env.IsAttached && this.Host.IsRunning)
			{
				ref readonly NativeInterface nativeInterface =
					ref this.GetNativeInterface<NativeInterface>(NativeInterface.ReleasePrimitiveArrayCriticalInfo);
				nativeInterface.PrimitiveArrayCriticalFunctions.ReleasePrimitiveArrayCritical(
					this.Reference, arrayRef, criticalPtr, mode);
				this.CheckJniError();
				this._criticalCount--;
			}
			JTrace.ReleaseMemory(true, this._env.IsAttached, this.Host.IsRunning, true, arrayRef, criticalPtr);
		}
		catch (Exception)
		{
			JTrace.ReleaseMemory(true, this._env.IsAttached, this.Host.IsRunning, false, arrayRef, criticalPtr);
			throw;
		}
	}
	public unsafe void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);
		JDataTypeMetadata metadata = IDataType.GetMetadata<TPrimitive>();
		fixed (TPrimitive* ptr = &MemoryMarshal.GetReference(elements))
			this.GetPrimitiveArrayRegion(jArray, metadata.Signature[0], new(ptr), startIndex, elements.Length);
		this.CheckJniError();
	}
	public unsafe void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		ImplementationValidationUtilities.ThrowIfProxy(jArray);
		ImplementationValidationUtilities.ThrowIfDefault(jArray);
		JDataTypeMetadata metadata = IDataType.GetMetadata<TPrimitive>();
		fixed (TPrimitive* ptr = &MemoryMarshal.GetReference(elements))
			this.SetPrimitiveArrayRegion(jArray, metadata.Signature[0], new(ptr), startIndex, elements.Length);
		this.CheckJniError();
	}
}