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
			JClassObject jClass = this.GetObjectClass(localRef, out JReferenceTypeMetadata metadata, true);
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
			{
				JLocalObject result = metadata.CreateInstance(jClass, localRef, true);
				this._callAdapter._cache.RegisterParameter(localRef, result);
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
			where TObject : JReferenceObject, IReferenceType<TObject>
		{
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TObject>();
			if (metadata.Modifier == JTypeModifier.Final) return this.CreateFinalObject<TObject>(localRef);
			if (JLocalObject.IsObjectType<TObject>())
				return (TObject)(Object)this.CreateInitialObject(localRef);
			JClassObject jClass = this.GetObjectClass(localRef, out JReferenceTypeMetadata classMetadata, true);
			JLocalObject jLocal = classMetadata.CreateInstance(jClass, localRef, true);
			return (TObject)metadata.ParseInstance(jLocal, true);
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
			if (classRef == result.LocalReference)
			{
				// Class is owned by this class.
				this._callAdapter._cache.RegisterParameter(classRef, result);
			}
			else
			{
				// Class is not owned by this class. A ClassView is registered instead.
				CallClassView callClassView = new(classRef, result);
				this._callAdapter._cache.RegisterParameter(classRef, callClassView);
			}
			return result;
		}
	}
}