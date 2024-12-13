using System.Diagnostics;
using UnblockZoneIdentifier;

if (ZoneIdentifierManager.IsZoneBlocked("DICOM.pptx"))
{
    ZoneIdentifierManager.UnblockZone("DICOM.pptx");
    ZoneIdentifierManager.RemoveZone("DICOM.pptx");
}

Debugger.Break();
