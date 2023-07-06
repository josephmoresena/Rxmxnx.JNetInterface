namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java reference class instance.
/// </summary>
internal interface IClassType : IClass, IReferenceType { }

/// <summary>
/// This interface exposes an object that represents a java reference class instance.
/// </summary>
/// <typeparam name="TClass">Type of java class type.</typeparam>
internal interface IClassType<out TClass> : IClassType, IReferenceType<TClass>
	where TClass : JReferenceObject, IClassType<TClass> { }