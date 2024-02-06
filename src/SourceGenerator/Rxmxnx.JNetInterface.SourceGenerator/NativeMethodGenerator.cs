using System;
using System.Diagnostics.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

[ExcludeFromCodeCoverage]
internal sealed class NativeMethodGenerator
{
	public String? TypeName { get; set; }
	public String? Documentation { get; set; }
	public String? DefinitionPrefix { get; set; }
	public String? DefinitionSuffix { get; set; }
	public String? BodyPrefix { get; set; }
	public String? BodySuffix { get; set; }
}