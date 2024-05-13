namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Maximum number of bytes usable on stack.
		/// </summary>
		private const Int32 MaxStackBytes = 128;

		/// <summary>
		/// Retrieves the <see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.
		/// </summary>
		/// <param name="returnType">A <see cref="JClassObject"/> instance.</param>
		/// <returns><see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.</returns>
		private static IReflectionMetadata? GetReflectionMetadata(JClassObject returnType)
		{
			using JStringObject className = NativeFunctionSetImpl.Instance.GetClassName(returnType);
			using JNativeMemory<Byte> mem = className.GetNativeUtf8Chars();
			return MetadataHelper.GetReflectionMetadata(mem.Values);
		}
		/// <summary>
		/// Retrieves a <see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.
		/// </summary>
		/// <param name="parameterTypes">A <see cref="JClassObject"/> list.</param>
		/// <returns><see cref="JArgumentMetadata"/> array from <paramref name="parameterTypes"/>.</returns>
		private static JArgumentMetadata[] GetCallMetadata(IReadOnlyList<JClassObject> parameterTypes)
		{
			JArgumentMetadata[] args = new JArgumentMetadata[parameterTypes.Count];
			for (Int32 i = 0; i < parameterTypes.Count; i++)
			{
				using JClassObject jClass = parameterTypes[i];
				using JStringObject className = NativeFunctionSetImpl.Instance.GetClassName(jClass);
				using JNativeMemory<Byte> mem = className.GetNativeUtf8Chars();
				args[i] = MetadataHelper.GetReflectionMetadata(mem.Values)!.ArgumentMetadata;
			}
			return args;
		}
		/// <summary>
		/// Retrieves JNI version for <paramref name="envRef"/>.
		/// </summary>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> instance.</param>
		/// <returns>JNI version for <paramref name="envRef"/>.</returns>
		private static Int32 GetVersion(JEnvironmentRef envRef)
		{
			GetVersionDelegate getVersion =
				envRef.Reference.Reference.GetVersionPointer.GetUnsafeDelegate<GetVersionDelegate>()!;
			return getVersion(envRef);
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
				JVirtualMachine.RemoveEnvironment(cache.VirtualMachine.Reference, cache.Reference);
			}
			finally
			{
				cache._cancellation.Dispose();
			}
		}
		/// <summary>
		/// Constructs an <typeparamref name="TThrowable"/> exception with the message specified by
		/// <paramref name="messageMem"/> and causes that exception to be thrown.
		/// </summary>
		/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
		/// <param name="messageMem">Fixed exception message.</param>
		/// <param name="cache">A <see cref="EnvironmentCache"/> instance.</param>
		/// <returns>JNI code result.</returns>
		private static JResult ThrowNew<TThrowable>(in IReadOnlyFixedMemory messageMem, EnvironmentCache cache)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			JClassObject jClass = cache.GetClass<TThrowable>();
			ThrowNewDelegate throwNew = cache.GetDelegate<ThrowNewDelegate>();
			using INativeTransaction jniTransaction = cache.VirtualMachine.CreateTransaction(2);
			JClassLocalRef classRef = jniTransaction.Add(cache.ReloadClass(jClass));
			return throwNew(cache.Reference, classRef, (ReadOnlyValPtr<Byte>)messageMem.Pointer);
		}
		/// <summary>
		/// Creates a <see cref="IFixedContext{T}.IDisposable"/> instance from an span created in stack.
		/// </summary>
		/// <typeparam name="T">Type of elements in span.</typeparam>
		/// <param name="stackSpan">A stack created span.</param>
		/// <param name="cache">Instance to free stack bytes.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private static IFixedContext<T>.IDisposable AllocToFixedContext<T>(scoped Span<T> stackSpan,
			EnvironmentCache? cache = default) where T : unmanaged
		{
			StackDisposable? disposable =
				cache is not null ? new(cache, stackSpan.Length * NativeUtilities.SizeOf<T>()) : default;
			ValPtr<T> ptr = (ValPtr<T>)stackSpan.GetUnsafeIntPtr();
			return ptr.GetUnsafeFixedContext(stackSpan.Length, disposable);
		}
		/// <summary>
		/// Creates a <see cref="IFixedContext{T}.IDisposable"/> instance from an span created in heap.
		/// </summary>
		/// <typeparam name="T">Type of elements in span.</typeparam>
		/// <param name="count">Number of allocated elements.</param>
		/// <returns>A <see cref="IFixedContext{T}.IDisposable"/> instance</returns>
		private static IFixedContext<T>.IDisposable AllocToFixedContext<T>(Int32 count) where T : unmanaged
			=> count == 0 ? ValPtr<T>.Zero.GetUnsafeFixedContext(0) : new T[count].AsMemory().GetFixedContext();
		/// <summary>
		/// Traces the assignment of a value to a primitive field.
		/// </summary>
		/// <param name="jLocal">Field instance object class.</param>
		/// <param name="jClass">Field declaring class.</param>
		/// <param name="definition">Call definition.</param>
		/// <param name="bytes">Binary span containing value to set to.</param>
		private static void TraceSetPrimitiveField(JLocalObject? jLocal, JClassObject jClass,
			JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			switch (definition.Information[1][0])
			{
				case UnicodePrimitiveSignatures.BooleanSignatureChar:
					JTrace.SetField<JBoolean>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.ByteSignatureChar:
					JTrace.SetField<JByte>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.CharSignatureChar:
					JTrace.SetField<JChar>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.DoubleSignatureChar:
					JTrace.SetField<JDouble>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.FloatSignatureChar:
					JTrace.SetField<JFloat>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.IntSignatureChar:
					JTrace.SetField<JInt>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.LongSignatureChar:
					JTrace.SetField<JLong>(jLocal, jClass, definition, bytes);
					break;
				case UnicodePrimitiveSignatures.ShortSignatureChar:
					JTrace.SetField<JShort>(jLocal, jClass, definition, bytes);
					break;
			}
		}
	}
}