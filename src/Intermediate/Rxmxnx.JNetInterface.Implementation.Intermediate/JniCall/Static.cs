namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JniCall
{
	public readonly ref partial struct Builder
	{
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder a <see cref="JEnvironmentRef"/> reference.
		/// </summary>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(JEnvironmentRef envRef) => new(new(envRef));
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using a <see cref="JEnvironmentRef"/> reference and
		/// a <see cref="JObjectLocalRef"/> instance.
		/// </summary>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">Output. <see cref="JLocalObject"/> from <paramref name="localRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(JEnvironmentRef envRef, JObjectLocalRef localRef,
			out JLocalObject jLocal)
		{
			Builder result = Builder.Create(envRef);
			jLocal = result.CreateInitialObject(localRef);
			return result;
		}
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using a <see cref="JEnvironmentRef"/> reference and
		/// a <see cref="JClassLocalRef"/> instance.
		/// </summary>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="classRef">Call <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="jClass">Output. <see cref="JClassObject"/> from <paramref name="classRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(JEnvironmentRef envRef, JClassLocalRef classRef,
			out JClassObject jClass)
		{
			Builder result = Builder.Create(envRef);
			jClass = result.CreateInitialClass(classRef);
			return result;
		}
		
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using a <see cref="JEnvironmentRef"/> reference and
		/// a <see cref="JObjectLocalRef"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">Output. <typeparamref name="TObject"/> from <paramref name="localRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create<TObject>(JEnvironmentRef envRef, JObjectLocalRef localRef,
			out TObject jLocal) where TObject : JLocalObject, IReferenceType<TObject>
		{
			Type typeofT = typeof(TObject);
			if (typeofT == typeof(JLocalObject))
			{
				Unsafe.SkipInit(out jLocal);
				return Builder.Create(envRef, localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
			}
			if (typeofT == typeof(JLocalObject))
			{
				Unsafe.SkipInit(out jLocal);
				return Builder.Create(envRef, localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
			}
			
			Builder result = Builder.Create(envRef);
			jLocal = result.CreateInitialObject<TObject>(localRef);
			return result;
		}
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using <see cref="IVirtualMachine"/> instance and
		/// a <see cref="JEnvironmentRef"/> reference.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(IVirtualMachine vm, JEnvironmentRef envRef) => new(new((JVirtualMachine)vm, envRef));
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using <see cref="IVirtualMachine"/> instance, 
		/// a <see cref="JEnvironmentRef"/> reference and a <see cref="JObjectLocalRef"/> instance.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">Output. <see cref="JLocalObject"/> from <paramref name="localRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(IVirtualMachine vm, JEnvironmentRef envRef, JObjectLocalRef localRef,
			out JLocalObject jLocal)
		{
			Builder result = Builder.Create(vm, envRef);
			jLocal = result.CreateInitialObject(localRef);
			return result;
		}
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using <see cref="IVirtualMachine"/> instance, 
		/// a <see cref="JEnvironmentRef"/> reference and a <see cref="JClassLocalRef"/> instance.
		/// </summary>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="classRef">Call <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="jClass">Output. <see cref="JClassObject"/> from <paramref name="classRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create(IVirtualMachine vm, JEnvironmentRef envRef, JClassLocalRef classRef,
			out JClassObject jClass)
		{
			Builder result = Builder.Create(vm, envRef);
			jClass = result.CreateInitialClass(classRef);
			return result;
		}
		/// <summary>
		/// Creates a <see cref="JniCall"/> builder using <see cref="IVirtualMachine"/> instance, 
		/// a <see cref="JEnvironmentRef"/> reference and a <see cref="JObjectLocalRef"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
		/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
		/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
		/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
		/// <param name="jLocal">Output. <typeparamref name="TObject"/> from <paramref name="localRef"/>.</param>
		/// <returns>A <see cref="Builder"/> instance.</returns>
		public static Builder Create<TObject>(IVirtualMachine vm, JEnvironmentRef envRef, JObjectLocalRef localRef,
			out TObject jLocal) where TObject : JLocalObject, IReferenceType<TObject>
		{
			Type typeofT = typeof(TObject);
			if (typeofT == typeof(JLocalObject))
			{
				Unsafe.SkipInit(out jLocal);
				return Builder.Create(vm, envRef, localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
			}
			if (typeofT == typeof(JClassObject))
			{
				Unsafe.SkipInit(out jLocal);
				JClassLocalRef classRef = NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
				return Builder.Create(vm, envRef, classRef, out Unsafe.As<TObject, JClassObject>(ref jLocal));
			}
			
			Builder result = Builder.Create(vm, envRef);
			jLocal = result.CreateInitialObject<TObject>(localRef);
			return result;
		}
	}
}