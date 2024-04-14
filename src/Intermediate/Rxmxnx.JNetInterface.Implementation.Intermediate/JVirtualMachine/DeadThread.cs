namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class is a JNI instance created from an invalid <see cref="JVirtualMachine"/> instance.
	/// </summary>
	/// <param name="VirtualMachine">A <see cref="JVirtualMachine"/> instance.</param>
	private sealed record DeadThread(JVirtualMachine VirtualMachine)
		: IThread, IReferenceFeature, IStringFeature, IArrayFeature
	{
		JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length)
			=> DeadThread.ThrowInvalidResult<JArrayObject<TElement>>();
		JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length, TElement initialElement)
			=> DeadThread.ThrowInvalidResult<JArrayObject<TElement>>();
		Int32 IArrayFeature.GetArrayLength(JReferenceObject jObject)
		{
			Debug.WriteLine($"Unable to determine {jObject.As<JObjectLocalRef>()} array length. JVM was destroyed.");
			return 0;
		}
		TElement? IArrayFeature.GetElement<TElement>(JArrayObject<TElement> jArray, Int32 index)
			where TElement : default
			=> DeadThread.ThrowInvalidResult<TElement>();
		void IArrayFeature.SetElement<TElement>(JArrayObject<TElement> jArray, Int32 index, TElement? value)
			where TElement : default
			=> DeadThread.ThrowInvalidResult<Byte>();
		Int32 IArrayFeature.IndexOf<TElement>(JArrayObject<TElement> jArray, TElement? item) where TElement : default
			=> DeadThread.ThrowInvalidResult<Int32>();
		void IArrayFeature.CopyTo<TElement>(JArrayObject<TElement> jArray, TElement?[] array, Int32 arrayIndex)
			where TElement : default
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IArrayFeature.GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Int32 startIndex,
			Memory<TPrimitive> elements)
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IArrayFeature.SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlyMemory<TPrimitive> elements,
			Int32 startIndex)
			=> DeadThread.ThrowInvalidResult<Byte>();
		INativeMemoryAdapter IArrayFeature.GetSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind)
			=> DeadThread.ThrowInvalidResult<INativeMemoryAdapter>();
		INativeMemoryAdapter IArrayFeature.GetCriticalSequence<TPrimitive>(JArrayObject<TPrimitive> jArray,
			JMemoryReferenceKind referenceKind)
			=> DeadThread.ThrowInvalidResult<INativeMemoryAdapter>();
		IntPtr IArrayFeature.GetPrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, out Boolean isCopy)
		{
			Unsafe.SkipInit(out isCopy);
			return DeadThread.ThrowInvalidResult<IntPtr>();
		}
		ValPtr<Byte> IArrayFeature.GetPrimitiveCriticalSequence(JArrayLocalRef arrayRef)
			=> DeadThread.ThrowInvalidResult<ValPtr<Byte>>();
		void IArrayFeature.ReleasePrimitiveSequence<TPrimitive>(JArrayLocalRef arrayRef, IntPtr pointer,
			JReleaseMode mode)
		{
			Debug.WriteLine($"Unable to release {pointer} memory from {arrayRef}. JVM was destroyed.");
		}
		void IArrayFeature.ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
		{
			Debug.WriteLine($"Unable to release {criticalPtr} critical memory from {arrayRef}. JVM was destroyed.");
		}
		ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
			=> DeadThread.ThrowInvalidResult<ObjectLifetime>();
		JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
			=> DeadThread.ThrowInvalidResult<JLocalObject>();
		void IReferenceFeature.MonitorEnter(JObjectLocalRef localRef)
			=> DeadThread.ThrowInvalidResult<ObjectLifetime>();
		void IReferenceFeature.MonitorExit(JObjectLocalRef localRef)
		{
			Debug.WriteLine($"Unable to exit monitor from {localRef}. JVM was destroyed.");
		}
		TGlobal IReferenceFeature.Create<TGlobal>(JLocalObject jLocal) => DeadThread.ThrowInvalidResult<TGlobal>();
		JWeak IReferenceFeature.CreateWeak(JGlobalBase jGlobal) => DeadThread.ThrowInvalidResult<JWeak>();
		Boolean IReferenceFeature.Unload(JLocalObject jLocal)
		{
			Debug.WriteLine($"Unable to unload {jLocal.InternalReference}. JVM was destroyed.");
			return true;
		}
		Boolean IReferenceFeature.Unload(JGlobalBase jGlobal)
		{
			JGlobalRef? globalRef = (jGlobal as JGlobal)?.Reference;
			JWeakRef? weakRef = (jGlobal as JWeak)?.Reference;
			Debug.WriteLine($"Unable to unload {globalRef?.ToString() ?? weakRef?.ToString()}. JVM was destroyed.");
			return true;
		}
		Boolean IReferenceFeature.IsParameter(JLocalObject jLocal)
		{
			Debug.WriteLine($"Unable to determine {jLocal.InternalReference} is parameter. JVM was destroyed.");
			return false;
		}
		IDisposable IReferenceFeature.GetSynchronizer(JReferenceObject jObject)
			=> DeadThread.ThrowInvalidResult<IDisposable>();
		Int32 IStringFeature.GetLength(JReferenceObject jObject)
		{
			Debug.WriteLine($"Unable to determine {jObject.As<JObjectLocalRef>()} string length. JVM was destroyed.");
			return 0;
		}
		Int32 IStringFeature.GetUtf8Length(JReferenceObject jObject)
		{
			Debug.WriteLine(
				$"Unable to determine {jObject.As<JObjectLocalRef>()} UTF8 string length. JVM was destroyed.");
			return 0;
		}
		void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IStringFeature.GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex)
			=> DeadThread.ThrowInvalidResult<Byte>();
		INativeMemoryAdapter IStringFeature.GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
			=> DeadThread.ThrowInvalidResult<INativeMemoryAdapter>();
		INativeMemoryAdapter IStringFeature.GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind)
			=> DeadThread.ThrowInvalidResult<INativeMemoryAdapter>();
		INativeMemoryAdapter IStringFeature.GetCriticalSequence(JStringObject jString,
			JMemoryReferenceKind referenceKind)
			=> DeadThread.ThrowInvalidResult<INativeMemoryAdapter>();
		JStringObject IStringFeature.Create(ReadOnlySpan<Char> data) => DeadThread.ThrowInvalidResult<JStringObject>();
		JStringObject IStringFeature.Create(ReadOnlySpan<Byte> utf8Data)
			=> DeadThread.ThrowInvalidResult<JStringObject>();
		ReadOnlyValPtr<Char> IStringFeature.GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			Unsafe.SkipInit(out isCopy);
			return DeadThread.ThrowInvalidResult<ReadOnlyValPtr<Char>>();
		}
		ReadOnlyValPtr<Byte> IStringFeature.GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			Unsafe.SkipInit(out isCopy);
			return DeadThread.ThrowInvalidResult<ReadOnlyValPtr<Byte>>();
		}
		ReadOnlyValPtr<Char> IStringFeature.GetCriticalSequence(JStringLocalRef stringRef)
			=> DeadThread.ThrowInvalidResult<ReadOnlyValPtr<Char>>();
		void IStringFeature.ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			Debug.WriteLine($"Unable to release {pointer} memory from {stringRef}. JVM was destroyed.");
		}
		void IStringFeature.ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
		{
			Debug.WriteLine($"Unable to release {pointer} memory from {stringRef}. JVM was destroyed.");
		}
		void IStringFeature.ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			Debug.WriteLine($"Unable to release {pointer} critical memory from {stringRef}. JVM was destroyed.");
		}
		Boolean IThread.Attached => false;
		Boolean IThread.Daemon => false;
		CString IThread.Name => CString.Empty;
		JEnvironmentRef IEnvironment.Reference => DeadThread.ThrowInvalidResult<JEnvironmentRef>();
		IVirtualMachine IEnvironment.VirtualMachine => this.VirtualMachine;
		Int32 IEnvironment.Version => DeadThread.ThrowInvalidResult<Int32>();
		Int32? IEnvironment.LocalCapacity
		{
			get => DeadThread.ThrowInvalidResult<Int32>();
			set => _ = DeadThread.ThrowInvalidResult<Int32>() + value;
		}
		ThrowableException? IEnvironment.PendingException
		{
			get => DeadThread.ThrowInvalidResult<ThrowableException?>();
			set => _ = DeadThread.ThrowInvalidResult<ThrowableException?>() ?? value;
		}
		IAccessFeature IEnvironment.AccessFeature => DeadThread.ThrowInvalidResult<IAccessFeature>();
		IClassFeature IEnvironment.ClassFeature => DeadThread.ThrowInvalidResult<IClassFeature>();
		IReferenceFeature IEnvironment.ReferenceFeature => this;
		IStringFeature IEnvironment.StringFeature => this;
		IArrayFeature IEnvironment.ArrayFeature => DeadThread.ThrowInvalidResult<IArrayFeature>();
		INioFeature IEnvironment.NioFeature => DeadThread.ThrowInvalidResult<INioFeature>();
		NativeFunctionSet IEnvironment.FunctionSet => DeadThread.ThrowInvalidResult<NativeFunctionSet>();
		Boolean IEnvironment.NoProxy => false;
		Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal) => true;
		JReferenceType IEnvironment.GetReferenceType(JObject jObject)
		{
			Debug.WriteLine($"Unable to determine reference type from {jObject}. JVM was destroyed.");
			return JReferenceType.InvalidRefType;
		}
		Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
		{
			Debug.WriteLine($"Unable to determine equality between {jObject} and {jObject}. JVM was destroyed.");
			return Object.ReferenceEquals(jObject, jOther);
		}
		Boolean IEnvironment.JniSecure() => true;
		void IEnvironment.WithFrame(Int32 capacity, Action action) => DeadThread.ThrowInvalidResult<Byte>();
		void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
			=> DeadThread.ThrowInvalidResult<Byte>();
		TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func)
			=> DeadThread.ThrowInvalidResult<TResult>();
		TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
			=> DeadThread.ThrowInvalidResult<TResult>();
		void IEnvironment.DescribeException() => DeadThread.ThrowInvalidResult<Byte>();
		void IDisposable.Dispose() { Debug.WriteLine("Unable to destroy a dead JNI instance. JVM was destroyed."); }

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <typeparam name="TResult">Type of expected result.</typeparam>
		/// <returns>A <typeparamref name="TResult"/> instance.</returns>
		/// <exception cref="InvalidOperationException">Always throws.</exception>
		private static TResult ThrowInvalidResult<TResult>()
			=> throw new InvalidOperationException("JVM was destroyed. Please create a new one in order to use JNI.");
	}
}