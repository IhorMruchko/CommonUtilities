using System;
using System.IO;

namespace CommonUtilities.Connection.IOServices
{
    /// <summary>
    /// Interacts with the file system to retrieve and save information to the file.
    /// </summary>
    /// <typeparam name="TDataStructure">Data structure in which C# stores the data.</typeparam>
    public abstract class IoService<TDataStructure>
    {
        /// <summary>
        /// Directory of the file.
        /// </summary>
        private string DirectoryPath { get; }
        
        /// <summary>
        /// File name where data will be stored.
        /// </summary>
        protected string FileName { get; set; }

        /// <summary>
        /// Returns combined path for directory and file name.
        /// </summary>
        protected string FilePath => Path.Combine(DirectoryPath, FileName);

        /// <summary>
        /// Creates directory connection in case such directory not found. 
        /// </summary>
        /// <param name="directoryPath">Directory to store file with data.</param>
        public IoService(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            DirectoryPath = directoryPath;
        }
        
        /// <summary>
        /// Reads information from the file.
        /// </summary>
        /// <returns>Converted information to the right C# data structure.</returns>
        protected abstract TDataStructure Read();
        
        /// <summary>
        /// Saves information to the file.
        /// </summary>
        /// <param name="data">Information to save.</param>
        public abstract void Save(TDataStructure data);

        /// <summary>
        /// Tries to read information from the file.
        /// </summary>
        /// <param name="data">Information that retrieved from the file.</param>
        /// <returns>True - if information was retrieved successfully.<para/>False - otherwise.</returns>
        public bool TryRead(out TDataStructure data)
        {
            try
            {
                data = Read();
                return true;
            }
            catch (Exception)
            {
                data = default;
                return false;
            }
        }
    }
}