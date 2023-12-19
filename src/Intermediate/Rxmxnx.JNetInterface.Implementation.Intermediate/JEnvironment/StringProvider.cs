namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IStringProvider
	{
		public JStringObject Create(ReadOnlySpan<Char> data)
		{
			JStringLocalRef stringRef = data.WithSafeFixed(this, JEnvironmentCache.CreateString);
			IEnvironment env = this._mainClasses.Environment;
			return this.Register<JStringObject>(new(env, stringRef, data.ToString(), false));
		}
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data)
		{
			JStringLocalRef stringRef = utf8Data.WithSafeFixed(this, JEnvironmentCache.CreateUtf8String);
			IEnvironment env = this._mainClasses.Environment;
			return this.Register<JStringObject>(new(env, stringRef, default, false));
		}
		public Int32 GetLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringLengthDelegate getStringLength = this.GetDelegate<GetStringLengthDelegate>();
			Int32 result = getStringLength(this.Reference, jObject.As<JStringLocalRef>());
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public Int32 GetUtf8Length(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfDummy(jObject);
			GetStringUtfLengthDelegate getStringUtf8Length = this.GetDelegate<GetStringUtfLengthDelegate>();
			Int32 result = getStringUtf8Length(this.Reference, jObject.As<JStringLocalRef>());
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public IntPtr GetSequence(JStringObject jString, out Boolean isCopy)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringCharsDelegate getStringChars = this.GetDelegate<GetStringCharsDelegate>();
			IntPtr result = getStringChars(this.Reference, jString.Reference, out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetUtf8Sequence(JStringObject jString, out Boolean isCopy)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringUtfCharsDelegate getStringUtf8Chars = this.GetDelegate<GetStringUtfCharsDelegate>();
			IntPtr result = getStringUtf8Chars(this.Reference, jString.Reference, out Byte isCopyByte);
			if (result == IntPtr.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public IntPtr GetCriticalSequence(JStringObject jString)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			GetStringCriticalDelegate getStringCritical = this.GetDelegate<GetStringCriticalDelegate>();
			IntPtr result = getStringCritical(this.Reference, jString.Reference, out _);
			if (result == IntPtr.Zero) this.CheckJniError();
			return result;
		}
		public void ReleaseSequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
			releaseStringChars(this.Reference, jString.Reference, (ReadOnlyValPtr<Char>)pointer);
			this.CheckJniError();
		}
		public void ReleaseUtf8Sequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
			releaseStringChars(this.Reference, jString.Reference, (ReadOnlyValPtr<Char>)pointer);
			this.CheckJniError();
		}
		public void ReleaseCriticalSequence(JStringObject jString, IntPtr pointer)
		{
			ValidationUtilities.ThrowIfDummy(jString);
			ReleaseStringCriticalDelegate releaseStringCritical = this.GetDelegate<ReleaseStringCriticalDelegate>();
			releaseStringCritical(this.Reference, jString.Reference, (ReadOnlyValPtr<Char>)pointer);
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
			using IFixedContext<Byte>.IDisposable fixedMemory = utf8Units.GetFixedContext();
			getStringUtfRegion(this.Reference, jString.Reference, startIndex, utf8Units.Length,
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
			getStringRegion(args.cache.Reference, args.jString.Reference, args.startIndex, ctx.Values.Length,
			                (ValPtr<Char>)ctx.Pointer);
		}
	}
}