namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents an accessible manager object.
/// </summary>
internal interface IAccessibleManager
{
	/// <summary>
	/// Retrieves field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	JFieldId GetFieldId(JFieldDefinition definition, JClassLocalRef classRef);
	/// <summary>
	/// Retrieves static field identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	JFieldId GetStaticFieldId(JFieldDefinition definition, JClassLocalRef classRef);
	/// <summary>
	/// Retrieves method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	JMethodId GetMethodId(JCallDefinition definition, JClassLocalRef classRef);
	/// <summary>
	/// Retrieves static method identifier for <paramref name="definition"/> in <paramref name="classRef"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	JMethodId GetStaticMethodId(JCallDefinition definition, JClassLocalRef classRef);
}