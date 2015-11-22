namespace NextCMS.Core.Domain.Authen
{
    /// <summary>
    /// Represents the user login result enumeration
    /// </summary>
    public enum UserLoginResult : int
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// User dies not exist (email or username)
        /// </summary>
        UserNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// User has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// User not registered 
        /// </summary>
        NotRegistered = 6,
    }
}
