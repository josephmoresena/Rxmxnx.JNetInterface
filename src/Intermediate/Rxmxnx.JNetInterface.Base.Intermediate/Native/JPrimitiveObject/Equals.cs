namespace Rxmxnx.JNetInterface.Native;

internal partial class JPrimitiveObject
{
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override Boolean Equals(Object? obj)
		=> obj is JPrimitiveObject primitive && primitive.AsSpan().SequenceEqual(this.AsSpan()) &&
			this.SizeOf == primitive.SizeOf;

	public abstract partial class Generic<TValue>
	{
		/// <inheritdoc cref="IEquatable{TValue}.Equals(TValue)"/>
		public Boolean Equals(TValue other) => this._value.Equals(other);
		/// <inheritdoc/>
		public override Boolean Equals(JObject? other)
			=> other is Generic<TValue> jPrimitive && this._value.Equals(jPrimitive._value);
		/// <inheritdoc/>
		public override Boolean Equals(Object? obj)
			=> obj is Generic<TValue> jPrimitive ? this.Equals(jPrimitive) : this.Value.Equals(obj);
	}
}

internal sealed partial class JPrimitiveObject<TPrimitive>
{
	/// <inheritdoc cref="IEquatable{TPrimitive}"/>
	public Boolean Equals(JPrimitiveObject<TPrimitive>? other) => other is not null && this.Value.Equals(other.Value);
	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
		=> other is JPrimitiveObject<TPrimitive> jPrimitive && this.Equals(jPrimitive);

	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JObject jObject ? this.Equals(jObject) : base.Equals(obj);
}