namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// This class is a JNI instance created from an invalid <see cref="JVirtualMachine"/> instance.
	/// </summary>
	/// <param name="VirtualMachine">A <see cref="JVirtualMachine"/> instance.</param>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
	                 Justification = CommonConstants.NonStandardTraceJustification)]
	private sealed record DeadThread(JVirtualMachine VirtualMachine)
		: IThread, IReferenceFeature, IStringFeature, IArrayFeature, IClassFeature
	{
		JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length)
			=> DeadThread.ThrowInvalidResult<JArrayObject<TElement>>();
		JArrayObject<TElement> IArrayFeature.CreateArray<TElement>(Int32 length, TElement initialElement)
			=> DeadThread.ThrowInvalidResult<JArrayObject<TElement>>();
		Int32 IArrayFeature.GetArrayLength(JReferenceObject jObject)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine {jObject.As<JObjectLocalRef>()} array length. JVM {this.VirtualMachine.Reference} was destroyed.");
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
		void IArrayFeature.GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, Span<TPrimitive> elements,
			Int32 startIndex)
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IArrayFeature.SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, ReadOnlySpan<TPrimitive> elements,
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
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to release {pointer} memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
		}
		void IArrayFeature.ReleasePrimitiveCriticalSequence(JArrayLocalRef arrayRef, ValPtr<Byte> criticalPtr)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to release {criticalPtr} critical memory from {arrayRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
		}
		JClassObject IClassFeature.VoidPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.BooleanPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.BytePrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.CharPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.DoublePrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.FloatPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.IntPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.LongPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.ShortPrimitive => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.ClassObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.VoidObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.BooleanObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.ByteObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.CharacterObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.DoubleObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.FloatObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.IntegerObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.LongObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.ShortObject => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.AsClassObject(JClassLocalRef classRef)
			=> DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.AsClassObject(JReferenceObject jObject)
			=> DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.GetClass<TDataType>() => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.GetObjectClass(JLocalObject jLocal) => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject? IClassFeature.GetSuperClass(JClassObject jClass)
			=> DeadThread.ThrowInvalidResult<JClassObject?>();
		Boolean IClassFeature.IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
			=> DeadThread.ThrowInvalidResult<Boolean>();
		Boolean IClassFeature.IsInstanceOf(JReferenceObject jObject, JClassObject jClass)
			=> DeadThread.ThrowInvalidResult<Boolean>();
		Boolean IClassFeature.IsInstanceOf<TDataType>(JReferenceObject jObject)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine if {jObject.As<JObjectLocalRef>()} is an instance of {IDataType.GetMetadata<TDataType>().ClassName} class. JVM {this.VirtualMachine.Reference} was destroyed.");
			return jObject is TDataType || jObject.ObjectClassName.AsSpan()
			                                      .SequenceEqual(IDataType.GetMetadata<TDataType>().ClassName);
		}
		JReferenceTypeMetadata? IClassFeature.GetTypeMetadata(JClassObject? jClass)
			=> DeadThread.ThrowInvalidResult<JReferenceTypeMetadata?>();
		JModuleObject? IClassFeature.GetModule(JClassObject jClass) => DeadThread.ThrowInvalidResult<JModuleObject?>();
		void IClassFeature.ThrowNew<TThrowable>(CString? message, Boolean throwException)
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IClassFeature.ThrowNew<TThrowable>(String? message, Boolean throwException)
			=> DeadThread.ThrowInvalidResult<Byte>();
		JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className)
			=> DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.GetClass(String classHash) => DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
			JClassLoaderObject? jClassLoader)
			=> DeadThread.ThrowInvalidResult<JClassObject>();
		JClassObject IClassFeature.
			LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
				ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
			=> DeadThread.ThrowInvalidResult<JClassObject>();
		void IClassFeature.GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
		{
			Unsafe.SkipInit(out name);
			Unsafe.SkipInit(out signature);
			Unsafe.SkipInit(out hash);
			DeadThread.ThrowInvalidResult<Byte>();
		}
		ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
			=> DeadThread.ThrowInvalidResult<ObjectLifetime>();
		JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
			=> DeadThread.ThrowInvalidResult<JLocalObject>();
		void IReferenceFeature.MonitorEnter(JObjectLocalRef localRef)
			=> DeadThread.ThrowInvalidResult<ObjectLifetime>();
		void IReferenceFeature.MonitorExit(JObjectLocalRef localRef)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to exit monitor from {localRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
		}
		TGlobal IReferenceFeature.Create<TGlobal>(JLocalObject jLocal) => DeadThread.ThrowInvalidResult<TGlobal>();
		JWeak IReferenceFeature.CreateWeak(JGlobalBase jGlobal) => DeadThread.ThrowInvalidResult<JWeak>();
		Boolean IReferenceFeature.Unload(JLocalObject jLocal)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to unload {jLocal.LocalReference}. JVM {this.VirtualMachine.Reference} was destroyed.");
			return true;
		}
		Boolean IReferenceFeature.Unload(JGlobalBase jGlobal)
		{
			JGlobalRef? globalRef = (jGlobal as JGlobal)?.Reference;
			JWeakRef? weakRef = (jGlobal as JWeak)?.Reference;
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to unload {globalRef?.ToString() ?? weakRef?.ToString()}. JVM {this.VirtualMachine.Reference} was destroyed.");
			return true;
		}
		Boolean IReferenceFeature.IsParameter(JLocalObject jLocal)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine {jLocal.LocalReference} is parameter. JVM {this.VirtualMachine.Reference} was destroyed.");
			return false;
		}
		IDisposable IReferenceFeature.GetSynchronizer(JReferenceObject jObject)
			=> DeadThread.ThrowInvalidResult<IDisposable>();
		Int32 IStringFeature.GetLength(JReferenceObject jObject)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine {jObject.As<JObjectLocalRef>()} string length. JVM {this.VirtualMachine.Reference} was destroyed.");
			return 0;
		}
		Int32 IStringFeature.GetUtf8Length(JReferenceObject jObject)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine {jObject.As<JObjectLocalRef>()} UTF8 string length. JVM {this.VirtualMachine.Reference} was destroyed.");
			return 0;
		}
		void IStringFeature.GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex)
			=> DeadThread.ThrowInvalidResult<Byte>();
		void IStringFeature.GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex)
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
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
		}
		void IStringFeature.ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to release {pointer} memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
		}
		void IStringFeature.ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to release {pointer} critical memory from {stringRef}. JVM {this.VirtualMachine.Reference} was destroyed.");
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
		IClassFeature IEnvironment.ClassFeature => this;
		IReferenceFeature IEnvironment.ReferenceFeature => this;
		IStringFeature IEnvironment.StringFeature => this;
		IArrayFeature IEnvironment.ArrayFeature => this;
		INioFeature IEnvironment.NioFeature => DeadThread.ThrowInvalidResult<INioFeature>();
		NativeFunctionSet IEnvironment.FunctionSet => DeadThread.ThrowInvalidResult<NativeFunctionSet>();
		Boolean IEnvironment.NoProxy => false;
		Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal) => true;
		JReferenceType IEnvironment.GetReferenceType(JObject jObject)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine reference type from {jObject.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
			return JReferenceType.InvalidRefType;
		}
		Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to determine equality between {jObject.ToTraceText()} and {jObject.ToTraceText()}. JVM {this.VirtualMachine.Reference} was destroyed.");
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
		Boolean? IEnvironment.IsVirtual(JThreadObject jThread) => DeadThread.ThrowInvalidResult<Boolean?>();
		void IDisposable.Dispose()
		{
			if (IVirtualMachine.TraceEnabled)
				Trace.WriteLine(
					$"Unable to destroy a dead JNI instance. JVM {this.VirtualMachine.Reference} was destroyed.");
		}

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <typeparam name="TResult">Type of expected result.</typeparam>
		/// <returns>A <typeparamref name="TResult"/> instance.</returns>
		/// <exception cref="InvalidOperationException">Always throws.</exception>
		private static TResult ThrowInvalidResult<TResult>()
			=> throw new InvalidOperationException(
				"JVM {this.VirtualMachine.Reference} was destroyed. Please create a new one in order to use JNI.");
	}
}