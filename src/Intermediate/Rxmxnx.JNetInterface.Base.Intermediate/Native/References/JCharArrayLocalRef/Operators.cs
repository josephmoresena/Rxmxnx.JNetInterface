namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JCharArrayLocalRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JCharArrayLocalRef left, JCharArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JCharArrayLocalRef left, JArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator ==(JArrayLocalRef left, JCharArrayLocalRef right) => left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JCharArrayLocalRef left, JCharArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JCharArrayLocalRef left, JArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static Boolean operator !=(JArrayLocalRef left, JCharArrayLocalRef right) => !left.Equals(right);
    /// <inheritdoc/>
    public static explicit operator JCharArrayLocalRef(JObjectLocalRef objRef) => new(objRef);
    /// <inheritdoc/>
    public static explicit operator JCharArrayLocalRef(JArrayLocalRef arrayRef) => new(arrayRef);
}