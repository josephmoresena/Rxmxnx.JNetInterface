namespace Rxmxnx.JNetInterface;

public readonly ref partial struct JNativeCallAdapter
{
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder a <see cref="JEnvironmentRef"/> reference.
	/// </summary>
	/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>A <see cref="Builder"/> instance.</returns>
	public static Builder Create(JEnvironmentRef envRef) => new(new(envRef));
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using a <see cref="JEnvironmentRef"/> reference and
	/// a <see cref="JObjectLocalRef"/> instance.
	/// </summary>
	/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="jLocal">Output. <see cref="JLocalObject"/> from <paramref name="localRef"/>.</param>
	/// <returns>A <see cref="Builder"/> instance.</returns>
	public static Builder Create(JEnvironmentRef envRef, JObjectLocalRef localRef, out JLocalObject jLocal)
	{
		Builder result = JNativeCallAdapter.Create(envRef);
		jLocal = result.CreateInitialObject(localRef);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using a <see cref="JEnvironmentRef"/> reference and
	/// a <see cref="JClassLocalRef"/> instance.
	/// </summary>
	/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="classRef">Call <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">Output. <see cref="JClassObject"/> from <paramref name="classRef"/>.</param>
	/// <returns>A <see cref="Builder"/> instance.</returns>
	public static Builder Create(JEnvironmentRef envRef, JClassLocalRef classRef, out JClassObject jClass)
	{
		Builder result = JNativeCallAdapter.Create(envRef);
		jClass = result.CreateInitialClass(classRef, true);
		return result;
	}

	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using a <see cref="JEnvironmentRef"/> reference and
	/// a <see cref="JObjectLocalRef"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
	/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="localRef">Call <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="jLocal">Output. <typeparamref name="TObject"/> from <paramref name="localRef"/>.</param>
	/// <returns>A <see cref="Builder"/> instance.</returns>
	public static Builder Create<TObject>(JEnvironmentRef envRef, JObjectLocalRef localRef, out TObject jLocal)
		where TObject : JLocalObject, IReferenceType<TObject>
	{
		if (JLocalObject.IsObjectType<TObject>())
		{
			Unsafe.SkipInit(out jLocal);
			return JNativeCallAdapter.Create(envRef, localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
		}
		if (JLocalObject.IsClassType<TObject>())
		{
			Unsafe.SkipInit(out jLocal);
			JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
			return JNativeCallAdapter.Create(envRef, classRef, out Unsafe.As<TObject, JClassObject>(ref jLocal));
		}

		Builder result = JNativeCallAdapter.Create(envRef);
		jLocal = result.CreateInitialObject<TObject>(localRef);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using <see cref="IVirtualMachine"/> instance and
	/// a <see cref="JEnvironmentRef"/> reference.
	/// </summary>
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">Call <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>A <see cref="Builder"/> instance.</returns>
	public static Builder Create(IVirtualMachine vm, JEnvironmentRef envRef) => new(new((JVirtualMachine)vm, envRef));
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using <see cref="IVirtualMachine"/> instance,
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
		Builder result = JNativeCallAdapter.Create(vm, envRef);
		jLocal = result.CreateInitialObject(localRef);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using <see cref="IVirtualMachine"/> instance,
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
		Builder result = JNativeCallAdapter.Create(vm, envRef);
		jClass = result.CreateInitialClass(classRef, true);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JNativeCallAdapter"/> builder using <see cref="IVirtualMachine"/> instance,
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
		if (JLocalObject.IsObjectType<TObject>())
		{
			Unsafe.SkipInit(out jLocal);
			return JNativeCallAdapter.Create(vm, envRef, localRef, out Unsafe.As<TObject, JLocalObject>(ref jLocal));
		}
		if (JLocalObject.IsClassType<TObject>())
		{
			Unsafe.SkipInit(out jLocal);
			JClassLocalRef classRef = JClassLocalRef.FromReference(in localRef);
			return JNativeCallAdapter.Create(vm, envRef, classRef, out Unsafe.As<TObject, JClassObject>(ref jLocal));
		}

		Builder result = JNativeCallAdapter.Create(vm, envRef);
		jLocal = result.CreateInitialObject<TObject>(localRef);
		return result;
	}

	public readonly ref partial struct Builder
	{
		/// <summary>
		/// Throws an exception if <paramref name="localRef"/> is not a local reference.
		/// </summary>
		/// <param name="env">A <see cref="JEnvironment"/> instance.</param>
		/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
		/// <exception cref="ArgumentException">
		/// Throws an exception if <paramref name="localRef"/> is not a local reference.
		/// </exception>
		private static void ThrowIfNotLocalReference(JEnvironment env, JObjectLocalRef localRef)
		{
			if (env.GetReferenceType(localRef) != JReferenceType.LocalRefType)
				throw new ArgumentException("JNI call only allow local references.");
		}
		/// <inheritdoc
		///     cref="JEnvironment.GetObjectClass(Rxmxnx.JNetInterface.Native.References.JObjectLocalRef,out Rxmxnx.JNetInterface.Types.Metadata.JReferenceTypeMetadata)"/>
		private static (JClassObject jClass, JReferenceTypeMetadata typeMetadata) GetObjectClassWithMetadata(
			(JEnvironment env, JObjectLocalRef localRef) args)
			=> (args.env.GetObjectClass(args.localRef, out JReferenceTypeMetadata typeMetadata), typeMetadata);
		/// <inheritdoc cref="JEnvironment.GetObjectClass(JObjectLocalRef)"/>
		private static JClassObject GetObjectClass((JEnvironment env, JObjectLocalRef localRef) args)
		{
			JClassLocalRef classRef = args.env.GetObjectClass(args.localRef);
			return args.env.GetClass(classRef);
		}
	}
}