namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JniCall
{
	public readonly ref partial struct Builder
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="call">Current <see cref="JniCall"/> instance.</param>
		internal Builder(JniCall call) => this._call = call;

		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.
		/// </summary>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <returns>Initial <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.</returns>
		internal JLocalObject CreateInitialObject(JObjectLocalRef localRef)
		{
			JEnvironment env = this._call._env;
			JClassObject jClass = this.GetObjectClass(localRef);
			if (!jClass.Name.AsSpan().SequenceEqual(env.ClassObject.Name))
			{
				JLocalObject result = new(jClass, localRef);
				this._call._cache[localRef] = result.Lifetime;
				return result;
			}
			JClassLocalRef classRef = NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
			return this.CreateInitialClass(classRef);
		}
		/// <summary>
		/// Retrieves initial <see cref="JLocalObject"/> instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>Initial <see cref="JClassObject"/> instance for <paramref name="classRef"/>.</returns>
		internal JClassObject CreateInitialClass(JClassLocalRef classRef)
		{
			JEnvironment env = this._call._env;
			JClassObject result = env.GetClass(classRef, true);
			result.SetValue(classRef);
			this._call._cache[classRef.Value] = result.Lifetime;
			return result;
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

			JLocalObject jLocalTemp = this.CreateInitialObject(localRef);
			try
			{
				return (TObject)metadata.ParseInstance(jLocalTemp);
			}
			finally
			{
				if (jLocalTemp is not JClassObject) jLocalTemp.Dispose();
			}
		}
	}
}