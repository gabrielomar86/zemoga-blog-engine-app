namespace BlogEngineApp.core.enums
{
    /// <summary>
    /// Enum Post Status
    /// </summary>
    public enum PostStatus
    {
        /// <summary>
        /// Create
        /// </summary>
        CreatePost = 0,
        /// <summary>
        /// Created
        /// </summary>
        UpdatePostToCreatedStatus = 1,
        /// <summary>
        /// Created
        /// </summary>
        PostCreated = 2,
        /// <summary>
        /// UpdatePostToPendingStatus
        /// </summary>
        UpdatePostToPendingStatus = 3,
        /// <summary>
        /// PostChangeToPendingStatus
        /// </summary>
        PostPending = 4,
        /// <summary>
        /// UpdatePostToApprovedStatus
        /// </summary>
        UpdatePostToApprovedStatus = 5,
        /// <summary>
        /// PostChangedToApprovedStatus
        /// </summary>
        PostApproved = 6,
        /// <summary>
        /// UpdatePostToRejectStatus
        /// </summary>
        UpdatePostToRejectStatus = 7,
        /// <summary>
        /// PostChangedToRejectedStatus
        /// </summary>
        PostRejected = 8,
    }
}