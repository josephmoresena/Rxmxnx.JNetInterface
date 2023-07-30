namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(JClassMetadata);

	/// <inheritdoc cref="IClass.Reference"/>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();

	/// <summary>
	/// Initialize the current instance with given <see cref="IDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">The <see cref="IDataType"/> with class definition.</typeparam>
	internal void Initialize<TDataType>() where TDataType : JLocalObject, IDataType
	{
		this._className = IDataType.GetMetadata<TDataType>().ClassName;
		this._signature = IDataType.GetMetadata<TDataType>().Signature;
		this._hash ??= new CStringSequence(this.Name, this.ClassSignature).ToString();
		this._isFinal = IDataType.GetMetadata<TDataType>().Modifier == JTypeModifier.Final;
	}

	/// <summary>
	/// Retrieves the hash for current type.
	/// </summary>
	/// <typeparam name="TDataType">The <see cref="IDataType"/> with class definition.</typeparam>
	/// <returns>The hash for current type.</returns>
	internal static String GetClassHash<TDataType>() where TDataType : IDataType
	{
		CString className = IDataType.GetMetadata<TDataType>().ClassName;
		CString classSignature = IDataType.GetMetadata<TDataType>().Signature;
		return new CStringSequence(className, classSignature).ToString();
	}
	/// <summary>
	/// Retrieves the hash for class.
	/// </summary>
	/// <param name="className">Fully qualified class name.</param>
	/// <param name="classSignature">JNI signature for <paramref name="className"/> class.</param>
	/// <returns>The hash for current class.</returns>
	internal static String GetClassHash(CString className, CString classSignature)
	{
		className = ValidationUtilities.ValidateNullTermination(className);
		classSignature = ValidationUtilities.ValidateNullTermination(classSignature);
		return new CStringSequence(className, classSignature).ToString();
	}
}