namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Class cache cache.
		/// </summary>
		public ClassCache<JClassObject> GetClassCache() => this._classes;
		/// <summary>
		/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference)
		{
			using JStringObject jString = this.GetClassName(classRef, out Boolean isPrimitive);
			try
			{
				using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
				JClassLocalRef usableClassRef = keepReference ? classRef : default;
				JClassObject jClass = isPrimitive ?
					this.GetPrimitiveClass(utf8Text.Values) :
					this.GetClass(utf8Text.Values, usableClassRef);
				return jClass;
			}
			finally
			{
				this.FreeUnregistered(jString);
			}
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		public void LoadClass(JClassObject? jClass)
		{
			if (jClass is null) return;
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
		}

		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="classNameCtx"/> as class name.
		/// </summary>
		/// <param name="classNameCtx">A <see cref="IReadOnlyFixedMemory"/> instance.</param>
		/// <param name="cache">Current <see cref="EnvironmentCache"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public static unsafe JClassLocalRef FindClass(in IReadOnlyFixedMemory classNameCtx, EnvironmentCache cache)
		{
			ref readonly NativeInterface nativeInterface =
				ref cache.GetNativeInterface<NativeInterface>(NativeInterface.FindClassInfo);
			JClassLocalRef result =
				nativeInterface.ClassFunctions.FindClass(cache.Reference, (ReadOnlyValPtr<Byte>)classNameCtx.Pointer);
			if (result.IsDefault) cache.CheckJniError();
			return result;
		}
	}
}