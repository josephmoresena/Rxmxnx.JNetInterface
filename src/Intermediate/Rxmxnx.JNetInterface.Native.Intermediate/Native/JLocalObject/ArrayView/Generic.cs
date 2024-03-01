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
		}
	}
}