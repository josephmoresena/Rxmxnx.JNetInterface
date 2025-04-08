namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement>
{
	/// <summary>
	/// This record stores the metadata for a class <see cref="IArrayType"/> type.
	/// </summary>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS2743,
	                 Justification = CommonConstants.NonGenericInGenericTypeJustification)]
#endif
	private sealed class ArrayTypeMetadata : JArrayTypeMetadata
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

		/// <summary>
		/// Constructor.
		/// </summary>
		private ArrayTypeMetadata() : base(IDataType.GetMetadata<TElement>().ArraySignature,
		                                   JArrayTypeMetadata.IsFinalElementType(IDataType.GetMetadata<TElement>()),
		                                   JArrayTypeMetadata.GetArrayDimension<TElement>()) { }

		/// <inheritdoc/>
		internal override Boolean IsInstance(JReferenceObject jObject)
		{
			Boolean result = jObject is IArrayObject<TElement>;
			if (result || this.ElementMetadata.Kind == JTypeKind.Primitive || jObject is not JArrayObject jArray)
				return result || jObject.InstanceOf<JArrayObject<TElement>>();
			JReferenceTypeMetadata elementMetadata = (JReferenceTypeMetadata)this.ElementMetadata;
			if (jArray.TypeMetadata.ElementMetadata is JReferenceTypeMetadata otherElementMetadata)
				result = otherElementMetadata.TypeOf(elementMetadata);
			return result || jObject.InstanceOf<JArrayObject<TElement>>();
		}
		/// <inheritdoc/>
		public override JArrayTypeMetadata? GetArrayMetadata()
			=> JArrayTypeMetadata.GetArrayArrayMetadata(this.ArraySignature, typeof(TElement));

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal override JClassObject GetClass(IEnvironment env)
			=> env.ClassFeature.GetClass<JArrayObject<TElement>>();
		/// <inheritdoc/>
		internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
			Boolean realClass = false)
			=> new Generic<TElement>(jClass, localRef, realClass);
		/// <inheritdoc/>
		internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
		{
			if (jLocal == null) return default;
			if (jLocal is not IArrayObject<TElement>)
				JLocalObject.Validate<JArrayObject<TElement>>(jLocal);
			return new JArrayObject<TElement>(JLocalObject.ArrayView.ParseArray<TElement>(jLocal, dispose));
		}
		/// <inheritdoc/>
		internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
		{
			if (jGlobal is null) return default;
			JLocalObject.Validate<JArrayObject<TElement>>(jGlobal);
			return new Generic<TElement>(env, jGlobal);
		}
		/// <inheritdoc/>
		internal override JFunctionDefinition<JArrayObject<TElement>> CreateFunctionDefinition(
			ReadOnlySpan<Byte> functionName, ReadOnlySpan<JArgumentMetadata> paramsMetadata)
			=> JFunctionDefinition<JArrayObject<TElement>>.Create(functionName, paramsMetadata);
		/// <inheritdoc/>
		internal override JFieldDefinition<JArrayObject<TElement>> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
			=> new(fieldName);
	}
}