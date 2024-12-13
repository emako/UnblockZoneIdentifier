using System;

namespace UnblockZoneIdentifier;

[Flags]
public enum STGM : int
{
    /// <summary>
    /// Specifies that the operation is direct (i.e., the object is accessed directly without any intermediate transactions).
    /// </summary>
    DIRECT = 0x00000000,

    /// <summary>
    /// Specifies that the operation is a transacted operation (i.e., the object is part of a transaction).
    /// </summary>
    TRANSACTED = 0x00010000,

    /// <summary>
    /// Specifies that the operation is simple. This flag is generally used for non-transactional and non-complex operations.
    /// </summary>
    SIMPLE = 0x08000000,

    /// <summary>
    /// Specifies that the file or object is opened in read-only mode.
    /// </summary>
    READ = 0x00000000,

    /// <summary>
    /// Specifies that the file or object is opened in write-only mode.
    /// </summary>
    WRITE = 0x00000001,

    /// <summary>
    /// Specifies that the file or object is opened for both reading and writing.
    /// </summary>
    READWRITE = 0x00000002,

    /// <summary>
    /// Specifies that no access is denied. This flag allows read and write sharing of the object.
    /// </summary>
    SHARE_DENY_NONE = 0x00000040,

    /// <summary>
    /// Specifies that read access is denied to other processes. The object can only be written to by the caller.
    /// </summary>
    SHARE_DENY_READ = 0x00000030,

    /// <summary>
    /// Specifies that write access is denied to other processes. The object can only be read by the caller.
    /// </summary>
    SHARE_DENY_WRITE = 0x00000020,

    /// <summary>
    /// Specifies that access to the object is exclusive, meaning no other processes can access it.
    /// </summary>
    SHARE_EXCLUSIVE = 0x00000010,

    /// <summary>
    /// Specifies that the operation has priority over other operations. This is often used for high-priority tasks.
    /// </summary>
    PRIORITY = 0x00040000,

    /// <summary>
    /// Specifies that the object should be deleted when it is released or no longer in use.
    /// </summary>
    DELETEONRELEASE = 0x04000000,

    /// <summary>
    /// Specifies that no scratch space should be used during the operation. Scratch space is temporary storage used for operations.
    /// </summary>
    NOSCRATCH = 0x00100000,

    /// <summary>
    /// Specifies that the file or object should be created if it does not already exist.
    /// </summary>
    CREATE = 0x00001000,

    /// <summary>
    /// Specifies that the object should be converted to a different format if necessary.
    /// </summary>
    CONVERT = 0x00020000,

    /// <summary>
    /// Specifies that the operation should fail if the object already exists.
    /// </summary>
    FAILIFTHERE = 0x00000000,

    /// <summary>
    /// Specifies that no snapshot of the object should be made during the operation.
    /// </summary>
    NOSNAPSHOT = 0x00200000,

    /// <summary>
    /// Specifies that the operation should use a direct, single-writer multi-reader (SWMR) approach, which allows multiple processes to read but only one to write.
    /// </summary>
    DIRECT_SWMR = 0x00400000,
}
