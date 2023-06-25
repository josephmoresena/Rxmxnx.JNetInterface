﻿namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI native value.
/// </summary>
public interface INative
{
	/// <summary>
	/// Current instance text value.
	/// </summary>
	internal String TextValue
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this switch
			{
				IFixedPointer fPtr => fPtr.Pointer.ToString(CommonConstants.IntPtrToStringFormat),
				JValue jValue => Convert.ToHexString(NativeUtilities.AsBytes(jValue)),
				JNativeInterface jNative => Convert.ToHexString(NativeUtilities.AsBytes(jNative)),
				JInvokeInterface jInvoke => Convert.ToHexString(NativeUtilities.AsBytes(jInvoke)),
				_ => this.ToString()!
			};
		}
	}

	/// <summary>
	/// Indicates the native type.
	/// </summary>
	static abstract JNativeType Type { get; }

	/// <summary>
	/// Current value as <see cref="String"/>.
	/// </summary>
	/// <returns>Current instance as <see cref="String"/>.</returns>
	String AsString();

	/// <summary>
	/// <paramref name="native"/> as <see cref="String"/>.
	/// </summary>
	/// <param name="native"><see cref="INative"/> instance.</param>
	/// <returns><see cref="INative"/> instance as <see cref="String"/>.</returns>
	internal static String ToString(INative native)
	{
		return native.AsString();
	}
}

/// <summary>
/// This interface exposes a JNI native value.
/// </summary>
internal interface INative<TSelf> : INative
	where TSelf : unmanaged, INative<TSelf>
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	String INative.AsString()
	{
		return String.Format(CommonConstants.NativeReferenceFormat, TSelf.Type.GetTypeName(), this.TextValue);
	}
}