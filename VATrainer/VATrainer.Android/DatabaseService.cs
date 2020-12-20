using System;
using System.IO;
using VATrainer.DataLayer;
using Xamarin.Forms;

[assembly: Dependency(typeof(VATrainer.Droid.DatabaseService))]
namespace VATrainer.Droid
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
            using BinaryReader binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(dbFileName));
            using BinaryWriter binaryWriter = new BinaryWriter(new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbFileName), FileMode.Create));
            byte[] buffer = new byte[2048];
            int length = 0;
            while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
            {
                binaryWriter.Write(buffer, 0, length);
            }
        }
    }
}
