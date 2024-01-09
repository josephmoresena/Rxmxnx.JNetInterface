namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record JEnvironmentCache : IStringFeature
	{
		public JStringObject Create(ReadOnlySpan<Char> data)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(data.WithSafeFixed(this, JEnvironmentCache.CreateString));
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, data.ToString()));
		}
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data)
		{
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef =
				jniTransaction.Add(utf8Data.WithSafeFixed(this, JEnvironmentCache.CreateUtf8String));
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, utf8Data.Length));
		}
		public Int32 GetLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringLengthDelegate getStringLength = this.GetDelegate<GetStringLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public Int32 GetUtf8Length(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringUtfLengthDelegate getStringUtf8Length = this.GetDelegate<GetStringUtfLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringUtf8Length(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public IntPtr GetSequence(JStringObject jString, out Boolean isCopy)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringCharsDelegate getStringChars = this.GetDelegate<GetStringCharsDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			IntPtr result = getStringChars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetUtf8Sequence(JStringObject jString, out Boolean isCopy)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringUtfCharsDelegate getStringUtf8Chars = this.GetDelegate<GetStringUtfCharsDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			IntPtr result = getStringUtf8Chars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetCriticalSequence(JStringObject jString)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringCriticalDelegate getStringCritical = this.GetDelegate<GetStringCriticalDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			IntPtr result = getStringCritical(this.Reference, stringRef, out _);
			if (result == IntPtr.Zero) this.CheckJniError();
			return result;
		}
		public void ReleaseSequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			releaseStringChars(this.Reference, stringRef, (ReadOnlyValPtr<Char>)pointer);
			this.CheckJniError();
		}
		public void ReleaseUtf8Sequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			releaseStringChars(this.Reference, stringRef, (ReadOnlyValPtr<Char>)pointer);
			this.CheckJniError();
		}
		public void ReleaseCriticalSequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCriticalDelegate releaseStringCritical = this.GetDelegate<ReleaseStringCriticalDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			releaseStringCritical(this.Reference, stringRef, (ReadOnlyValPtr<Char>)pointer);
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
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateUnaryTransaction();
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
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateUnaryTransaction();
			JStringLocalRef stringRef = jniTransaction.Add(args.jString);
			getStringRegion(args.cache.Reference, stringRef, args.startIndex, ctx.Values.Length,
			                (ValPtr<Char>)ctx.Pointer);
		}
	}
}