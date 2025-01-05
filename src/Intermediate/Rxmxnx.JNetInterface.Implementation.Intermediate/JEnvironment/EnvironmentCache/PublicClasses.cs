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
		/// <param name="runtimeInformation">Runtime known type information.</param>
		/// <param name="deleteLocalRef">Indicates whether local class reference should be deleted.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference,
			WellKnownRuntimeTypeInformation runtimeInformation = default, Boolean deleteLocalRef = false)
		{
			Boolean isReferenceType = runtimeInformation.Kind is not null and not JTypeKind.Primitive;
			using JStringObject jString = this.GetClassName(classRef, isReferenceType, out Boolean isPrimitive);
			try
			{
				using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
				JClassLocalRef usableClassRef = keepReference ? classRef : default;
				JClassObject jClass = isPrimitive ?
					this.GetPrimitiveClass(utf8Text.Values) :
					this.GetClass(utf8Text.Values, usableClassRef, runtimeInformation);
				if (JVirtualMachine.IsMainClass(jClass.Hash))
					this.LoadMainClass(jClass, classRef, deleteLocalRef);
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
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="className"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="signature">Class JNI signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public unsafe JClassLocalRef FindMainClass(CString className, CString signature)
		{
			JClassLocalRef classRef;
			fixed (Byte* ptr = &MemoryMarshal.GetReference(className.AsSpan()))
			{
				JTrace.FindClass(className);
				classRef = this.FindClass(ptr, true);
			}
			if (!classRef.IsDefault) return classRef;

			this._env.DescribeException();
			this.ClearException();
			throw new NotSupportedException(
				$"Main class {ClassNameHelper.GetClassName(signature)} is not available for JNI access.");
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
			return this.AsClassObject(JClassLocalRef.FromReference(in localRef), JTypeKind.Interface);
		}
	}
}