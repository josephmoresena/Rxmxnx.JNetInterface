namespace Rxmxnx.JNetInterface;

/// <summary>
/// Represents a JNI call adapter.
/// </summary>
public readonly ref partial struct JNativeCallAdapter
{
	/// <summary>
	/// Current <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment => this._env;

	/// <summary>
	/// Finalizes call.
	/// </summary>
	public void FinalizeCall() => this._cache?.Dispose();
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">Primitive result.</param>
	/// <returns><paramref name="result"/>.</returns>
	public TPrimitive FinalizeCall<TPrimitive>(TPrimitive result)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		this.FinalizeCall();
		return result;
	}
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JLocalObject.InterfaceView"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JObjectLocalRef FinalizeCall(JLocalObject.InterfaceView? result)
		=> this.FinalizeCall(ILocalViewObject.GetObject(result));
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JLocalObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JObjectLocalRef FinalizeCall(JLocalObject? result)
	{
		JObjectLocalRef jniResult = default;
		if (result is JClassObject jClass) this._env.ReloadClass(jClass);
		if (result is not null)
		{
			JTrace.FinalizeCall(result);
			jniResult = result.LocalReference;
			if (jniResult == default && result.Reference != default)
				jniResult = result.Reference;
			else if (this._cache.Contains(jniResult))
				this._cache.Remove(jniResult);
		}
		this.FinalizeCall();
		return jniResult;
	}
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JClassObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JClassLocalRef FinalizeCall(JClassObject? result) => this.FinalizeCall<JClassLocalRef>(result);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JThrowableObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JThrowableLocalRef FinalizeCall(JThrowableObject? result) => this.FinalizeCall<JThrowableLocalRef>(result);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JStringObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JStringLocalRef FinalizeCall(JStringObject? result) => this.FinalizeCall<JStringLocalRef>(result);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JArrayLocalRef FinalizeCall(JArrayObject? result) => this.FinalizeCall<JArrayLocalRef>(result);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <typeparam name="TElement">Type of <see cref="IReferenceType{TReference}"/> array element.</typeparam>
	/// <param name="result">A <see cref="JArrayObject{TElement}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JObjectArrayLocalRef FinalizeCall<TElement>(JArrayObject<TElement>? result)
		where TElement : JReferenceObject, IReferenceType<TElement>
		=> this.FinalizeCall<JObjectArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JBoolean}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JBooleanArrayLocalRef FinalizeCall(JArrayObject<JBoolean>? result)
		=> this.FinalizeCall<JBooleanArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JByte}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JByteArrayLocalRef FinalizeCall(JArrayObject<JByte>? result)
		=> this.FinalizeCall<JByteArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JClassObject"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JCharArrayLocalRef FinalizeCall(JArrayObject<JChar>? result)
		=> this.FinalizeCall<JCharArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JDouble}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JDoubleArrayLocalRef FinalizeCall(JArrayObject<JDouble>? result)
		=> this.FinalizeCall<JDoubleArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JFloat}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JFloatArrayLocalRef FinalizeCall(JArrayObject<JFloat>? result)
		=> this.FinalizeCall<JFloatArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JInt}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JIntArrayLocalRef FinalizeCall(JArrayObject<JInt>? result) => this.FinalizeCall<JIntArrayLocalRef>(result);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JLong}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JLongArrayLocalRef FinalizeCall(JArrayObject<JLong>? result)
		=> this.FinalizeCall<JLongArrayLocalRef>(result?.Object);
	/// <summary>
	/// Finalizes call.
	/// </summary>
	/// <param name="result">A <see cref="JArrayObject{JShort}"/> result.</param>
	/// <returns>A JNI reference to <paramref name="result"/>.</returns>
	public JShortArrayLocalRef FinalizeCall(JArrayObject<JShort>? result)
		=> this.FinalizeCall<JShortArrayLocalRef>(result?.Object);

	/// <summary>
	/// Builder for <see cref="JNativeCallAdapter"/>
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
			jClass = this.CreateInitialClass(classRef, true);
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
			jString = this.CreateInitialObject<JStringObject>(stringRef.Value);
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
		/// Appends to current call a <see cref="JArrayObject"/> parameter.
		/// </summary>
		/// <param name="arrayRef">A parameter <see cref="JArrayLocalRef"/> reference.</param>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance from <paramref name="arrayRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter(JArrayLocalRef arrayRef, out JArrayObject jArray)
		{
			jArray = (JArrayObject)this.CreateInitialObject<JLocalObject>(arrayRef.Value);
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
		public Builder WithParameter(JShortArrayLocalRef arrayRef, out JArrayObject<JShort> jArray)
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
		public Builder WithParameter<TElement>(JObjectArrayLocalRef arrayRef, out JArrayObject<TElement> jArray)
			where TElement : JReferenceObject, IReferenceType<TElement>
		{
			jArray = this.CreateInitialObject<JArrayObject<TElement>>(arrayRef.Value);
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
			where TObject : JReferenceObject, IReferenceType<TObject>
		{
			if (JLocalObject.IsObjectType<TObject>())
			{
				Unsafe.SkipInit(out jLocal);
				return this.WithParameter(localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
			}
			if (JLocalObject.IsClassType<TObject>())
			{
				Unsafe.SkipInit(out jLocal);
				JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
				return this.WithParameter(classRef, out Unsafe.As<TObject, JClassObject>(ref jLocal));
			}
			jLocal = this.CreateInitialObject<TObject>(localRef);
			return this;
		}
		/// <summary>
		/// Appends to current call a <see cref="JThrowableObject"/> parameter.
		/// </summary>
		/// <param name="throwableRef">A parameter <see cref="JThrowableLocalRef"/> reference.</param>
		/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance from <paramref name="throwableRef"/>.</param>
		/// <returns>Current <see cref="Builder"/> instance.</returns>
		public Builder WithParameter<TThrowable>(JThrowableLocalRef throwableRef, out TThrowable jThrowable)
			where TThrowable : JThrowableObject, IThrowableType<TThrowable>
		{
			jThrowable = this.CreateInitialObject<TThrowable>(throwableRef.Value);
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
			where TElement : IDataType<TElement>
		{
			jArray = this.CreateInitialObject<JArrayObject<TElement>>(arrayRef.Value);
			return this;
		}

		/// <summary>
		/// Retrieves current <see cref="JNativeCallAdapter"/> instance.
		/// </summary>
		/// <returns>A <see cref="JNativeCallAdapter"/> instance.</returns>
		public JNativeCallAdapter Build()
		{
			this._callAdapter._cache.Activate(out _);
			return this._callAdapter;
		}
	}
}