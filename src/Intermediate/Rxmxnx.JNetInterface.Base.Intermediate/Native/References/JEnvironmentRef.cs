﻿namespace Rxmxnx.JNetInterface.Native.References;

/// <summary>
/// <c>JNIEnv</c> pointer. Represents a pointer to a <c>JNIEnv</c> object.
/// </summary>
/// <remarks>This references is valid only for the thread who owns the reference.</remarks>
public readonly partial struct JEnvironmentRef : IFixedPointer, INative<JEnvironmentRef>,
    IReadOnlyReferenceable<JEnvironmentValue>
{
    /// <inheritdoc/>
    public static JNativeType Type => JNativeType.JEnvironmentRef;

#pragma warning disable CS0649
    /// <summary>
    /// Internal pointer value.
    /// </summary>
    private readonly IntPtr _value;
#pragma warning restore CS0649

    /// <inheritdoc/>
    public IntPtr Pointer => this._value;
    /// <summary>
    /// <see langword="readonly ref"/> <see cref="JEnvironmentValue"/> from this pointer.
    /// </summary>
    public ref readonly JEnvironmentValue Reference => ref this._value.GetUnsafeReadOnlyReference<JEnvironmentValue>();
}