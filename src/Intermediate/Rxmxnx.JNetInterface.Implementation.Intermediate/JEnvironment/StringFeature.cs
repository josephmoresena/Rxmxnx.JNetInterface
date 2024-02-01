namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record JEnvironmentCache : IStringFeature
	{
		public JStringObject Create(ReadOnlySpan<Char> data)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(data.WithSafeFixed(this, JEnvironmentCache.CreateString));
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, data.ToString()));
		}
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef =
				jniTransaction.Add(utf8Data.WithSafeFixed(this, JEnvironmentCache.CreateUtf8String));
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, utf8Data.Length));
		}
		public Int32 GetLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringLengthDelegate getStringLength = this.GetDelegate<GetStringLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public Int32 GetUtf8Length(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringUtfLengthDelegate getStringUtf8Length = this.GetDelegate<GetStringUtfLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringUtf8Length(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public INativeMemoryHandle GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryHandle(jString, referenceKind, false);
		}
		public INativeMemoryHandle GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryHandle(jString, referenceKind, default);
		}
		public INativeMemoryHandle GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryHandle(jString, referenceKind, true);
		}
		public ReadOnlyValPtr<Char> GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			GetStringCharsDelegate getStringChars = this.GetDelegate<GetStringCharsDelegate>();
			ReadOnlyValPtr<Char> result = getStringChars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == ReadOnlyValPtr<Char>.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public ReadOnlyValPtr<Byte> GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			GetStringUtfCharsDelegate getStringUtf8Chars = this.GetDelegate<GetStringUtfCharsDelegate>();
			ReadOnlyValPtr<Byte> result = getStringUtf8Chars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == ReadOnlyValPtr<Byte>.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public ReadOnlyValPtr<Char> GetCriticalSequence(JStringLocalRef stringRef)
		{
			GetStringCriticalDelegate getStringCritical = this.GetDelegate<GetStringCriticalDelegate>();
			ReadOnlyValPtr<Char> result = getStringCritical(this.Reference, stringRef, out _);
			if (result == ReadOnlyValPtr<Char>.Zero) this.CheckJniError();
			return result;
		}
		public void ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
			releaseStringChars(this.Reference, stringRef, pointer);
			this.CheckJniError();
		}
		public void ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
		{
			ReleaseStringUtfCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringUtfCharsDelegate>();
			releaseStringChars(this.Reference, stringRef, pointer);
			this.CheckJniError();
		}
		public void ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			ReleaseStringCriticalDelegate releaseStringCritical = this.GetDelegate<ReleaseStringCriticalDelegate>();
			releaseStringCritical(this.Reference, stringRef, pointer);
			this.CheckJniError();
		}
		public void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			chars.WithSafeFixed((this, jString, startIndex), JEnvironmentCache.GetStringRegion);
			this.CheckJniError();
		}
		public void GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex = 0)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringUtfRegionDelegate getStringUtfRegion = this.GetDelegate<GetStringUtfRegionDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			using IFixedContext<Byte>.IDisposable fixedMemory = utf8Units.GetFixedContext();
			getStringUtfRegion(this.Reference, stringRef, startIndex, utf8Units.Length,
			                   (ValPtr<Byte>)fixedMemory.Pointer);
			this.CheckJniError();
		}

		private static JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, JEnvironmentCache cache)
		{
			NewStringDelegate newString = cache.GetDelegate<NewStringDelegate>();
			JStringLocalRef result = newString(cache.Reference, (ReadOnlyValPtr<Char>)ctx.Pointer, ctx.Values.Length);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
		private static JStringLocalRef CreateUtf8String(in IReadOnlyFixedContext<Byte> ctx, JEnvironmentCache cache)
		{
			NewStringUtfDelegate newUtf8String = cache.GetDelegate<NewStringUtfDelegate>();
			JStringLocalRef result = newUtf8String(cache.Reference, (ReadOnlyValPtr<Byte>)ctx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
		private static void GetStringRegion(in IFixedContext<Char> ctx,
			(JEnvironmentCache cache, JStringObject jString, Int32 startIndex) args)
		{
			GetStringRegionDelegate getStringRegion = args.cache.GetDelegate<GetStringRegionDelegate>();
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(args.jString);
			getStringRegion(args.cache.Reference, stringRef, args.startIndex, ctx.Values.Length,
			                (ValPtr<Char>)ctx.Pointer);
		}
	}
}