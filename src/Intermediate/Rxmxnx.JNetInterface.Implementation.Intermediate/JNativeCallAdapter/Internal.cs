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
		internal JLocalObject CreateInitialObject(JObjectLocalRef localRef)
		{
			JEnvironment env = this._callAdapter._env;
			JClassObject jClass = this.GetObjectClass(localRef, true);
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
			{
				JReferenceTypeMetadata metadata = MetadataHelper.GetMetadata(jClass.Hash) ??
					(JReferenceTypeMetadata)MetadataHelper.GetMetadata<JLocalObject>();
				JLocalObject result = metadata.CreateInstance(jClass, localRef, true);
				this._callAdapter._cache[localRef] = result.Lifetime;
				return result;
			}
			JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
			return this.CreateInitialClass(classRef);
		}
		/// <summary>
		/// Retrieves initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
		internal TObject CreateInitialObject<TObject>(JObjectLocalRef localRef)
			where TObject : JLocalObject, IReferenceType<TObject>
		{
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetMetadata<TObject>();
			if (metadata.Modifier == JTypeModifier.Final) return this.CreateFinalObject<TObject>(localRef);
			if (JLocalObject.IsObjectType<TObject>())
				return (TObject)this.CreateInitialObject(localRef);
			JClassObject jClass = this.GetObjectClass(localRef, true);
			return (TObject)metadata.CreateInstance(jClass, localRef, true);
		}
		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="validateReference">Indicates whether <paramref name="classRef"/> should be validated.</param>
		/// <returns>Initial <see cref="JClassObject"/> instance for <paramref name="classRef"/>.</returns>
		internal JClassObject CreateInitialClass(JClassLocalRef classRef, Boolean validateReference = false)
		{
			JEnvironment env = this._callAdapter._env;
			if (validateReference) this.ThrowIfNotClassObject(classRef.Value);
			JClassObject result = env.GetClass(classRef, true);
			// Check if class reference is owned by this call.
			if (classRef == result.InternalReference)
			{
				this._callAdapter._cache[classRef.Value] = result.Lifetime;
			}
			else
			{
				CallClassView callClassView = new(classRef, result);
				this._callAdapter._cache[classRef.Value] = callClassView.Lifetime;
			}
			return result;
		}
	}
}