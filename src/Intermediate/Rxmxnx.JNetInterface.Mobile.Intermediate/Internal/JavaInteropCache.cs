namespace Rxmxnx.JNetInterface.Internal;

internal sealed class JavaInteropCache : AlienLocalCache
{
	/// <summary>
	/// A <see cref="IVirtualMachineHost"/> instance.
	/// </summary>
	private readonly IVirtualMachineHost _host;
	/// <inheritdoc/>
	public override String Name => "java.interop";

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <param name="env">A <see cref="INativeThread"/> instance.</param>
	public JavaInteropCache(IVirtualMachineHost host, INativeThread env) : base(env)
	{
		this._host = host;
		this.Environment.LocalCache = this;
	}

	/// <summary>
	/// Registers all references to the current instance from the current context.
	/// </summary>
	/// <param name="array">Array to store local objects.</param>
	/// <param name="objects">A read-only span of objects.</param>
	/// <returns>A read-only span of <see cref="JLocalObject"/> instances.</returns>
	public ReadOnlySpan<JLocalObject?> RegisterContext(JLocalObject?[] array, ReadOnlySpan<IJavaPeerable?> objects)
	{
		JTrace.RegisterInterop(this.Environment, objects.Length);
		for (Int32 index = 0; index < objects.Length; index++)
		{
			JObjectLocalRef localRef = JavaInteropCache.GetLocalRef(objects[index]);
			if (localRef == default)
			{
				array[index] = default;
				continue;
			}
			array[index] = this.GetLocalObject(localRef, objects[index]?.GetJniTypeName());
		}
		return array.AsSpan()[..objects.Length];
	}
	public ReadOnlySpan<JLocalObject?> RegisterContext(JLocalObject?[] array, JniObjectInfo[] objects)
	{
		JTrace.RegisterInterop(this.Environment, objects.Length);
		for (Int32 index = 0; index < objects.Length; index++)
		{
			JObjectLocalRef localRef = JavaInteropCache.GetLocalRef(objects[index].Reference);
			if (localRef == default)
			{
				array[index] = default;
				continue;
			}
			array[index] = this.GetLocalObject(localRef, objects[index].JniClassName);
		}
		return array.AsSpan()[..objects.Length];
	}
	/// <inheritdoc/>
	public override void Remove(JObjectLocalRef localRef)
	{
		base.Remove(localRef);
		this.RemoveAlien(localRef);
	}

	/// <summary>
	/// Retrieves a <see cref="JLocalObject"/> instance from <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A JNI reference.</param>
	/// <param name="jniClassName">The JNI object class name.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	private JLocalObject GetLocalObject(JObjectLocalRef localRef, String? jniClassName)
	{
		if ("java/lang/Class".AsSpan().SequenceEqual(jniClassName))
			return this.GetLocalClass(localRef);
		JClassObject jClass = this.GetObjectClass(localRef, jniClassName, out JReferenceTypeMetadata typeMetadata);
		if (CommonNames.ClassObject.SequenceEqual(jClass.Name.AsSpan()))
			return this.GetLocalClass(localRef);
		JLocalObject jLocal =
			typeMetadata.CreateInstance(jClass, localRef, typeMetadata.ClassName.SequenceEqual(jClass.Name));
		this.RegisterAlien(localRef, jLocal);
		return jLocal;
	}
	/// <summary>
	/// Retrieves a <see cref="JClassObject"/> instance from <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A JNI reference.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	private JClassObject GetLocalClass(JObjectLocalRef localRef)
	{
		JClassLocalRef classRef = new(localRef);
		JClassObject result = this.Environment.GetReferenceTypeClass(classRef, true);
		if (classRef.Value == result.LocalReference)
			// Class is owned by this context.
			this.RegisterAlien(classRef, result);
		else
			// Class is not owned by this context. A ClassView is registered instead.
			this.RegisterAlien(classRef, new ClassView(classRef, result));
		return result;
	}
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="localRef">A JNI reference.</param>
	/// <param name="jniClassName">The JNI object class name.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JClassObject GetObjectClass(JObjectLocalRef localRef, String? jniClassName,
		out JReferenceTypeMetadata typeMetadata)
	{
		if (String.IsNullOrEmpty(jniClassName))
			return this.Environment.GetObjectClass(localRef, out typeMetadata);

		JTrace.FindClass(jniClassName);
		Int32 classNameLength = Encoding.UTF8.GetByteCount(jniClassName);
		ReadOnlySpan<Byte> className = JavaInteropCache.CopyClassName(jniClassName, stackalloc Byte[classNameLength]);
		String classHash = TypeInfoSequence.CreateHash(className, false, out Boolean isArray);
		ClassObjectMetadata classObjectMetadata =
			this._host.TypeManager.GetTypeInformation(classHash) as ClassObjectMetadata ??
			new ClassObjectMetadata(classHash, classNameLength, classNameLength + 2, isArray);
		return this.Environment.GetObjectClass(classObjectMetadata, localRef, out typeMetadata);
	}

	/// <summary>
	/// Copies the <paramref name="jniClassName"/> UTF-8 bytes to <paramref name="className"/>.
	/// </summary>
	/// <param name="jniClassName">JNI class name.</param>
	/// <param name="className">UTF-8 byte buffer.</param>
	/// <returns>UTF-8 JNI class name.</returns>
	private static ReadOnlySpan<Byte> CopyClassName(String jniClassName, Span<Byte> className)
	{
		Encoding.UTF8.GetBytes(jniClassName, className);
		return className;
	}
	/// <summary>
	/// Retrieves the <see cref="JObjectLocalRef"/> reference from <paramref name="obj"/>.
	/// </summary>
	/// <param name="obj">A <see cref="IJavaPeerable"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JObjectLocalRef GetLocalRef(IJavaPeerable? obj)
		=> obj is null ? default : JavaInteropCache.GetLocalRef(obj.PeerReference);
	/// <summary>
	/// Retrieves the <see cref="JObjectLocalRef"/> reference from <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JniObjectReference"/> reference.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference.</returns>
	private static JObjectLocalRef GetLocalRef(JniObjectReference reference)
	{
		JObjectLocalRef localRef = default;
		if (reference.IsValid && reference.Handle != IntPtr.Zero)
			Unsafe.As<JObjectLocalRef, IntPtr>(ref localRef) = reference.Handle;
		return localRef;
	}
}