namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	private sealed partial class RuntimeMetadataStorage
	{
		/// <summary>
		/// State for interface assignation.
		/// </summary>
		private readonly struct InterfaceAssignationState
		{
			public String FromHash { get; init; }
			public ConcurrentDictionary<AssignationKey, Boolean> AssignationCache { get; init; }
		}
	}
}