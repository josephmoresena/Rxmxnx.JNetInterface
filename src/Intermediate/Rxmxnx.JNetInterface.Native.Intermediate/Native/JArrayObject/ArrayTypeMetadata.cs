namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement>
{
	/// <summary>
	/// This record stores the metadata for a class <see cref="IArrayType"/> type.
	/// </summary>
	private sealed record ArrayTypeMetadata : JArrayTypeMetadata
	{
		/// <summary>
		/// Metadata array instance.
		/// </summary>
		public static readonly ArrayTypeMetadata Instance = new();

		/// <inheritdoc/>
		public override Type Type => JArrayTypeMetadata.GetArrayType<TElement>() ?? typeof(JArrayObject);
		/// <inheritdoc/>
		public override JDataTypeMetadata ElementMetadata => IDataType.GetMetadata<TElement>();
		/// <inheritdoc/>
		public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<JArrayObject<TElement>>();
		/// <inheritdoc/>
		public override JClassTypeMetadata BaseMetadata => JLocalObject.ObjectClassMetadata;
		/// <inheritdoc/>
		public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces => InterfaceSet.ArraySet;

		/// <summary>
		/// Constructor.
		/// </summary>
		private ArrayTypeMetadata() : base(IDataType.GetMetadata<TElement>().ArraySignature,
		                                   JArrayTypeMetadata.IsFinalElementType(IDataType.GetMetadata<TElement>()),
		                                   JArrayTypeMetadata.GetArrayDeep<TElement>()) { }

		/// <inheritdoc/>
		public override String ToString()
			=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

		/// <inheritdoc/>
		internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
			Boolean realClass = false)
			=> JArrayObject<TElement>.Create(
				new() { Class = jClass, RealClass = realClass, LocalReference = localRef, });
		/// <inheritdoc/>
		internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
			=> jLocal?.CastTo<JArrayObject<TElement>>();
		/// <inheritdoc/>
		internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			=> jGlobal is null ? default(JLocalObject?) : JArrayObject<TElement>.Create(env, jGlobal);
		/// <inheritdoc/>
		internal override JFunctionDefinition<JArrayObject<TElement>> CreateFunctionDefinition(
			ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata)
			=> JFunctionDefinition<JArrayObject<TElement>>.Create(functionName, metadata);
		/// <inheritdoc/>
		internal override JFieldDefinition<JArrayObject<TElement>> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
			=> new(fieldName);
		/// <inheritdoc/>
		internal override JArrayTypeMetadata? GetArrayMetadata()
			=> JArrayTypeMetadata.GetArrayArrayMetadata(this.ArraySignature, typeof(TElement));
		/// <inheritdoc/>
		internal override void SetObjectElement(JArrayObject jArray, Int32 index, JReferenceObject? value)
		{
			if (jArray is not JArrayObject<TElement> jGenericArray) return;
			switch (value)
			{
				case TElement element:
					jGenericArray[index] = element;
					break;
				case null:
					jGenericArray[index] = default;
					break;
				default:
					ArrayTypeMetadata.SetObjectElement(jGenericArray, index, value);
					break;
			}
		}
		/// <summary>
		/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="index">Element index.</param>
		/// <param name="value">Object instance.</param>
		private static void SetObjectElement(JArrayObject<TElement> jArray, Int32 index, JReferenceObject value)
		{
			JReferenceTypeMetadata elementMetadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TElement>();
			JLocalObject? jLocal = value as JLocalObject ?? (value as IWrapper<JLocalObject>)?.Value;
			TElement? element = (TElement?)(Object?)elementMetadata.ParseInstance(jLocal);
			try
			{
				jArray[index] = element;
			}
			finally
			{
				(element as JLocalObject)?.Dispose();
			}
		}
	}
}