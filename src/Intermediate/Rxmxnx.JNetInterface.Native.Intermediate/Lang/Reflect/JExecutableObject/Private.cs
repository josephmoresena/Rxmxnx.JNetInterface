namespace Rxmxnx.JNetInterface.Lang.Reflect;

public partial class JExecutableObject : ILocalObject
{
	/// <inheritdoc cref="JExecutableObject.Definition"/>
	private JCallDefinition? _callDefinition;
	/// <summary>
	/// Class information for declaring class.
	/// </summary>
	private ITypeInformation? _classInformation;
	/// <inheritdoc cref="JExecutableObject.MethodId"/>
	private JMethodId? _methodId;

	ObjectMetadata ILocalObject.CreateMetadata()
		=> new ExecutableObjectMetadata(base.CreateMetadata())
		{
			Definition = this.Definition,
			ClassInformation = this._classInformation ?? this.DeclaringClass.GetInformation(),
			MethodId = this._methodId,
		};

	/// <summary>
	/// Retrieves the <see cref="JCallDefinition"/> instance for the current instance.
	/// </summary>
	/// <returns>A <see cref="JCallDefinition"/> instance.</returns>
	private JCallDefinition GetCallDefinition()
	{
		IEnvironment env = this.Environment;
		using JStringObject memberName = env.FunctionSet.GetName(this);
		using JArrayObject<JClassObject> parameterTypes = env.FunctionSet.GetParameterTypes(this);
		using JClassObject? returnType = env.FunctionSet.GetReturnType(this);
		return env.AccessFeature.GetDefinition(memberName, parameterTypes, returnType);
	}
	/// <summary>
	/// Retrieves the <see cref="JMethodId"/> identifier for the current instance.
	/// </summary>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private JMethodId GetMethodId()
	{
		IEnvironment env = this.Environment;
		return env.AccessFeature.GetMethodId(this);
	}

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not ExecutableObjectMetadata executableMetadata) return;
		this._callDefinition = executableMetadata.Definition;
		this._classInformation = executableMetadata.ClassInformation;
		this._methodId = executableMetadata.MethodId;
	}

	/// <inheritdoc/>
	internal override void ClearValue()
	{
		this._methodId = default;
		base.ClearValue();
	}
}