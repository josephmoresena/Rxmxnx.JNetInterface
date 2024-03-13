namespace Rxmxnx.JNetInterface.Lang.Reflect;

public partial class JFieldObject : ILocalObject
{
	/// <summary>
	/// Class hash for declaring class.
	/// </summary>
	private String? _classHash;
	/// <inheritdoc cref="JFieldObject.Definition"/>
	private JFieldDefinition? _fieldDefinition;
	/// <inheritdoc cref="JFieldObject.FieldId"/>
	private JFieldId? _fieldId;

	ObjectMetadata ILocalObject.CreateMetadata()
		=> new FieldObjectMetadata(base.CreateMetadata())
		{
			Definition = this.Definition,
			ClassHash = this._classHash ?? this.DeclaringClass.Hash,
			FieldId = this._fieldId,
		};

	/// <summary>
	/// Retrieves the <see cref="JFieldDefinition"/> instance for current instance.
	/// </summary>
	/// <returns>A <see cref="JFieldDefinition"/> instance.</returns>
	private JFieldDefinition GetFieldDefinition()
	{
		IEnvironment env = this.Environment;
		using JStringObject memberName = env.FunctionSet.GetName(this);
		using JClassObject returnType = env.FunctionSet.GetFieldType(this);
		return env.AccessFeature.GetDefinition(memberName, returnType);
	}
	/// <summary>
	/// Retrieves the <see cref="JFieldId"/> identifier for current instance.
	/// </summary>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private JFieldId GetFieldId()
	{
		IEnvironment env = this.Environment;
		return env.AccessFeature.GetFieldId(this);
	}

	/// <inheritdoc/>
	protected override void ProcessMetadata(ObjectMetadata instanceMetadata)
	{
		base.ProcessMetadata(instanceMetadata);
		if (instanceMetadata is not FieldObjectMetadata fieldMetadata) return;
		this._fieldDefinition = fieldMetadata.Definition;
		this._classHash = fieldMetadata.ClassHash;
		this._fieldId = fieldMetadata.FieldId;
	}

	/// <inheritdoc/>
	internal override void ClearValue()
	{
		this._fieldId = default;
		base.ClearValue();
	}
}