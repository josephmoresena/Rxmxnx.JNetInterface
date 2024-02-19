namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Maximum amount of bytes usable on stack.
		/// </summary>
		private const Int32 MaxStackBytes = 128;

		/// <summary>
		/// JNI Delegate dictionary.
		/// </summary>
		private static readonly Dictionary<Type, Int32> delegateIndex = new()
		{
			{ typeof(DefineClassDelegate), 0 },
			{ typeof(FindClassDelegate), 1 },
			{ typeof(FromReflectedMethodDelegate), 2 },
			{ typeof(FromReflectedFieldDelegate), 3 },
			{ typeof(ToReflectedMethodDelegate), 4 },
			{ typeof(GetSuperclassDelegate), 5 },
			{ typeof(IsAssignableFromDelegate), 6 },
			{ typeof(ToReflectedFieldDelegate), 7 },
			{ typeof(ThrowDelegate), 8 },
			{ typeof(ThrowNewDelegate), 9 },
			{ typeof(ExceptionOccurredDelegate), 10 },
			{ typeof(ExceptionDescribeDelegate), 11 },
			{ typeof(ExceptionClearDelegate), 12 },
			{ typeof(FatalErrorDelegate), 13 },
			{ typeof(PushLocalFrameDelegate), 14 },
			{ typeof(PopLocalFrameDelegate), 15 },
			{ typeof(NewGlobalRefDelegate), 16 },
			{ typeof(DeleteGlobalRefDelegate), 17 },
			{ typeof(DeleteLocalRefDelegate), 18 },
			{ typeof(IsSameObjectDelegate), 19 },
			{ typeof(NewLocalRefDelegate), 20 },
			{ typeof(EnsureLocalCapacityDelegate), 21 },
			{ typeof(NewObjectADelegate), 25 },
			{ typeof(GetObjectClassDelegate), 26 },
			{ typeof(IsInstanceOfDelegate), 27 },
			{ typeof(GetMethodIdDelegate), 28 },
			{ typeof(CallObjectMethodADelegate), 31 },
			{ typeof(CallBooleanMethodADelegate), 34 },
			{ typeof(CallByteMethodADelegate), 37 },
			{ typeof(CallCharMethodADelegate), 40 },
			{ typeof(CallShortMethodADelegate), 43 },
			{ typeof(CallIntMethodADelegate), 46 },
			{ typeof(CallLongMethodADelegate), 49 },
			{ typeof(CallFloatMethodADelegate), 52 },
			{ typeof(CallDoubleMethodADelegate), 55 },
			{ typeof(CallVoidMethodADelegate), 58 },
			{ typeof(CallNonVirtualObjectMethodADelegate), 61 },
			{ typeof(CallNonVirtualBooleanMethodADelegate), 64 },
			{ typeof(CallNonVirtualByteMethodADelegate), 67 },
			{ typeof(CallNonVirtualCharMethodADelegate), 70 },
			{ typeof(CallNonVirtualShortMethodADelegate), 73 },
			{ typeof(CallNonVirtualIntMethodADelegate), 76 },
			{ typeof(CallNonVirtualLongMethodADelegate), 79 },
			{ typeof(CallNonVirtualFloatMethodADelegate), 82 },
			{ typeof(CallNonVirtualDoubleMethodADelegate), 85 },
			{ typeof(CallNonVirtualVoidMethodADelegate), 88 },
			{ typeof(GetFieldIdDelegate), 89 },
			{ typeof(GetObjectFieldDelegate), 90 },
			{ typeof(GetBooleanFieldDelegate), 91 },
			{ typeof(GetByteFieldDelegate), 92 },
			{ typeof(GetCharFieldDelegate), 93 },
			{ typeof(GetShortFieldDelegate), 94 },
			{ typeof(GetIntFieldDelegate), 95 },
			{ typeof(GetLongFieldDelegate), 96 },
			{ typeof(GetFloatFieldDelegate), 97 },
			{ typeof(GetDoubleFieldDelegate), 98 },
			{ typeof(SetObjectFieldDelegate), 99 },
			{ typeof(SetBooleanFieldDelegate), 100 },
			{ typeof(SetByteFieldDelegate), 101 },
			{ typeof(SetCharFieldDelegate), 102 },
			{ typeof(SetShortFieldDelegate), 103 },
			{ typeof(SetIntFieldDelegate), 104 },
			{ typeof(SetLongFieldDelegate), 105 },
			{ typeof(SetFloatFieldDelegate), 106 },
			{ typeof(SetDoubleFieldDelegate), 107 },
			{ typeof(GetStaticMethodIdDelegate), 108 },
			{ typeof(CallStaticObjectMethodADelegate), 111 },
			{ typeof(CallStaticBooleanMethodADelegate), 114 },
			{ typeof(CallStaticByteMethodADelegate), 117 },
			{ typeof(CallStaticCharMethodADelegate), 120 },
			{ typeof(CallStaticShortMethodADelegate), 123 },
			{ typeof(CallStaticIntMethodADelegate), 126 },
			{ typeof(CallStaticLongMethodADelegate), 129 },
			{ typeof(CallStaticFloatMethodADelegate), 132 },
			{ typeof(CallStaticDoubleMethodADelegate), 135 },
			{ typeof(CallStaticVoidMethodADelegate), 138 },
			{ typeof(GetStaticFieldIdDelegate), 139 },
			{ typeof(GetStaticObjectFieldDelegate), 140 },
			{ typeof(GetStaticBooleanFieldDelegate), 141 },
			{ typeof(GetStaticByteFieldDelegate), 142 },
			{ typeof(GetStaticCharFieldDelegate), 143 },
			{ typeof(GetStaticShortFieldDelegate), 144 },
			{ typeof(GetStaticIntFieldDelegate), 145 },
			{ typeof(GetStaticLongFieldDelegate), 146 },
			{ typeof(GetStaticFloatFieldDelegate), 147 },
			{ typeof(GetStaticDoubleFieldDelegate), 148 },
			{ typeof(SetStaticObjectFieldDelegate), 149 },
			{ typeof(SetStaticBooleanFieldDelegate), 150 },
			{ typeof(SetStaticByteFieldDelegate), 151 },
			{ typeof(SetStaticCharFieldDelegate), 152 },
			{ typeof(SetStaticShortFieldDelegate), 153 },
			{ typeof(SetStaticIntFieldDelegate), 154 },
			{ typeof(SetStaticLongFieldDelegate), 155 },
			{ typeof(SetStaticFloatFieldDelegate), 156 },
			{ typeof(SetStaticDoubleFieldDelegate), 157 },
			{ typeof(NewStringDelegate), 158 },
			{ typeof(GetStringLengthDelegate), 159 },
			{ typeof(GetStringCharsDelegate), 160 },
			{ typeof(ReleaseStringCharsDelegate), 161 },
			{ typeof(NewStringUtfDelegate), 162 },
			{ typeof(GetStringUtfLengthDelegate), 163 },
			{ typeof(GetStringUtfCharsDelegate), 164 },
			{ typeof(ReleaseStringUtfCharsDelegate), 165 },
			{ typeof(GetArrayLengthDelegate), 166 },
			{ typeof(NewObjectArrayDelegate), 167 },
			{ typeof(GetObjectArrayElementDelegate), 168 },
			{ typeof(SetObjectArrayElementDelegate), 169 },
			{ typeof(NewBooleanArrayDelegate), 170 },
			{ typeof(NewByteArrayDelegate), 171 },
			{ typeof(NewCharArrayDelegate), 172 },
			{ typeof(NewShortArrayDelegate), 173 },
			{ typeof(NewIntArrayDelegate), 174 },
			{ typeof(NewLongArrayDelegate), 175 },
			{ typeof(NewFloatArrayDelegate), 176 },
			{ typeof(NewDoubleArrayDelegate), 177 },
			{ typeof(GetBooleanArrayElementsDelegate), 178 },
			{ typeof(GetByteArrayElementsDelegate), 179 },
			{ typeof(GetCharArrayElementsDelegate), 180 },
			{ typeof(GetShortArrayElementsDelegate), 181 },
			{ typeof(GetIntArrayElementsDelegate), 182 },
			{ typeof(GetLongArrayElementsDelegate), 183 },
			{ typeof(GetFloatArrayElementsDelegate), 184 },
			{ typeof(GetDoubleArrayElementsDelegate), 185 },
			{ typeof(ReleaseBooleanArrayElementsDelegate), 186 },
			{ typeof(ReleaseByteArrayElementsDelegate), 187 },
			{ typeof(ReleaseCharArrayElementsDelegate), 188 },
			{ typeof(ReleaseShortArrayElementsDelegate), 189 },
			{ typeof(ReleaseIntArrayElementsDelegate), 190 },
			{ typeof(ReleaseLongArrayElementsDelegate), 191 },
			{ typeof(ReleaseFloatArrayElementsDelegate), 192 },
			{ typeof(ReleaseDoubleArrayElementsDelegate), 193 },
			{ typeof(GetBooleanArrayRegionDelegate), 194 },
			{ typeof(GetByteArrayRegionDelegate), 195 },
			{ typeof(GetCharArrayRegionDelegate), 196 },
			{ typeof(GetShortArrayRegionDelegate), 197 },
			{ typeof(GetIntArrayRegionDelegate), 198 },
			{ typeof(GetLongArrayRegionDelegate), 199 },
			{ typeof(GetFloatArrayRegionDelegate), 200 },
			{ typeof(GetDoubleArrayRegionDelegate), 201 },
			{ typeof(SetBooleanArrayRegionDelegate), 202 },
			{ typeof(SetByteArrayRegionDelegate), 203 },
			{ typeof(SetCharArrayRegionDelegate), 204 },
			{ typeof(SetShortArrayRegionDelegate), 205 },
			{ typeof(SetIntArrayRegionDelegate), 206 },
			{ typeof(SetLongArrayRegionDelegate), 207 },
			{ typeof(SetFloatArrayRegionDelegate), 208 },
			{ typeof(SetDoubleArrayRegionDelegate), 209 },
			{ typeof(RegisterNativesDelegate), 210 },
			{ typeof(UnregisterNativesDelegate), 211 },
			{ typeof(MonitorEnterDelegate), 212 },
			{ typeof(MonitorExitDelegate), 213 },
			{ typeof(GetVirtualMachineDelegate), 214 },
			{ typeof(GetStringRegionDelegate), 215 },
			{ typeof(GetStringUtfRegionDelegate), 216 },
			{ typeof(GetPrimitiveArrayCriticalDelegate), 217 },
			{ typeof(ReleasePrimitiveArrayCriticalDelegate), 218 },
			{ typeof(GetStringCriticalDelegate), 219 },
			{ typeof(ReleaseStringCriticalDelegate), 220 },
			{ typeof(NewWeakGlobalRefDelegate), 221 },
			{ typeof(DeleteWeakGlobalRefDelegate), 222 },
			{ typeof(ExceptionCheckDelegate), 223 },
			{ typeof(NewDirectByteBufferDelegate), 224 },
			{ typeof(GetDirectBufferAddressDelegate), 225 },
			{ typeof(GetDirectBufferCapacityDelegate), 226 },
			{ typeof(GetObjectRefTypeDelegate), 227 },
			// JNI 0x00090000
			{ typeof(GetModuleDelegate), 228 },
			// JNI 0x00130000
			{ typeof(IsVirtualThreadDelegate), 229 },
		};

		/// <summary>
		/// Retrieves the <see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.
		/// </summary>
		/// <param name="returnType">A <see cref="JClassObject"/> instance.</param>
		/// <returns><see cref="IReflectionMetadata"/> instance for <paramref name="returnType"/>.</returns>
		private static IReflectionMetadata? GetReflectionMetadata(JClassObject returnType)
		{
			using JStringObject className = InternalFunctionCache.Instance.GetClassName(returnType);
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
				using JStringObject className = InternalFunctionCache.Instance.GetClassName(jClass);
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
		/// Retrieves throwable message.
		/// </summary>
		/// <param name="jClass">Throwable class.</param>
		/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
		/// <returns>Throwable message.</returns>
		private static String GetThrowableMessage(JClassObject jClass, JThrowableLocalRef throwableRef)
		{
			using JStringObject throwableMessage = JThrowableObject.GetThrowableMessage(jClass, throwableRef);
			return throwableMessage.Value;
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
	}
}