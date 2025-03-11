namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is proxy.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <exception cref="ArgumentException">Throws an exception if <paramref name="jObject"/> is proxy.</exception>
	[return: NotNullIfNotNull(nameof(jObject))]
	private static TObject? ThrowIfNotProxy<TObject>(TObject? jObject) where TObject : JReferenceObject
	{
		if (jObject is null || jObject.IsProxy) return jObject;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new ArgumentException(resource.InvalidProxyObject);
	}
	/// <summary>
	/// Throws an exception if <paramref name="jClass"/> is not <paramref name="typeMetadata"/> data type.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance</param>
	/// <param name="typeMetadata">A <see cref="ITypeInformation"/> instance</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="jClass"/> is not <paramref name="typeMetadata"/> data type.
	/// </exception>
	private static void ThrowIfNotClass(JClassObject jClass, JDataTypeMetadata typeMetadata)
	{
		if (typeMetadata.Hash.AsSpan().SequenceEqual(typeMetadata.Hash) ||
		    typeMetadata.ClassName.SequenceEqual(jClass.Name)) return;
		IMessageResource resource = IMessageResource.GetInstance();
		String objectClassName = ClassNameHelper.GetClassName(jClass.ClassSignature);
		String className = ITypeInformation.GetJavaClassName(typeMetadata);
		throw new ArgumentException(resource.NotTypeObject(objectClassName, className));
	}
	/// <summary>
	/// Throws an exception if <paramref name="jClass"/> is not <paramref name="elementMetadata"/> array data type.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance</param>
	/// <param name="elementMetadata">A <see cref="JDataTypeMetadata"/> instance</param>
	/// <exception cref="InvalidOperationException">
	/// Throws an exception if <paramref name="jClass"/> is not <paramref name="elementMetadata"/> array data type.
	/// </exception>
	private static void ThrowIfNotArrayClass(JClassObject jClass, JDataTypeMetadata elementMetadata)
	{
		IMessageResource resource = IMessageResource.GetInstance();
		String className = ClassNameHelper.GetClassName(jClass.ClassSignature);

		if (!jClass.IsArray)
			throw new ArgumentException(resource.InvalidArrayClass(className));

		if (jClass.Name.AsSpan().SequenceEqual(elementMetadata.ArraySignature)) return;

		String arrayClassName = ClassNameHelper.GetClassName(elementMetadata.ArraySignature);
		throw new ArgumentException(resource.NotTypeObject(className, arrayClassName));
	}
}