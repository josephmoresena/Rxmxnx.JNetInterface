namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ObjectFunctionSet
{
	/// <summary>
	/// Pointer to <c>IsInstanceOf</c> function.
	/// Tests whether an object is an instance of a class.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct IsInstanceOfPtr
	{
		/// <summary>
		/// Function pointer for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JBoolean>
			Windows;
		/// <summary>
		/// Function pointer for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JBoolean> Unix;
	}
}