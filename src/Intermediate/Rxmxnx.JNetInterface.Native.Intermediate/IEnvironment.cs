﻿namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI instance.
/// </summary>
public partial interface IEnvironment
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
	/// Internal accessor object.
	/// </summary>
	internal IAccessor Accessor { get; }
	/// <summary>
	/// Internal class provider object.
	/// </summary>
	internal IClassProvider ClassProvider { get; }

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
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitive"/> type.</typeparam>
	/// <returns>The class instance for given type.</returns>
	IClass GetWrapperClass<TPrimitive>() where TPrimitive : IPrimitive<TPrimitive>
		=> this.ClassProvider.GetClass<TPrimitive>();
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	IClass GetObjectClass(CString className) => this.ClassProvider.GetClass(className);
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="JLocalObject"/> type.</typeparam>
	/// <returns>The class instance for given type.</returns>
	IClass GetObjectClass<TObject>() where TObject : JLocalObject, IDataType<TObject>
		=> this.ClassProvider.GetClass<TObject>();
	/// <summary>
	/// Creates a <typeparamref name="TObject"/> instance for <paramref name="objRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="JLocalObject"/> type.</typeparam>
	/// <param name="objRef"></param>
	/// <returns>A <typeparamref name="TObject"/> instance passed as JNI argument.</returns>
	TObject CreateParameterObject<TObject>(JObjectLocalRef objRef) where TObject : JLocalObject, IDataType<TObject>;
}