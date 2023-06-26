using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

/// <summary>
/// Native type helper.
/// </summary>
internal sealed record NativeTypeHelper
{
	/// <summary>
	/// Name of native type internal value.
	/// </summary>
	private readonly String? _internalPointerName;
	/// <summary>
	/// Indicates whether the native type is an array reference.
	/// </summary>
	private readonly Boolean _isArrRef;
	/// <summary>
	/// Indicates whether the native type is an object reference.
	/// </summary>
	private readonly Boolean _isObjRef;
	/// <summary>
	/// Native type symbol.
	/// </summary>
	private readonly ISymbol _typeSymbol;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeSymbol">Native type symbol.</param>
	/// <param name="interfaces">Native type symbol interfaces.</param>
	private NativeTypeHelper(ISymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		Boolean isPointer = interfaces.Contains("Rxmxnx.PInvoke.IFixedPointer");

		this._typeSymbol = typeSymbol;
		this._isArrRef = interfaces.Contains("Rxmxnx.JNetInterface.Internal.IArrayReference");
		this._isObjRef = this._isArrRef || interfaces.Contains("Rxmxnx.JNetInterface.Internal.IObjectReference");
		this._internalPointerName = isPointer ? typeSymbol.Name.EndsWith("Value") ? "_functions" : "_value" : default;
	}

	/// <summary>
	/// Appends the source code for current symbol.
	/// </summary>
	/// <param name="context">Generation context.</param>
	public void AddSourceCode(GeneratorExecutionContext context)
	{
		this._typeSymbol.GenerateNativeStructToString(context);
		if (this._isArrRef)
			this._typeSymbol.GenerateArrayRefOperators(context);
		if (this._isObjRef)
			this._typeSymbol.GenerateObjectRefOperators(context);
		if (this._internalPointerName is not null)
			this._typeSymbol.GenerateSelfEquatableStructOperators(context, this._internalPointerName);
	}

	/// <summary>
	/// Creates a new helper if <paramref name="typeSymbol"/> is a native type.
	/// </summary>
	/// <param name="typeSymbol">Type symbol.</param>
	/// <param name="interfaces">Type symbol interfaces.</param>
	/// <returns>Created helper instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NativeTypeHelper? Create(INamedTypeSymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		if (typeSymbol.TypeKind == TypeKind.Struct && interfaces.Contains("Rxmxnx.JNetInterface.INative"))
			return new(typeSymbol, interfaces);
		return default;
	}
}