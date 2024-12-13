using System.Runtime.InteropServices;

namespace UnblockZoneIdentifier;

internal static class Kernel32
{
    /// <summary>
    /// Deletes a specified file from the file system.
    /// </summary>
    /// <param name="name">The full path of the file to be deleted.</param>
    /// <returns>
    /// Returns <c>true</c> if the file was successfully deleted; otherwise, <c>false</c>.
    /// Use the <see cref="Marshal.GetLastWin32Error"/> method to obtain the error code in case of failure.
    /// </returns>
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DeleteFile(string name);
}
