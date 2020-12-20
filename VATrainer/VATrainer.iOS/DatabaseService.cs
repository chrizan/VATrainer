using Foundation;
using System;
using System.IO;
using VATrainer.DataLayer;
using Xamarin.Forms;

[assembly: Dependency(typeof(VATrainer.iOS.DatabaseService))]
namespace VATrainer.iOS
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseConstants.DbFileName);

        public DatabaseService() { }

        public void CopyDbToInternalStorage()
        {
            if (!File.Exists(dbPath))
            {
                CopyDbFile(DatabaseConstants.DbFileName);
                CopyDbFile(DatabaseConstants.DbFileName_shm);
                CopyDbFile(DatabaseConstants.DbFileName_wal);
            }
        }

        public string GetDbPath()
        {
            return dbPath;
        }

        private void CopyDbFile(string dbFileName)
        {
            string sourceFileName = Path.Combine(NSBundle.MainBundle.ResourcePath, dbFileName);
            string destFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbFileName);
            File.Copy(sourceFileName, destFileName);
        }
    }
}