namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Maximum number of bytes usable on stack.
		/// </summary>
		private const Int32 MaxStackBytes = 128;

		/// <summary>
		/// Disposable context for zero pointer.
		/// </summary>
		private static readonly IFixedContext<Byte>.IDisposable zeroByteContext =
			ValPtr<Byte>.Zero.GetUnsafeFixedContext(0);

		/// <summary>
		/// Retrieves JNI version for <paramref name="envRef"/>.
		/// </summary>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		/// <returns>JNI version for <paramref name="envRef"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe Int32 GetVersion(JEnvironmentRef envRef)
		{
			ref readonly JEnvironmentValue refValue = ref envRef.Reference;
			ref readonly NativeInterface nativeInterface =
				ref Unsafe.AsRef<NativeInterface>(refValue.Pointer.ToPointer());
			return nativeInterface.GetVersion(envRef);
		}
		/// <summary>
		/// Cache finalize method.
		/// </summary>
		/// <param name="obj">A <see cref="EnvironmentCache"/> instance.</param>
		private static void FinalizeCache(Object? obj)
		{
			if (obj is not EnvironmentCache cache) return;
			try
			{
				cache.Thread.Join();
			}
			finally
			{
				JVirtualMachine.RemoveEnvironment(cache.VirtualMachine.Reference, cache.Reference);
				cache._cancellation.Dispose();
			}
		}
		/// <summary>
		/// Creates a <see cref="IFixedContext{Byte}.IDisposable"/> instance from an span created in heap.
		/// </summary>
		/// <param name="count">Number of allocated bytes.</param>
		/// <returns>A <see cref="IFixedContext{Byte}.IDisposable"/> instance</returns>
		private static IFixedContext<Byte>.IDisposable AllocHeapContext(Int32 count)
			=> count == 0 ? EnvironmentCache.zeroByteContext : new Byte[count].AsMemory().GetFixedContext();
		/// <summary>
		/// Creates a <typeparamref name="T"/> span allocated in heap.
		/// </summary>
		/// <typeparam name="T">Type of elements in span.</typeparam>
		/// <param name="count">Number of allocated elements.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private static Span<T> HeapAlloc<T>(Int32 count) where T : unmanaged
			=> count == 0 ? Span<T>.Empty : new T[count].AsSpan();
		/// <summary>
		/// Traces the assignment of a value to a primitive field.
		/// </summary>
		/// <param name="jLocal">Field instance object class.</param>
		/// <param name="jClass">Field declaring class.</param>
		/// <param name="definition">Call definition.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TraceSetPrimitiveField(JLocalObject? jLocal, JClassObject jClass,
			JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			switch (definition.Descriptor[0])
			{
				case CommonNames.BooleanSignatureChar:
					JTrace.SetField<JBoolean>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.ByteSignatureChar:
					JTrace.SetField<JByte>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.CharSignatureChar:
					JTrace.SetField<JChar>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.DoubleSignatureChar:
					JTrace.SetField<JDouble>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.FloatSignatureChar:
					JTrace.SetField<JFloat>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.IntSignatureChar:
					JTrace.SetField<JInt>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.LongSignatureChar:
					JTrace.SetField<JLong>(jLocal, jClass, definition, bytes);
					break;
				case CommonNames.ShortSignatureChar:
					JTrace.SetField<JShort>(jLocal, jClass, definition, bytes);
					break;
			}
		}
		/// <summary>
		/// Retrieves the <c>boolean</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetBooleanArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetBooleanArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseBooleanArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetBooleanArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetBooleanArrayRegionInfo,
				_ => NativeInterface.NewBooleanArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>byte</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetByteArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetByteArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseByteArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetByteArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetByteArrayRegionInfo,
				_ => NativeInterface.NewByteArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>char</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetCharArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetCharArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseCharArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetCharArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetCharArrayRegionInfo,
				_ => NativeInterface.NewCharArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>double</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetDoubleArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetDoubleArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseDoubleArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetDoubleArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetDoubleArrayRegionInfo,
				_ => NativeInterface.NewDoubleArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>float</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetFloatArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetFloatArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseFloatArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetFloatArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetFloatArrayRegionInfo,
				_ => NativeInterface.NewFloatArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>int</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetIntArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetIntArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseIntArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetIntArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetIntArrayRegionInfo,
				_ => NativeInterface.NewIntArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>long</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetLongArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetLongArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseLongArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetLongArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetLongArrayRegionInfo,
				_ => NativeInterface.NewLongArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>short</c> <see cref="JniMethodInfo"/> instance for <paramref name="arrayFunction"/>.
		/// </summary>
		/// <param name="arrayFunction">A <see cref="ArrayFunctionSet.PrimitiveFunction"/> value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetShortArrayFunctionInfo(ArrayFunctionSet.PrimitiveFunction arrayFunction)
			=> arrayFunction switch
			{
				ArrayFunctionSet.PrimitiveFunction.GetElements => NativeInterface.GetShortArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.ReleaseElements => NativeInterface.ReleaseShortArrayElementsInfo,
				ArrayFunctionSet.PrimitiveFunction.GetRegion => NativeInterface.GetShortArrayRegionInfo,
				ArrayFunctionSet.PrimitiveFunction.SetRegion => NativeInterface.SetShortArrayRegionInfo,
				_ => NativeInterface.NewShortArrayInfo,
			};
		/// <summary>
		/// Retrieves the <c>boolean</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetBooleanInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallBooleanMethodInfo : NativeInterface.CallNonVirtualBooleanMethodInfo;
		/// <summary>
		/// Retrieves the <c>byte</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetByteInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallByteMethodInfo : NativeInterface.CallNonVirtualByteMethodInfo;
		/// <summary>
		/// Retrieves the <c>char</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetCharInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallCharMethodInfo : NativeInterface.CallNonVirtualCharMethodInfo;
		/// <summary>
		/// Retrieves the <c>double</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetDoubleInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallDoubleMethodInfo : NativeInterface.CallNonVirtualCharMethodInfo;
		/// <summary>
		/// Retrieves the <c>float</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetFloatInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallFloatMethodInfo : NativeInterface.CallNonVirtualFloatMethodInfo;
		/// <summary>
		/// Retrieves the <c>int</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetIntInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallIntMethodInfo : NativeInterface.CallNonVirtualIntMethodInfo;
		/// <summary>
		/// Retrieves the <c>long</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetLongInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallLongMethodInfo : NativeInterface.CallNonVirtualLongMethodInfo;
		/// <summary>
		/// Retrieves the <c>short</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetShortInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallShortMethodInfo : NativeInterface.CallNonVirtualShortMethodInfo;
		/// <summary>
		/// Retrieves the <c>void</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetVoidInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallVoidMethodInfo : NativeInterface.CallNonVirtualVoidMethodInfo;
		/// <summary>
		/// Retrieves the <c>Object</c> <see cref="JniMethodInfo"/> instance for instance method call.
		/// </summary>
		/// <param name="nonVirtual">Indicates whether current call is non-virtual.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetObjectInstanceMethodInfo(Boolean nonVirtual)
			=> nonVirtual ? NativeInterface.CallObjectMethodInfo : NativeInterface.CallNonVirtualObjectMethodInfo;
		/// <summary>
		/// Retrieves the <c>boolean</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticBooleanFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticBooleanFieldInfo : NativeInterface.SetStaticBooleanFieldInfo;
		/// <summary>
		/// Retrieves the <c>byte</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticByteFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticByteFieldInfo : NativeInterface.SetStaticByteFieldInfo;
		/// <summary>
		/// Retrieves the <c>char</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticCharFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticCharFieldInfo : NativeInterface.SetStaticCharFieldInfo;
		/// <summary>
		/// Retrieves the <c>double</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticDoubleFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticDoubleFieldInfo : NativeInterface.SetStaticDoubleFieldInfo;
		/// <summary>
		/// Retrieves the <c>float</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticFloatFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticFloatFieldInfo : NativeInterface.SetStaticFloatFieldInfo;
		/// <summary>
		/// Retrieves the <c>int</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticIntFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticIntFieldInfo : NativeInterface.SetStaticIntFieldInfo;
		/// <summary>
		/// Retrieves the <c>long</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticLongFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticLongFieldInfo : NativeInterface.SetStaticLongFieldInfo;
		/// <summary>
		/// Retrieves the <c>short</c> <see cref="JniMethodInfo"/> instance for static field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetStaticShortFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetStaticShortFieldInfo : NativeInterface.SetStaticShortFieldInfo;
		/// <summary>
		/// Retrieves the <c>boolean</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceBooleanFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetBooleanFieldInfo : NativeInterface.SetBooleanFieldInfo;
		/// <summary>
		/// Retrieves the <c>byte</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceByteFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetByteFieldInfo : NativeInterface.SetByteFieldInfo;
		/// <summary>
		/// Retrieves the <c>char</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceCharFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetCharFieldInfo : NativeInterface.SetCharFieldInfo;
		/// <summary>
		/// Retrieves the <c>double</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceDoubleFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetDoubleFieldInfo : NativeInterface.SetDoubleFieldInfo;
		/// <summary>
		/// Retrieves the <c>float</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceFloatFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetFloatFieldInfo : NativeInterface.SetFloatFieldInfo;
		/// <summary>
		/// Retrieves the <c>int</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceIntFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetIntFieldInfo : NativeInterface.SetIntFieldInfo;
		/// <summary>
		/// Retrieves the <c>long</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceLongFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetLongFieldInfo : NativeInterface.SetLongFieldInfo;
		/// <summary>
		/// Retrieves the <c>short</c> <see cref="JniMethodInfo"/> instance for instance field call.
		/// </summary>
		/// <param name="getField">Indicates whether current call is for get field value.</param>
		/// <returns>A <see cref="JniMethodInfo"/> instance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static JniMethodInfo GetInstanceShortFieldFunctionInfo(Boolean getField)
			=> getField ? NativeInterface.GetShortFieldInfo : NativeInterface.SetShortFieldInfo;
	}
}