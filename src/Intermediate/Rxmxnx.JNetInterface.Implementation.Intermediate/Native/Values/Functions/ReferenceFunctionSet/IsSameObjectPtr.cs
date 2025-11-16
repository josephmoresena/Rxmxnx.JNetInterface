namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ReferenceFunctionSet
{
	/// <summary>
	/// Pointer to <c>IsSameObject</c> function.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct IsSameObjectPtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef, JBoolean>
			Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef, JBoolean> Unix;
	}
}