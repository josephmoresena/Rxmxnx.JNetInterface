namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Copies string characters region in <paramref name="charsPtr"/>.
		/// </summary>
		/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
		/// <param name="charsPtr">Buffer memory address.</param>
		/// <param name="startIndex">Region start index.</param>
		/// <param name="length">Number of characters in region.</param>
		private unsafe void GetStringRegion(JStringObject jString, IntPtr charsPtr, Int32 startIndex, Int32 length)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringLengthInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			nativeInterface.StringRegionFunctions.Utf16.GetStringRegion(this.Reference, stringRef, startIndex, length,
			                                                            (ValPtr<Char>)charsPtr);
		}
		/// <summary>
		/// Copies string UTF-8 characters region in <paramref name="unitsPtr"/>.
		/// </summary>
		/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
		/// <param name="unitsPtr">UTF-8 buffer memory address.</param>
		/// <param name="startIndex">Region start index.</param>
		/// <param name="length">Number of UTF-8 characters in region.</param>
		private unsafe void GetStringUtf8Region(JStringObject jString, IntPtr unitsPtr, Int32 startIndex, Int32 length)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.GetStringUtfRegionInfo);
			using INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(jString);
			nativeInterface.StringRegionFunctions.Utf8.GetStringRegion(this.Reference, stringRef, startIndex, length,
			                                                           (ValPtr<Byte>)unitsPtr);
		}
		/// <summary>
		/// Creates a new java string containing <paramref name="chars"/> data.
		/// </summary>
		/// <param name="chars">UTF-16 text.</param>
		/// <returns>A <see cref="JStringLocalRef"/> reference.</returns>
		private unsafe JStringLocalRef CreateString(ReadOnlySpan<Char> chars)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewStringInfo);
			JStringLocalRef result;
			fixed (Char* ptr = &MemoryMarshal.GetReference(chars))
				result = nativeInterface.StringFunctions.NewString(this.Reference, ptr, chars.Length);
			if (result.IsDefault) this.CheckJniError();
			return result;
		}
		/// <summary>
		/// Creates a new java string containing <paramref name="units"/> data.
		/// </summary>
		/// <param name="units">UTF-8 text.</param>
		/// <returns>A <see cref="JStringLocalRef"/> reference.</returns>
		private unsafe JStringLocalRef CreateUtf8String(ReadOnlySpan<Byte> units)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.NewStringUtfInfo);
			JStringLocalRef result;
			fixed (Byte* ptr = &MemoryMarshal.GetReference(units))
				result = nativeInterface.StringFunctions.NewStringUtf(this.Reference, ptr);
			if (result.IsDefault) this.CheckJniError();
			return result;
		}
	}
}