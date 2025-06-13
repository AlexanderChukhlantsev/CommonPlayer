using System.IO;

using NAudio;
using NAudio.Wave;

namespace CommonPlayer.Models
{
    public class TrackModel : BaseModel
    {
        #region Private fields
        private readonly string _pathToFile = string.Empty;
        public string _artist = string.Empty;
        public string _title = string.Empty;
        public TimeSpan _duration = TimeSpan.Zero;
        #endregion

        #region Constructor
        public TrackModel(string pathToFile)
        {
            if (File.Exists(pathToFile) == false)
                throw new FileNotFoundException($"Файл {Path.GetFileName(pathToFile)} не найден!");

            _pathToFile = pathToFile;
        }
        #endregion
    }
}
