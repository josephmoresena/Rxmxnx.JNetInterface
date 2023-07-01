namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that implements the <see cref="IEquatable{TSelf}"/> and
/// <see cref="IComparable{TSelf}"/> interfaces.
/// </summary>
/// <typeparam name="TSelf">Type of object.</typeparam>
internal interface ISelfEquatableComparable<TSelf> : IEquatable<TSelf>, IComparable<TSelf>
	where TSelf : ISelfEquatableComparable<TSelf> { }