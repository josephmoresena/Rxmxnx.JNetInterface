namespace Rxmxnx.JNetInterface.Lang.Reflect;

public partial class JExecutableObject
{
	/// <inheritdoc cref="JExecutableObject.Definition"/>
	private JCallDefinition? _callDefinition;
	/// <summary>
	/// Class information for the declaring class.
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
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private JCallDefinition GetCallDefinition()
	{
		IEnvironment env = this.Environment;
		CallDefinitionFunction callDefinition = new(this);
		callDefinition.WithFrame(env, IVirtualMachine.GetCallDefinitionCapacity, out JCallDefinition result);
		return result;
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

	/// <summary>
	/// Frame function that reads the call definition of an executable.
	/// </summary>
	/// <param name="executable">A <see cref="JExecutableObject"/> instance.</param>
	private readonly struct CallDefinitionFunction(JExecutableObject executable) : IFrameFunction<JCallDefinition>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		JCallDefinition IFrameFunction<JCallDefinition>.Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> _)
		{
			using JStringObject memberName = env.FunctionSet.GetName(executable);
			using JArrayObject<JClassObject> parameterTypes = env.FunctionSet.GetParameterTypes(executable);
			using JClassObject? returnType = env.FunctionSet.GetReturnType(executable);
			return env.AccessFeature.GetDefinition(memberName, parameterTypes, returnType);
		}
	}
}