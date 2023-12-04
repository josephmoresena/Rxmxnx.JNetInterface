namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI instance.
/// </summary>
public interface IEnvironment : IWrapper<JEnvironmentRef>
{
	/// <summary>
	/// JNI reference to the interface.
	/// </summary>
	JEnvironmentRef Reference { get; }
	/// <summary>
	/// Virtual machine that owns the current JNI context.
	/// </summary>
	IVirtualMachine VirtualMachine { get; }
	/// <summary>
	/// JNI version.
	/// </summary>
	Int32 Version { get; }
	/// <summary>
	/// The current ensured capacity for local references.
	/// </summary>
	Int32? LocalCapacity { get; set; }

	/// <summary>
	/// Internal accessor provider object.
	/// </summary>
	internal IAccessProvider AccessProvider { get; }
	/// <summary>
	/// Internal class provider object.
	/// </summary>
	internal IClassProvider ClassProvider { get; }
	/// <summary>
	/// Internal reference provider object.
	/// </summary>
	internal IReferenceProvider ReferenceProvider { get; }
	/// <summary>
	/// Internal String provider object.
	/// </summary>
	internal IStringProvider StringProvider { get; }
	/// <summary>
	/// Internal Array provider object.
	/// </summary>
	internal IArrayProvider ArrayProvider { get; }

	JEnvironmentRef IWrapper<JEnvironmentRef>.Value => this.Reference;

	/// <summary>
	/// Retrieves the JNI type reference of <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>
	/// <see cref="JReferenceType"/> value indicating the reference <paramref name="jObject"/>
	/// reference type.
	/// </returns>
	JReferenceType GetReferenceType(JObject jObject);
	/// <summary>
	/// Indicates whether the both instance <paramref name="jObject"/> and <paramref name="jOther"/>
	/// refer to the same object.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <param name="jOther">Another <see cref="JObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if both <paramref name="jObject"/> and <paramref name="jOther"/> refer
	/// to the same object; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsSameObject(JObject jObject, JObject? jOther);
	/// <summary>
	/// Creates a <typeparamref name="TObject"/> instance for <paramref name="localRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="JLocalObject"/> type.</typeparam>
	/// <param name="localRef">A local object reference.</param>
	/// <returns>A <typeparamref name="TObject"/> instance passed as JNI argument.</returns>
	TObject? CreateParameterObject<TObject>(JObjectLocalRef localRef)
		where TObject : JLocalObject, IReferenceType<TObject>;
	/// <summary>
	/// Creates a <see cref="JClassObject"/> instance for <paramref name="classRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="classRef">A local class reference.</param>
	/// <returns>A <see cref="JClassObject"/> instance passed as JNI argument.</returns>
	JClassObject? CreateParameterObject(JClassLocalRef classRef);
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance for <paramref name="stringRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="stringRef">A local string reference.</param>
	/// <returns>A <see cref="JStringObject"/> instance passed as JNI argument.</returns>
	JStringObject? CreateParameterObject(JStringLocalRef stringRef);
	/// <summary>
	/// Creates a <see cref="JArrayObject{TElement}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="arrayRef">A local array reference.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance passed as JNI argument.</returns>
	JArrayObject<TElement>? CreateParameterArray<TElement>(JArrayLocalRef arrayRef)
		where TElement : IObject, IDataType<TElement>;

	/// <summary>
	/// Retrieves a <see cref="JObjectLocalRef"/> reference from given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JObjectLocalRef"/> reference from given object.</returns>
	JObjectLocalRef GetReturn(JLocalObject? jLocal);
	/// <summary>
	/// Retrieves a <see cref="JClassLocalRef"/> reference from given class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference from given object.</returns>
	JClassLocalRef GetReturn(JClassObject? jClass);
	/// <summary>
	/// Retrieves a <see cref="JArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <returns>A <see cref="JArrayLocalRef"/> reference from given object.</returns>
	JArrayLocalRef GetReturn(JArrayObject? jArray);
	/// <summary>
	/// Retrieves a <see cref="JThrowableLocalRef"/> reference from given throwable.
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <returns>A <see cref="JThrowableLocalRef"/> reference from given object.</returns>
	JThrowableLocalRef GetReturn(JThrowableObject? jThrowable);
}