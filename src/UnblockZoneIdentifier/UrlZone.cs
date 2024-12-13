namespace UnblockZoneIdentifier;

/// <summary>
/// Represents the security zone of a URL, used for managing security policies and permissions for files and URLs.
/// </summary>
public enum UrlZone
{
    /// <summary>
    /// Represents an invalid or unknown security zone. This value is used when the zone could not be determined.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// Represents the "Local Machine" zone. This is typically used for files and URLs located on the local computer and considered safe.
    /// </summary>
    LocalMachine = 0,

    /// <summary>
    /// Represents the "Intranet" zone. This zone is used for internal corporate networks, which are generally trusted but not as secure as the local machine zone.
    /// </summary>
    Intranet,

    /// <summary>
    /// Represents the "Trusted Sites" zone. This zone is used for websites or resources that the user has explicitly marked as trusted.
    /// </summary>
    Trusted,

    /// <summary>
    /// Represents the "Internet" zone. This is the default zone for most URLs and represents external websites or resources that are considered less trusted.
    /// </summary>
    Internet,

    /// <summary>
    /// Represents the "Untrusted" zone. This zone is used for URLs and files that are considered potentially unsafe or harmful, typically including unverified or suspicious content.
    /// </summary>
    Untrusted,
}
