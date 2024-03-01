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
			public override String ToString() => this._stringValue ??= this.GetStringValue();

			/// <inheritdoc cref="Object.ToString()"/>
			private String GetStringValue()
			{
				ReadOnlySpan<Byte> arraySignature = this.Class.ClassSignature;
				CString elementName = JArrayObject.GetElementName(arraySignature, out Int32 dimension);
				CString elementGenericName =
					JArrayObject.GetElementName(this.TypeMetadata.Signature, out Int32 genericDimension);
				String result =
					$"{elementName.ToString().Replace('/', '.')}[{this.Length}]{String.Concat(Enumerable.Repeat("[]", dimension - 1))}";
				return genericDimension != dimension || !elementGenericName.AsSpan().SequenceEqual(elementName) ?
					$"{this.TypeMetadata.Signature} {result}" :
					result;
			}
		}
	}
}