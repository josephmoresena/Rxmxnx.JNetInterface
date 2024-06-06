namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class ArrayView
	{
		/// <summary>
		/// This class represents a generic array instance.
		/// </summary>
		/// <typeparam name="TElement">Type of <see cref="IDataType"/> array element.</typeparam>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected sealed class Generic<TElement> : JArrayObject, IArrayObject<TElement>
			where TElement : IObject, IDataType<TElement>
		{
			/// <summary>
			/// String value.
			/// </summary>
			private String? _stringValue;

			/// <inheritdoc/>
			internal override JArrayTypeMetadata TypeMetadata => JArrayObject<TElement>.Metadata;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
			/// <param name="localRef">Local reference.</param>
			/// <param name="realClass">Indicates whether current class is real class.</param>
			internal Generic(JClassObject jClass, JObjectLocalRef localRef, Boolean realClass = false) : base(
				new() { Class = jClass, RealClass = realClass, LocalReference = localRef, }) { }
			/// <inheritdoc/>
			internal Generic(JClassObject jClass, JArrayLocalRef jArrayRef, Int32? length) : base(
				jClass, jArrayRef, length) { }
			/// <inheritdoc/>
			internal Generic(JLocalObject jLocal, JClassObject? jClass) : base(jLocal, jClass) { }
			/// <inheritdoc/>
			internal Generic(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
			/// <inheritdoc/>
			internal override void ValidateObjectElement(JReferenceObject? jObject)
			{
				Boolean result = IDataType.GetMetadata<TElement>() is JReferenceTypeMetadata metadata ?
					metadata.IsInstance(this.Environment, jObject) :
					jObject is TElement;
				CommonValidationUtilities.ThrowIfInvalidCast(IDataType.GetMetadata<TElement>(), result);
			}

			/// <inheritdoc/>
			public override String ToString() => $"{this.GetStringValue()} {this.Reference}";
			/// <inheritdoc/>
			public override String ToTraceText()
				=> $"{this.TypeMetadata.ElementMetadata.Signature[0] switch
				{
					UnicodePrimitiveSignatures.BooleanSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JBooleanArrayLocalRef>()),
					UnicodePrimitiveSignatures.ByteSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JByteArrayLocalRef>()),
					UnicodePrimitiveSignatures.CharSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JCharArrayLocalRef>()),
					UnicodePrimitiveSignatures.DoubleSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JDoubleArrayLocalRef>()),
					UnicodePrimitiveSignatures.FloatSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JFloatArrayLocalRef>()),
					UnicodePrimitiveSignatures.IntSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JIntArrayLocalRef>()),
					UnicodePrimitiveSignatures.LongSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JLongArrayLocalRef>()),
					UnicodePrimitiveSignatures.ShortSignatureChar => JObject.GetObjectIdentifier(this.Class.Name, this.As<JShortArrayLocalRef>()),
					_ => JObject.GetObjectIdentifier(this.Class.Name, this.As<JObjectArrayLocalRef>()),
				}} length: {this.Length}";

			/// <inheritdoc cref="Object.ToString()"/>
			private String GetStringValue()
			{
				if (this._stringValue is not null) return this._stringValue;
				Boolean matchClass = this.Class.ClassSignature.AsSpan().SequenceEqual(this.TypeMetadata.Signature);
				String elementName = this.GetElementName(out Int32 dimension);
				String value = $"{elementName}[{this.Length}]{String.Concat(Enumerable.Repeat("[]", dimension - 1))}";
				this._stringValue = matchClass ? value : $"{this.TypeMetadata.Signature} {value}";
				return this._stringValue;
			}
		}
	}
}