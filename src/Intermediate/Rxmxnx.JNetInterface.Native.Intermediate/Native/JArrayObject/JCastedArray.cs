namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// This record wraps a casted <see cref="JArrayObject"/>
	/// </summary>
	internal sealed class JCastedArray<TCastedElement> : JReferenceObject, ILocalObject, IArrayObject<TCastedElement>
		where TCastedElement : JInterfaceObject<TCastedElement>, IInterfaceType<TCastedElement>
	{
		/// <summary>
		/// Internal instance object.
		/// </summary>
		private readonly JArrayObject _array;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="arrayObject">Original <see cref="JArrayObject"/> instance.</param>
		public JCastedArray(JArrayObject arrayObject) : base(arrayObject) => this._array = arrayObject;

		public override CString ObjectClassName => this._array.ObjectClassName;
		public override CString ObjectSignature => this._array.ObjectSignature;
		IVirtualMachine ILocalObject.VirtualMachine => this._array.Environment.VirtualMachine;
		Boolean ILocalObject.IsDummy => this._array.IsDummy;
		ObjectLifetime ILocalObject.Lifetime => this._array.Lifetime;
		ObjectMetadata ILocalObject.CreateMetadata() => ILocalObject.CreateMetadata(this._array);
		void ILocalObject.ProcessMetadata(ObjectMetadata instanceMetadata)
			=> ILocalObject.ProcessMetadata(this._array, instanceMetadata);
		internal override void ClearValue() => this._array.ClearValue();
		internal override Boolean IsAssignableTo<TDataType>() => this._array.IsAssignableTo<TDataType>();
		internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
			=> this._array.SetAssignableTo<TDataType>(isAssignable);
		internal override ReadOnlySpan<Byte> AsSpan() => this._array.AsSpan();

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JCastedArray{TCastedElement}"/>
		/// to <see cref="JArrayObject{TCastedElement}"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="JCastedArray{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static explicit operator JArrayObject<TCastedElement>?(JCastedArray<TCastedElement>? castedArray)
			=> castedArray is not null ?
				castedArray._array as JArrayObject<TCastedElement> ?? new(castedArray._array) :
				default;
		/// <summary>
		/// Defines an implicit conversion of a given <see cref="JCastedArray{TCastedElement}"/>
		/// to <see cref="JArrayObject"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="JCastedArray{TCastedElement}"/> to implicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static implicit operator JArrayObject?(JCastedArray<TCastedElement>? castedArray) => castedArray?._array;
		/// <summary>
		/// Defines an implicit conversion of a given <see cref="JCastedArray{TCastedElement}"/>
		/// to <see cref="JLocalObject"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="JCastedArray{TCastedElement}"/> to implicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static implicit operator JLocalObject?(JCastedArray<TCastedElement>? castedArray) => castedArray?._array;

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JArrayObject{TCastedElement}"/>
		/// to <see cref="JCastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jArray))]
		public static implicit operator JCastedArray<TCastedElement>?(JArrayObject<TCastedElement>? jArray)
			=> jArray is not null ? new(jArray) : default;
		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JArrayObject{TCastedElement}"/>
		/// to <see cref="JCastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jArray))]
		public static implicit operator JCastedArray<TCastedElement>?(JArrayObject? jArray)
		{
			if (jArray is null) return default;
			IEnvironment env = jArray.Environment;
			ValidationUtilities.ThrowIfInvalidCast<JArrayObject<TCastedElement>>(
				env.ClassFeature.IsAssignableTo<JArrayObject<TCastedElement>>(jArray));
			return new(jArray);
		}
		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JLocalObject"/>
		/// to <see cref="JCastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jLocal))]
		public static implicit operator JCastedArray<TCastedElement>?(JLocalObject? jLocal)
		{
			if (jLocal is null) return default;
			IEnvironment env = jLocal.Environment;
			ValidationUtilities.ThrowIfInvalidCast<JArrayObject<TCastedElement>>(
				env.ClassFeature.IsAssignableTo<JArrayObject<TCastedElement>>(jLocal));
			return JArrayObject<TCastedElement>.Create(jLocal)!;
		}
	}
}