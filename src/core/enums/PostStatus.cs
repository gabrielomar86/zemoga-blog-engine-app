namespace BlogEngineApp.core.enums
{
    /// <summary>
    /// Enum Post Status
    /// </summary>
    public enum PostStatus
    {
        /// <summary>
        /// CreatePost
        /// </summary>
        CreatePost = 0,
        /// <summary>
        /// UpdatePostToCreatedStatus
        /// </summary>
        UpdatePostToCreatedStatus = 1,
        /// <summary>
        /// PostCreated
        /// </summary>
        PostCreated = 2,
        /// <summary>
        /// UpdatePostToPendingStatus
        /// </summary>
        UpdatePostToPendingStatus = 3,
        /// <summary>
        /// PostPending
        /// </summary>
        PostPending = 4,
        /// <summary>
        /// UpdatePostToApprovedStatus
        /// </summary>
        UpdatePostToApprovedStatus = 5,
        /// <summary>
        /// PostApproved
        /// </summary>
        PostApproved = 6,
        /// <summary>
        /// UpdatePostToRejectStatus
        /// </summary>
        UpdatePostToRejectStatus = 7,
        /// <summary>
        /// PostRejected
        /// </summary>
        PostRejected = 8,
        /// <summary>
        /// PostDeleted
        /// </summary>
        PostDeleted = 9,
    }
}