using JThrowableObject = Rxmxnx.JNetInterface.Lang.JThrowableObject;

namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents a JNI call.
/// </summary>
public readonly ref partial struct JniCall
{
	/// <summary>
	/// Current <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._env;
	
	/// <summary>
	/// Builder for <see cref="JniCall"/>
	/// </summary>
	public readonly ref partial struct Builder
	{
		/// <summary>
		/// Appends to current call a <see cref="JLocalObject"/> parameter.
		/// </summary>
		/// <param name="localRef">A parameter <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance from <paramref name="localRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JObjectLocalRef localRef, out JLocalObject jLocal)
		{
			jLocal = this.CreateInitialObject(localRef);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JClassObject"/> parameter.
		/// </summary>
		/// <param name="classRef">A parameter <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance from <paramref name="classRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JClassLocalRef classRef, out JClassObject jClass)
		{
			jClass = this.CreateInitialClass(classRef);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JStringObject"/> parameter.
		/// </summary>
		/// <param name="stringRef">A parameter <see cref="JStringLocalRef"/> reference.</param>
		/// <param name="jString">A <see cref="JStringObject"/> instance from <paramref name="stringRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JStringLocalRef stringRef, out JStringObject jString)
		{
			JClassObject jStringClass = this._call._env.GetClass<JStringObject>();
			jString = new(jStringClass, stringRef);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JThrowableObject"/> parameter.
		/// </summary>
		/// <param name="throwableRef">A parameter <see cref="JThrowableLocalRef"/> reference.</param>
		/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance from <paramref name="throwableRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JThrowableLocalRef throwableRef, out JThrowableObject jThrowable)
		{
			jThrowable = this.CreateInitialObject<JThrowableObject>(throwableRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JBoolean"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JBooleanArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JBoolean}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JBooleanArrayLocalRef arrayRef, out JArrayObject<JBoolean> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JBoolean>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JByte"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JByteArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JByte}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JByteArrayLocalRef arrayRef, out JArrayObject<JByte> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JByte>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JChar"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JCharArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JChar}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JCharArrayLocalRef arrayRef, out JArrayObject<JChar> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JChar>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JDouble"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JDoubleArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JDouble}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JDoubleArrayLocalRef arrayRef, out JArrayObject<JDouble> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JDouble>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JFloat"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JFloatArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JFloat}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JFloatArrayLocalRef arrayRef, out JArrayObject<JFloat> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JFloat>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JInt"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JIntArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JInt}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JIntArrayLocalRef arrayRef, out JArrayObject<JInt> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JInt>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JLong"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JLongArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JLong}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JLongArrayLocalRef arrayRef, out JArrayObject<JLong> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JLong>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JShort"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JShortArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JShort}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JLongArrayLocalRef arrayRef, out JArrayObject<JShort> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JShort>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JLocalObject"/> array parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JObjectArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{JLocalObject}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JObjectArrayLocalRef arrayRef, out JArrayObject<JLocalObject> jArray)
		{
			jArray = this.CreateInitialObject<JArrayObject<JLocalObject>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <typeparamref name="TObject"/> parameter.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="localRef">A parameter <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">A <typeparamref name="TObject"/> instance from <paramref name="localRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter<TObject>(JObjectLocalRef localRef, out TObject jLocal)
			where TObject : JLocalObject, IReferenceType<TObject>
		{
			Type typeofT = typeof(TObject);
			if (typeofT == typeof(JLocalObject))
			{
				Unsafe.SkipInit(out jLocal);
				return this.WithParameter(localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
			}
			if (typeofT == typeof(JLocalObject))
			{
				Unsafe.SkipInit(out jLocal);
				JClassLocalRef classRef = NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
				return this.WithParameter(classRef, out Unsafe.As<TObject, JClassObject>(ref jLocal));
			}
			jLocal = this.CreateInitialObject<TObject>(localRef);
			return this;
		}
		/// <summary>
		/// Appends to current call a <typeparamref name="TObject"/> array parameter.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="arrayRef">A parameter <see cref="JObjectArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{TElement}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter<TObject>(JObjectArrayLocalRef arrayRef, out JArrayObject<TObject> jArray)
			where TObject : JLocalObject, IReferenceType<TObject>
		{
			jArray = this.CreateInitialObject<JArrayObject<TObject>>(arrayRef.Value);
			return this;
		}
		/// <summary>
		/// Appends to current call a <typeparamref name="TElement"/> array parameter.
		/// </summary>
		/// <typeparam name="TElement">A <see cref="IDataType"/> type.</typeparam>
		/// <param name="arrayRef">A parameter <see cref="JArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject{TElement}"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter<TElement>(JArrayLocalRef arrayRef, out JArrayObject<TElement> jArray)
			where TElement : IObject, IDataType<TElement>
		{
			jArray = this.CreateInitialObject<JArrayObject<TElement>>(arrayRef.Value);
			return this;
		}
		
        /// <summary>
		/// Retrieves current <see cref="JniCall"/> instance.
		/// </summary>
		/// <returns>A <see cref="JniCall"/> instance.</returns>
		public JniCall Build() => this._call;
	}
}
