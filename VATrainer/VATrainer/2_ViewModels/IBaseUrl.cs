namespace VATrainer.ViewModels
{
    /// <summary>
    /// Platform specific evaluation of the Assets folder (Android) Url or the Resources folder (iOS) Url.
    /// </summary>
    public interface IBaseUrl 
    {
        string Get(); 
    }
}
