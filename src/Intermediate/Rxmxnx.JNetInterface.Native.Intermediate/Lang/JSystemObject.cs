namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JSystemObject>;

/// <summary>
/// This class represents a local <c>java.lang.System</c> instance.
/// </summary>
public sealed class JSystemObject : JLocalObject.Uninstantiable<JSystemObject>, IUninstantiableType<JSystemObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.SystemHash, 16);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.CreateBuiltInMetadata<JSystemObject>(JSystemObject.typeInfo, JTypeModifier.Final);

	static TypeMetadata IClassType<JSystemObject>.Metadata => JSystemObject.typeMetadata;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd0;

	/// <summary>
	/// Retrieves the system property indicated by the specified key.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="propertyName">The name of the system property.</param>
	/// <returns>
	/// The <see cref="String"/> value of the system property, or <see langword="null"/> if there is no property
	/// with that key.
	/// </returns>
	public static String? GetProperty(IEnvironment env, String? propertyName)
	{
		if (String.IsNullOrWhiteSpace(propertyName)) return default;
		using JStringObject propName = JStringObject.Create(env, propertyName);
		using JStringObject? propValue = env.FunctionSet.GetProperty(propName);
		return propValue?.Value;
	}
	/// <summary>
	/// Sets the system property indicated by the specified key.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="propertyName">The name of the system property.</param>
	/// <param name="propertyValue">The value of the system property.</param>
	public static void SetProperty(IEnvironment env, String propertyName, String? propertyValue)
	{
#if NET8_0_OR_GREATER
		ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
#else
		ArgumentException.ThrowIfNullOrEmpty(propertyName);
#endif
		using JStringObject propName = JStringObject.Create(env, propertyName);
		using JStringObject? propValue = JStringObject.Create(env, propertyValue);
		env.FunctionSet.SetProperty(propName, propValue);
	}
}