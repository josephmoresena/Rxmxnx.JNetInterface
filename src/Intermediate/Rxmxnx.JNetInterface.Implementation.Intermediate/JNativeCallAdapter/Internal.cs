namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JNativeCallAdapter
{
	public readonly ref partial struct Builder
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="callAdapter">Current <see cref="JNativeCallAdapter"/> instance.</param>
		internal Builder(JNativeCallAdapter callAdapter) => this._callAdapter = callAdapter;

		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.</returns>
		internal JLocalObject? CreateInitialObject(JObjectLocalRef localRef)
		{
			if (localRef == default) return default;

			INativeThread env = this._callAdapter._env;
			this._callAdapter._cache.Activate(out LocalCache previous);
			try
			{
				JClassObject jClass = this.GetObjectClass(localRef, out JReferenceTypeMetadata metadata, true);
				if (!jClass.Name.AsSpan().SequenceEqual(env.ClassFeature.ClassObject.Name))
				{
					JLocalObject result = metadata.CreateInstance(jClass, localRef, true);
					this._callAdapter._cache.RegisterAlien(localRef, result);
					return result;
				}
			}
			finally
			{
				env.LocalCache = previous;
			}
			JClassLocalRef classRef = new(localRef);
			return this.CreateInitialClass(classRef);
		}
		/// <summary>
		/// Retrieves initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
#if !NET8_0_OR_GREATER
		[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
		internal TObject? CreateInitialObject<TObject>(JObjectLocalRef localRef)
			where TObject : JReferenceObject, IReferenceType<TObject>
		{
			if (localRef == default) return default;

			JReferenceTypeMetadata typeMetadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TObject>();
			if (typeMetadata.Modifier == JTypeModifier.Final) return this.CreateFinalObject<TObject>(localRef);
			JClassObject jClass = this.GetObjectClass(localRef, out JReferenceTypeMetadata classMetadata, true);
			return (TObject)this.CreateObject(jClass, localRef, classMetadata, typeMetadata);
		}
		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="validateReference">Indicates whether <paramref name="classRef"/> should be validated.</param>
		/// <returns>Initial <see cref="JClassObject"/> instance for <paramref name="classRef"/>.</returns>
		internal JClassObject? CreateInitialClass(JClassLocalRef classRef, Boolean validateReference = false)
		{
			if (classRef == default) return default;

			INativeThread env = this._callAdapter._env;
			if (validateReference) this.ThrowIfNotClassObject(classRef.Value);
			JClassObject result = env.GetReferenceTypeClass(classRef, true);
			if (classRef.Value == result.LocalReference)
				// Class is owned by this call.
				this._callAdapter._cache.RegisterAlien(classRef, result);
			else
				// Class is not owned by this call. A ClassView is registered instead.
				this._callAdapter._cache.RegisterAlien(classRef, new ClassView(classRef, result));
			return result;
		}
	}
}