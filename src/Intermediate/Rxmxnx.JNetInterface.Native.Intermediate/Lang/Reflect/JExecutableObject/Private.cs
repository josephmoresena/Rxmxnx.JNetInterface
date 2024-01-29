namespace Rxmxnx.JNetInterface.Reflect;

public partial class JExecutableObject : ILocalObject
{
	/// <inheritdoc cref="JExecutableObject.Definition"/>
	private JCallDefinition? _callDefinition;
	/// <inheritdoc cref="JExecutableObject.MethodId"/>
	private JMethodId? _methodId;

	/// <summary>
	/// Executable JNI definition.
	/// </summary>
	public JCallDefinition Definition => this._callDefinition ??= this.GetCallDefinition();

	/// <summary>
	/// JNI method id.
	/// </summary>
	internal JMethodId MethodId => this._methodId ??= this.GetMethodId();

	ObjectMetadata ILocalObject.CreateMetadata()
		=> new ExecutableObjectMetadata(base.CreateMetadata())
		{
			Definition = this._callDefinition, MethodId = this._methodId,
		};

	/// <summary>
	/// Retrieves the <see cref="JCallDefinition"/> instance for current instance.
	/// </summary>
	/// <returns>A <see cref="JCallDefinition"/> instance.</returns>
	private JCallDefinition GetCallDefinition()
	{
		IEnvironment env = this.Environment;
		using JStringObject memberName = env.Functions.GetName(this);
		using JArrayObject<JClassObject> parameterTypes = env.Functions.GetParameterTypes(this);
		using JClassObject? returnType = env.Functions.GetReturnType(this);
		return env.AccessFeature.GetDefinition(memberName, parameterTypes, returnType);
	}
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
		this._methodId = executableMetadata.MethodId;
	}

	/// <inheritdoc/>
	internal override void ClearValue()
	{
		this._methodId = default;
		base.ClearValue();
	}
}