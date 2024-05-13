namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache : IStringFeature
	{
		public JStringObject Create(ReadOnlySpan<Char> data)
		{
			JStringLocalRef stringRef = data.WithSafeFixed(this, EnvironmentCache.CreateString);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, data.ToString()));
		}
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data)
		{
			JStringLocalRef stringRef = utf8Data.WithSafeFixed(this, EnvironmentCache.CreateUtf8String);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, utf8Data.Length));
		}
		public Int32 GetLength(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			GetStringLengthDelegate getStringLength = this.GetDelegate<GetStringLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public Int32 GetUtf8Length(JReferenceObject jObject)
		{
			ValidationUtilities.ThrowIfProxy(jObject);
			GetStringUtfLengthDelegate getStringUtf8Length = this.GetDelegate<GetStringUtfLengthDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = getStringUtf8Length(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfProxy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, false);
		}
		public ReadOnlyValPtr<Char> GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			GetStringCharsDelegate getStringChars = this.GetDelegate<GetStringCharsDelegate>();
			ReadOnlyValPtr<Char> result = getStringChars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == ReadOnlyValPtr<Char>.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfProxy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, default);
		}
		public ReadOnlyValPtr<Byte> GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			GetStringUtfCharsDelegate getStringUtf8Chars = this.GetDelegate<GetStringUtfCharsDelegate>();
			ReadOnlyValPtr<Byte> result = getStringUtf8Chars(this.Reference, stringRef, out Byte isCopyByte);
			if (result == ReadOnlyValPtr<Byte>.Zero) this.CheckJniError();
			isCopy = isCopyByte == JBoolean.TrueValue;
			return result;
		}
		public INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ValidationUtilities.ThrowIfProxy(jString);
			ValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, true);
		}
		public ReadOnlyValPtr<Char> GetCriticalSequence(JStringLocalRef stringRef)
		{
			GetStringCriticalDelegate getStringCritical = this.GetDelegate<GetStringCriticalDelegate>();
			ReadOnlyValPtr<Char> result = getStringCritical(this.Reference, stringRef, out _);
			if (result == ReadOnlyValPtr<Char>.Zero) this.CheckJniError();
			this._criticalCount++;
			return result;
		}
		public void ReleaseSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					ReleaseStringCharsDelegate releaseStringChars = this.GetDelegate<ReleaseStringCharsDelegate>();
					releaseStringChars(this.Reference, stringRef, pointer);
					this.CheckJniError();
				}
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, true, stringRef,
				                     pointer);
			}
			catch (Exception)
			{
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, false, stringRef,
				                     pointer);
				throw;
			}
		}
		public void ReleaseUtf8Sequence(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> pointer)
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					ReleaseStringUtfCharsDelegate releaseStringChars =
						this.GetDelegate<ReleaseStringUtfCharsDelegate>();
					releaseStringChars(this.Reference, stringRef, pointer);
					this.CheckJniError();
				}
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, true, stringRef,
				                     pointer);
			}
			catch (Exception)
			{
				JTrace.ReleaseMemory(false, this._env.IsAttached, this.VirtualMachine.IsAlive, false, stringRef,
				                     pointer);
				throw;
			}
		}
		public void ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					ReleaseStringCriticalDelegate releaseStringCritical =
						this.GetDelegate<ReleaseStringCriticalDelegate>();
					releaseStringCritical(this.Reference, stringRef, pointer);
					this.CheckJniError();
					this._criticalCount--;
				}
				JTrace.ReleaseMemory(true, this._env.IsAttached, this.VirtualMachine.IsAlive, true, stringRef, pointer);
			}
			catch (Exception)
			{
				JTrace.ReleaseMemory(true, this._env.IsAttached, this.VirtualMachine.IsAlive, false, stringRef,
				                     pointer);
				throw;
			}
		}
		public void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0)
		{
			ValidationUtilities.ThrowIfProxy(jString);
			chars.WithSafeFixed((this, jString, startIndex), EnvironmentCache.GetStringRegion);
			this.CheckJniError();
		}
		public void GetCopyUtf8(JStringObject jString, Memory<Byte> utf8Units, Int32 startIndex = 0)
		{
			ValidationUtilities.ThrowIfProxy(jString);
			GetStringUtfRegionDelegate getStringUtfRegion = this.GetDelegate<GetStringUtfRegionDelegate>();
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			using IFixedContext<Byte>.IDisposable fixedMemory = utf8Units.GetFixedContext();
			getStringUtfRegion(this.Reference, stringRef, startIndex, utf8Units.Length,
			                   (ValPtr<Byte>)fixedMemory.Pointer);
			this.CheckJniError();
		}
	}
}