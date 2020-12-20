namespace VATrainer.DataLayer
{
    /// <summary>
    /// Encapsulates platform specific implementation of functionality concerning the database
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Gets the platform specific path of the database
        /// </summary>
        string GetDbPath();

        /// <summary>
        /// Platform specific copying of the database into an accessible directory
        /// </summary>
        void CopyDbToInternalStorage();
    }
}
