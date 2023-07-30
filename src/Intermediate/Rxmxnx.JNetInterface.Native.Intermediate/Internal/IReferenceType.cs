namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
internal interface IReferenceType : IObject, IDataType, IDisposable { }

/// <summary>
/// This interface exposes an object that represents a java reference type instance.
/// </summary>
/// <typeparam name="TObject">Type of java reference type.</typeparam>
internal interface IReferenceType<out TObject> : IReferenceType, IDataType<TObject>
	where TObject : JReferenceObject, IReferenceType<TObject> { }