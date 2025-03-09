namespace Rxmxnx.JNetInterface.Lang.Reflect;

public partial class JFieldObject
{
	/// <summary>
	/// Class metadata for declaring class.
	/// </summary>
	private ITypeInformation? _classInformation;
	/// <inheritdoc cref="JFieldObject.Definition"/>
	private JFieldDefinition? _fieldDefinition;
	/// <inheritdoc cref="JFieldObject.FieldId"/>
	private JFieldId? _fieldId;

	ObjectMetadata ILocalObject.CreateMetadata()
		=> new FieldObjectMetadata(base.CreateMetadata())
		{
			Definition = this.Definition,
			ClassInformation = this._classInformation ?? this.DeclaringClass.GetInformation(),
			FieldId = this._fieldId,
		};

	/// <summary>
	/// Retrieves the <see cref="JFieldDefinition"/> instance for the current instance.
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
	/// Retrieves the <see cref="JFieldId"/> identifier for the current instance.
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
		this._classInformation = fieldMetadata.ClassInformation;
		this._fieldId = fieldMetadata.FieldId;
	}

	/// <inheritdoc/>
	internal override void ClearValue()
	{
		this._fieldId = default;
		base.ClearValue();
	}
}