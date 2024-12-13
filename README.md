[![NuGet](https://img.shields.io/nuget/v/UnblockZoneIdentifier.svg)](https://nuget.org/packages/UnblockZoneIdentifier) [![GitHub license](https://img.shields.io/github/license/emako/UnblockZoneIdentifier)](https://github.com/emako/UnblockZoneIdentifier/blob/master/LICENSE) [![Actions](https://github.com/emako/UnblockZoneIdentifier/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/emako/UnblockZoneIdentifier/actions/workflows/library.nuget.yml) [![Platform](https://img.shields.io/badge/platform-Windows-blue?logo=windowsxp&color=1E9BFA)](https://dotnet.microsoft.com/zh-cn/download/dotnet/latest/runtime)

# UnblockZoneIdentifier

A lightweight NuGet package designed to effortlessly remove ZoneIdentifier marks from files downloaded from the internet.

---

Internet Explorer introduced Attachment Services in Windows XP Service Pack 2. Attachment Services is a set of COM objects that email clients and browsers can use when saving and opening files downloaded from other computers. When savving the files, the client uses IAttachmentExecute.SetSource to specify the URL the file was retrieved from. This stores the URLs (Internet Explorer) internet zone in an NTFS alternate data stream, which is checked when the file is about to be opened. If this is set to an internet zone, you are prompted before the file is opened.

This is nice, and everything, except .net also uses this flag when deciding on trust levels, which can make life awkward for downloading plugins that suddenly don't work.

## Usage

```c#
using UnblockZoneIdentifier;

if (ZoneIdentifierManager.IsZoneBlocked("path/to/file")) // Check Zone.Identifier Internet
{
    ZoneIdentifierManager.UnblockZone("path/to/file"); // Change Zone.Identifier Internet to LocalMachine
    ZoneIdentifierManager.RemoveZone("path/to/file"); // Remove Zone.Identifier file
}
```

## Thanks

https://github.com/citizenmatt/UnblockZoneIdentifier

https://github.com/jstangroome/ZoneIdentifier

https://github.com/DEV-Explo-IT/UnblockZoneIdentifier