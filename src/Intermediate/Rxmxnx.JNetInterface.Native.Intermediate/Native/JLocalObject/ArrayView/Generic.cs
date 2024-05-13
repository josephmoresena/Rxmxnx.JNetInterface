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
				ValidationUtilities.ThrowIfInvalidCast<TElement>(result);
			}

			/// <inheritdoc/>
			public override String ToString() => $"{this.GetStringValue()} {this.Reference}";
			/// <inheritdoc/>
			public override String ToTraceText()
				=> $"{this.Class.Name} length: {this.Length} {this.GetReferenceText()}";

			/// <inheritdoc cref="Object.ToString()"/>
			private String GetStringValue()
			{
				if (this._stringValue is null)
				{
					ReadOnlySpan<Byte> arraySignature = this.Class.ClassSignature;
					CString elementName = JArrayObject.GetElementName(arraySignature, out Int32 dimension);
					CString elementGenericName =
						JArrayObject.GetElementName(this.TypeMetadata.Signature, out Int32 genericDimension);
					String result =
						$"{elementName.ToString().Replace('/', '.')}[{this.Length}]{String.Concat(Enumerable.Repeat("[]", dimension - 1))}";
					this._stringValue =
						genericDimension != dimension || !elementGenericName.AsSpan().SequenceEqual(elementName) ?
							$"{this.TypeMetadata.Signature} {result}" :
							result;
				}
				return this._stringValue;
			}
			/// <summary>
			/// Returns a <see cref="String"/> that represents the current reference.
			/// </summary>
			/// <returns>A <see cref="String"/> that represents the current object.</returns>
			private String GetReferenceText()
				=> this.TypeMetadata.ElementMetadata.Signature[0] switch
				{
					UnicodePrimitiveSignatures.BooleanSignatureChar => this.As<JBooleanArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.ByteSignatureChar => this.As<JByteArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.CharSignatureChar => this.As<JCharArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.DoubleSignatureChar => this.As<JDoubleArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.FloatSignatureChar => this.As<JFloatArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.IntSignatureChar => this.As<JIntArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.LongSignatureChar => this.As<JLongArrayLocalRef>().ToString(),
					UnicodePrimitiveSignatures.ShortSignatureChar => this.As<JShortArrayLocalRef>().ToString(),
					_ => this.As<JObjectArrayLocalRef>().ToString(),
				};
		}
	}
}