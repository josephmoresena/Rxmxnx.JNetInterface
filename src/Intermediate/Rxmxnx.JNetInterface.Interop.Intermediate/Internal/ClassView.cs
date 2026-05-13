namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class is a view of a previously loaded class but with a new reference in a JNI call.
/// </summary>
internal sealed class ClassView : JLocalObject.View<JClassObject>, ILocalObject
{
	/// <summary>
	/// Internal lifetime.
	/// </summary>
	private readonly ObjectLifetime _lifetime;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public ClassView(JClassLocalRef classRef, JClassObject jClass) : base(jClass)
		=> this._lifetime = new(jClass.Environment, classRef.Value, this, false);

	ObjectLifetime ILocalObject.Lifetime => this._lifetime;
}