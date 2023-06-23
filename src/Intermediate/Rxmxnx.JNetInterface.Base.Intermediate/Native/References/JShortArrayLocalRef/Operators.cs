namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JShortArrayLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JShortArrayLocalRef left, JShortArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JShortArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JArrayLocalRef left, JShortArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JShortArrayLocalRef left, JShortArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JShortArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JArrayLocalRef left, JShortArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JShortArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
    /// <inheritdoc/>
    public static explicit operator JShortArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}