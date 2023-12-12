namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Number</c> instance.
/// </summary>
public partial class JNumberObject : JLocalObject, IClassType<JNumberObject>,
	IInterfaceImplementation<JNumberObject, JSerializableObject>
{
	/// <inheritdoc/>
	internal JNumberObject(IEnvironment env, JObjectLocalRef jLocalRef, Boolean isDummy, JClassObject? jClass = default)
		: base(env, jLocalRef, isDummy, jClass) { }

	/// <inheritdoc/>
	protected JNumberObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JNumberObject(JLocalObject jLocal, JClassObject? jClass = default) : base(
		jLocal, jClass ?? jLocal.Environment.ClassProvider.NumberClassObject) { }

	/// <summary>
	/// Returns the value of the specified number as a <typeparamref name="TPrimitive"/>, which may
	/// involve rounding or truncation.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType"/> numeric type.</typeparam>
	/// <returns>A <typeparamref name="TPrimitive"/> numeric value.</returns>
	public virtual TPrimitive GetValue<TPrimitive>()
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>
	{
		JFunctionDefinition<TPrimitive> definition = JNumberObject.GetValueDefinition<TPrimitive>();
		return JFunctionDefinition<TPrimitive>.Invoke(definition, this);
	}

	/// <inheritdoc/>
	public static JNumberObject? Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JNumberObject>(jLocal)) : default;
	/// <inheritdoc/>
	public static JNumberObject? Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ? new(env, JLocalObject.Validate<JNumberObject>(jGlobal, env)) : default;
}