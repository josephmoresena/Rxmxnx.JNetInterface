namespace Rxmxnx.JNetInterface.Native.Values.Functions;

internal readonly partial struct ReferenceFunctionSet
{
	/// <summary>
	/// Set of function pointers to manage JNI reference frame through JNI.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private readonly unsafe struct FrameFunctionSet
	{
		/// <summary>
		/// Function set for Windows Operating System.
		/// </summary>
		[FieldOffset(0)]
		private readonly Windows _windows;
		/// <summary>
		/// Function set for Unix-like Operating System.
		/// </summary>
		[FieldOffset(0)]
		private readonly Unix _unix;

		/// <summary>
		/// <c>PushLocalFrame</c>.
		/// </summary>
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
			=> OperatingSystem.IsWindows() ?
				this._windows.PushLocalFrame(envRef, capacity) :
				this._unix.PushLocalFrame(envRef, capacity);
		/// <summary>
		/// <c>PopLocalFrame</c>.
		/// </summary>
#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
			=> OperatingSystem.IsWindows() ?
				this._windows.PopLocalFrame(envRef, localRef) :
				this._unix.PopLocalFrame(envRef, localRef);

		/// <summary>
		/// Windows function set.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private readonly struct Windows
		{
			/// <summary>
			/// Pointer to <c>PushLocalFrame</c> function.
			/// Creates a new local reference frame, in which at least a given number of local references can be created.
			/// </summary>
			/// <remarks>
			/// Note that local references already created in previous local frames are still valid in the current local frame.
			/// </remarks>
			public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, Int32, JResult> PushLocalFrame;
			/// <summary>
			/// Pointer to <c>PopLocalFrame</c> function.
			/// Pops off the current local reference frame, frees all the local references, and returns a
			/// local reference in the previous local reference frame for the given result object.
			/// </summary>
			public readonly delegate* unmanaged[Stdcall]<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef>
				PopLocalFrame;
		}

		/// <summary>
		/// Unix function set.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private readonly struct Unix
		{
			/// <summary>
			/// Pointer to <c>PushLocalFrame</c> function.
			/// Creates a new local reference frame, in which at least a given number of local references can be created.
			/// </summary>
			/// <remarks>
			/// Note that local references already created in previous local frames are still valid in the current local frame.
			/// </remarks>
			public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, Int32, JResult> PushLocalFrame;
			/// <summary>
			/// Pointer to <c>PopLocalFrame</c> function.
			/// Pops off the current local reference frame, frees all the local references, and returns a
			/// local reference in the previous local reference frame for the given result object.
			/// </summary>
			public readonly delegate* unmanaged[Cdecl]<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef> PopLocalFrame;
		}
	}
}