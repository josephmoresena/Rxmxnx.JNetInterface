namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
	private sealed partial class EnvironmentCache
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
		/// <param name="isReferenceType">Indicates whether class reference is from a reference type.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference, Boolean isReferenceType)
		{
			using JStringObject jString = this.GetClassName(classRef, isReferenceType, out Boolean isPrimitive);
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
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		/// <param name="frame">A <see cref="LocalFrame"/> instance.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		public void LoadClass(LocalFrame frame, JClassLocalRef classRef, JClassObject jClass)
		{
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
			if (classRef != jClass.LocalReference) return;
			JTrace.RegisterObject(jClass, frame.Id, frame.Name);
			frame[classRef.Value] = jClass.Lifetime.GetCacheable();
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="namePtr"/> as class name.
		/// </summary>
		/// <param name="namePtr">A pointer to class name.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public unsafe JClassLocalRef FindClass(Byte* namePtr)
		{
			ref readonly NativeInterface nativeInterface =
				ref this.GetNativeInterface<NativeInterface>(NativeInterface.FindClassInfo);
			JClassLocalRef result = nativeInterface.ClassFunctions.FindClass(this.Reference, namePtr);
			if (result.IsDefault) this.CheckJniError();
			return result;
		}
		/// <summary>
		/// Retrieves class element from interfaces class array.
		/// </summary>
		/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
		/// <param name="index">Element index.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetInterfaceClass(JArrayLocalRef arrayRef, Int32 index)
		{
			JObjectLocalRef localRef =
				this.GetObjectArrayElement(JObjectArrayLocalRef.FromReference(in arrayRef), index);
			return this.AsClassObject(JClassLocalRef.FromReference(in localRef), true);
		}
	}
}