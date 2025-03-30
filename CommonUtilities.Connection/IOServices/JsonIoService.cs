using System.IO;
using Newtonsoft.Json;


// TODO: make Update array acceptable
namespace CommonUtilities.Connection.IOServices
{
    /// <summary>
    /// Represents connection to the .json file
    /// </summary>
    /// <typeparam name="TDataStructure">Data structure in which C# stores the data.</typeparam>
    public class JsonIoService<TDataStructure> : IoService<TDataStructure>
    {
        /// <summary>
        /// Settings on how to store information into the .json file.
        /// </summary>
        private readonly JsonSerializerSettings _setting = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
        
        /// <summary>
        /// Creates directory and file in case they are not present on the machine. 
        /// </summary>
        /// <param name="directoryPath">Directory path where file will be stored.</param>
        /// <param name="fileName">File name. Could be without .json extension.</param>
        public JsonIoService(string directoryPath, string fileName) : base(directoryPath)
        {
            FileName = fileName;

            if (Path.GetExtension(FileName) != ".json")
            {
                FileName = $"{Path.GetFileName(FileName)}.json";
            }

            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }
        }

        protected override TDataStructure Read()
            => JsonConvert.DeserializeObject<TDataStructure>(File.ReadAllText(FilePath), _setting);

        public override void Save(TDataStructure data) 
            => File.WriteAllText(FilePath, JsonConvert.SerializeObject(data, _setting));
    }
}