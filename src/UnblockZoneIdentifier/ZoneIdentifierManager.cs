using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace UnblockZoneIdentifier;

public static class ZoneIdentifierManager
{
    /// <summary>
    /// Checks if the specified file is blocked by its zone (e.g., from the Internet).
    /// </summary>
    /// <param name="fileName">The full path of the file.</param>
    /// <returns>True if the file is blocked (from the Internet zone); otherwise, false.</returns>
    public static bool IsZoneBlocked(string fileName)
    {
        if (TryGetZone(fileName,
            out IPersistFile persistFile,
            out IZoneIdentifier zoneIdentifier,
            out UrlZone? zone))
        {
            try
            {
                return zone == UrlZone.Internet;
            }
            finally
            {
                Marshal.ReleaseComObject(persistFile);
                Marshal.ReleaseComObject(zoneIdentifier);
            }
        }
        return false;
    }

    /// <summary>
    /// Unlocks the zone for the specified file by setting its zone to LocalMachine.
    /// </summary>
    /// <param name="fileName">The full path of the file.</param>
    /// <returns>True if the zone was successfully unlocked; otherwise, false.</returns>
    public static bool UnblockZone(string fileName)
    {
        if (TryGetZone(fileName,
            out IPersistFile persistFile,
            out IZoneIdentifier zoneIdentifier,
            out UrlZone? zone))
        {
            if (zone != UrlZone.LocalMachine)
            {
                // zoneIdentifier.Remove doesn't work, failing with an access denied, I have no idea why.
                // Calling SetId and Save opens the alternate data stream and deletes it just fine
                zoneIdentifier.SetId(UrlZone.LocalMachine);

                try
                {
                    persistFile.Save(fileName, true);
                    return true;
                }
                catch (COMException e)
                {
                    if (e.ErrorCode == unchecked((int)0x80004005)) // AccessDenied
                    {
                        return false;
                    }
                    return false;
                }
                finally
                {
                    Marshal.ReleaseComObject(persistFile);
                    Marshal.ReleaseComObject(zoneIdentifier);
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Removes the zone identifier (alternate data stream) for the specified file.
    /// </summary>
    /// <param name="fileName">The full path of the file.</param>
    /// <returns>True if the zone identifier was successfully deleted; otherwise, false.</returns>
    public static bool RemoveZone(string fileName)
    {
        return Kernel32.DeleteFile($"{fileName}:Zone.Identifier");
    }

    /// <summary>
    /// Tries to retrieve the zone identifier for the given file.
    /// </summary>
    /// <param name="fileName">The full path of the file.</param>
    /// <param name="persistFile">The COM object to interact with the file.</param>
    /// <param name="zoneIdentifier">The COM object representing the zone identifier.</param>
    /// <param name="zone">The zone of the file (e.g., Internet, LocalMachine).</param>
    /// <returns>Returns true if the file's zone identifier was retrieved successfully; otherwise, false.</returns>
    private static bool TryGetZone(string fileName, out IPersistFile persistFile, out IZoneIdentifier zoneIdentifier, out UrlZone? zone)
    {
        fileName = Path.GetFullPath(fileName);

        if (!File.Exists(fileName))
        {
            // The file does not exist, so file is not blocked
            persistFile = null;
            zoneIdentifier = null;
            zone = null;
            return false;
        }

        var persistentZoneIdentifier = new PersistentZoneIdentifier();
        persistFile = (IPersistFile)persistentZoneIdentifier;

        try
        {
            persistFile.Load(fileName, (int)(STGM.READWRITE | STGM.SHARE_EXCLUSIVE));
        }
        catch (FileNotFoundException e)
        {
            // When calling persistFile.Load, the object tries to open filename:Zone.Identifier
            // So, if the file doesn't have an identifier, we get a file not found, and there's
            // nothing more we can do. Since we've tried to open the alternate data stream, we
            // can't seem to set the identifier, either. I think you need to use
            // IAttachmentExecute.SetSource to set the url which dictates the security zone
            _ = e;

            // The file does not exist, so file is not blocked
            zoneIdentifier = null;
            zone = null;
            return false;
        }
        catch (Exception e)
        {
            _ = e;

            // Error opening file, so file is not blocked
            zoneIdentifier = null;
            zone = null;
            return false;
        }

        zoneIdentifier = (IZoneIdentifier)persistentZoneIdentifier;
        zone = zoneIdentifier.GetId();
        return true;
    }
}
