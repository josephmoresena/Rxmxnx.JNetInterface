namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement>
{
	/// <summary>
	/// This record stores the metadata for a class <see cref="IArrayType"/> type.
	/// </summary>
	private sealed record JArrayGenericTypeMetadata : JArrayTypeMetadata
	{
		/// <summary>
		/// Metadata array instance.
		/// </summary>
		public static readonly JArrayGenericTypeMetadata Instance = new();

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
		private JArrayGenericTypeMetadata() : base(IDataType.GetMetadata<TElement>().ArraySignature,
		                                           JArrayTypeMetadata.GetArrayDeep<TElement>()) { }
		/// <inheritdoc/>
		public override String ToString()
			=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

		/// <inheritdoc/>
		internal override JLocalObject
			CreateInstance(JClassObject jClass, JObjectLocalRef localRef, Boolean realClass = false)
			=> JArrayObject<TElement>.Create(new IReferenceType.ClassInitializer
			{
				Class = jClass, RealClass = realClass, LocalReference = localRef,
			});
		/// <inheritdoc/>
		internal override JArrayObject? ParseInstance(JLocalObject? jLocal)
		{
			if (jLocal is null) return default;
			return jLocal as JArrayObject<TElement> ?? JArrayObject<TElement>.Create(jLocal);
		}
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
					JArrayGenericTypeMetadata.SetObjectElement(jGenericArray, index, value);
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
			TElement element = value is JLocalObject jLocal ?
				(TElement)(Object)elementMetadata.ParseInstance(jLocal) :
				(TElement)(Object)elementMetadata.CreateInstance(jArray.Environment.ClassFeature.GetClass<TElement>(),
				                                                 value.As<JObjectLocalRef>());
			try
			{
				jArray[index] = element;
			}
			finally
			{
				((IDisposable)element).Dispose();
			}
		}
	}
}