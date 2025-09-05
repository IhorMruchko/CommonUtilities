using System;
using CommonUtilities.Connection.IOServices;

namespace CommonUtilities.Connection.Databases
{
    /// <summary>
    /// Reads object from the JSON file.
    /// </summary>
    /// <typeparam name="TObject">Type of the read object.</typeparam>
    public class JsonObjectReader<TObject>: IObjectReader<TObject>
    {
        private readonly JsonIoService<TObject> _connection;
        private TObject _target;

        public JsonObjectReader(string directory, string fileName)
        {
            _connection = new JsonIoService<TObject>(directory, fileName);
            _target = _connection.TryRead(out var res) ? res : default;
        }

        public TObject Get()
        {
            return _target;
        }

        public void Update(Func<TObject, TObject> update)
        {
            _target = update(_target);
            _connection.Save(_target);
        }

        public void Update(Action<TObject> update)
        {
            update(_target);
            _connection.Save(_target);
        }

        public void Update(TObject newObject)
        {
            _target = newObject;
            _connection.Save(_target);
        }
    }
}