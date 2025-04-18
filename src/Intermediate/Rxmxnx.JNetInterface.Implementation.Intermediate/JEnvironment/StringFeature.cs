namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache : IStringFeature
	{
		public JStringObject Create(ReadOnlySpan<Char> data, String? value = default)
		{
			JStringLocalRef stringRef = this.CreateString(data);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, value, data.Length));
		}
		public JStringObject Create(ReadOnlySpan<Byte> utf8Data)
		{
			JStringLocalRef stringRef = this.CreateUtf8String(utf8Data);
			JClassObject jStringClass = this.GetClass<JStringObject>();
			return this.Register<JStringObject>(new(jStringClass, stringRef, utf8Data.Length));
		}
		public Int32 GetLength(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringLengthInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = nativeInterface.StringFunctions.Utf16.GetStringLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public Int32 GetUtf8Length(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringUtfLengthInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int32 result = nativeInterface.StringFunctions.Utf8.GetStringLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public unsafe Int64? GetUtf8LongLength(JReferenceObject jObject)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			if (this.Version < NativeInterface24.RequiredVersion) return default;

			ref readonly NativeInterface24 nativeInterface =
				ref this.GetNativeInterface<NativeInterface24>(NativeInterface24.GetStringUtfLongLengthInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add<JStringLocalRef>(jObject);
			Int64 result = nativeInterface.GetStringUtfLongLength(this.Reference, stringRef);
			if (result <= 0) this.CheckJniError();
			return result;
		}
		public INativeMemoryAdapter GetSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jString);
			ImplementationValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, false);
		}
		public ReadOnlyValPtr<Char> GetSequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringCharsInfo);
			ReadOnlyValPtr<Char> result =
				nativeInterface.StringFunctions.Utf16.GetStringChars(this.Reference, stringRef, out JBoolean isCopyJ);
			if (result == ReadOnlyValPtr<Char>.Zero) this.CheckJniError();
			isCopy = isCopyJ.Value;
			return result;
		}
		public INativeMemoryAdapter GetUtf8Sequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jString);
			ImplementationValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, default);
		}
		public ReadOnlyValPtr<Byte> GetUtf8Sequence(JStringLocalRef stringRef, out Boolean isCopy)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringUtfCharsInfo);
			ReadOnlyValPtr<Byte> result =
				nativeInterface.StringFunctions.Utf8.GetStringChars(this.Reference, stringRef, out JBoolean isCopyJ);
			if (result == ReadOnlyValPtr<Byte>.Zero) this.CheckJniError();
			isCopy = isCopyJ.Value;
			return result;
		}
		public INativeMemoryAdapter GetCriticalSequence(JStringObject jString, JMemoryReferenceKind referenceKind)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jString);
			ImplementationValidationUtilities.ThrowIfDefault(jString);
			return this.VirtualMachine.CreateMemoryAdapter(jString, referenceKind, true);
		}
		public unsafe ReadOnlyValPtr<Char> GetCriticalSequence(JStringLocalRef stringRef)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringCriticalInfo);
			ReadOnlyValPtr<Char> result =
				nativeInterface.StringCriticalFunctions.GetStringCritical(this.Reference, stringRef, out _);
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
					ref readonly NativeInterface nativeInterface =
						ref this.GetNativeInterface<NativeInterface>(NativeInterface.ReleaseStringCharsInfo);
					nativeInterface.StringFunctions.Utf16.ReleaseStringChars(this.Reference, stringRef, pointer);
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
					ref readonly NativeInterface nativeInterface =
						ref this.GetNativeInterface<NativeInterface>(NativeInterface.ReleaseStringUtfCharsInfo);
					nativeInterface.StringFunctions.Utf8.ReleaseStringChars(this.Reference, stringRef, pointer);
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
		public unsafe void ReleaseCriticalSequence(JStringLocalRef stringRef, ReadOnlyValPtr<Char> pointer)
		{
			try
			{
				if (this._env.IsAttached && this.VirtualMachine.IsAlive)
				{
					ref readonly NativeInterface nativeInterface =
						ref this.GetNativeInterface<NativeInterface>(NativeInterface.ReleaseStringUtfCharsInfo);
					nativeInterface.StringCriticalFunctions.ReleaseStringCritical(this.Reference, stringRef, pointer);
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
		public unsafe void GetCopy(JStringObject jString, Span<Char> chars, Int32 startIndex = 0)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jString);
			fixed (Char* ptr = &MemoryMarshal.GetReference(chars))
				this.GetStringRegion(jString, new(ptr), startIndex, chars.Length);
			this.CheckJniError();
		}
		public unsafe void GetUtf8Copy(JStringObject jString, Span<Byte> utf8Units, Int32 startIndex = 0)
		{
			ImplementationValidationUtilities.ThrowIfProxy(jString);
			fixed (Byte* ptr = &MemoryMarshal.GetReference(utf8Units))
				this.GetStringUtf8Region(jString, new(ptr), startIndex, utf8Units.Length);
			this.CheckJniError();
		}
	}
}