namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// This record wraps a casted <see cref="JArrayObject"/>
	/// </summary>
	internal sealed class CastedArray<TCastedElement> : View<JArrayObject>, IArrayObject<TCastedElement>
		where TCastedElement : JInterfaceObject<TCastedElement>, IInterfaceType<TCastedElement>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="arrayObject">Original <see cref="JArrayObject"/> instance.</param>
		public CastedArray(JArrayObject arrayObject) : base(arrayObject) { }

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="CastedArray{TCastedElement}"/>
		/// to <see cref="JArrayObject{TCastedElement}"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="CastedArray{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static explicit operator JArrayObject<TCastedElement>?(CastedArray<TCastedElement>? castedArray)
			=> castedArray is not null ?
				castedArray.Object as JArrayObject<TCastedElement> ?? new(castedArray.Object) :
				default;
		/// <summary>
		/// Defines an implicit conversion of a given <see cref="CastedArray{TCastedElement}"/>
		/// to <see cref="JArrayObject"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="CastedArray{TCastedElement}"/> to implicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static implicit operator JArrayObject?(CastedArray<TCastedElement>? castedArray) => castedArray?.Object;
		/// <summary>
		/// Defines an implicit conversion of a given <see cref="CastedArray{TCastedElement}"/>
		/// to <see cref="JLocalObject"/>.
		/// </summary>
		/// <param name="castedArray">A <see cref="CastedArray{TCastedElement}"/> to implicitly convert.</param>
		[return: NotNullIfNotNull(nameof(castedArray))]
		public static implicit operator JLocalObject?(CastedArray<TCastedElement>? castedArray) => castedArray?.Object;

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JArrayObject{TCastedElement}"/>
		/// to <see cref="CastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jArray))]
		public static implicit operator CastedArray<TCastedElement>?(JArrayObject<TCastedElement>? jArray)
			=> jArray is not null ? new(jArray) : default;
		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JArrayObject{TCastedElement}"/>
		/// to <see cref="CastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jArray))]
		public static implicit operator CastedArray<TCastedElement>?(JArrayObject? jArray)
		{
			if (jArray is null) return default;
			IEnvironment env = jArray.Environment;
			ValidationUtilities.ThrowIfInvalidCast<JArrayObject<TCastedElement>>(
				env.ClassFeature.IsAssignableTo<JArrayObject<TCastedElement>>(jArray));
			return new(jArray);
		}
		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JLocalObject"/>
		/// to <see cref="CastedArray{TCastedElement}"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JArrayObject{TCastedElement}"/> to explicitly convert.</param>
		[return: NotNullIfNotNull(nameof(jLocal))]
		public static implicit operator CastedArray<TCastedElement>?(JLocalObject? jLocal)
		{
			if (jLocal is null) return default;
			IEnvironment env = jLocal.Environment;
			ValidationUtilities.ThrowIfInvalidCast<JArrayObject<TCastedElement>>(
				env.ClassFeature.IsAssignableTo<JArrayObject<TCastedElement>>(jLocal));
			return jLocal.CastTo<JArrayObject<TCastedElement>>();
		}
	}
}