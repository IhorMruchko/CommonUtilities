using System;

namespace CommonUtilities.Connection.Databases
{
    /// <summary>
    /// Defines interface for the reader of the object
    /// from the external source.
    /// </summary>
    /// <typeparam name="TObject">Type of the object to read.</typeparam>
    public interface IObjectReader<TObject>
    {
        /// <summary>
        /// Reads object from the file.
        /// </summary>
        /// <returns>Current version of the object</returns>
        TObject Get();
        
        /// <summary>
        /// Updates object with the specified rule.
        /// </summary>
        /// <param name="update">Rule of the object update</param>
        void Update(Func<TObject, TObject> update);
        
        /// <summary>
        /// Changes the current object with specific rue.
        /// </summary>
        /// <param name="update">Rule of the object change</param>
        void Update(Action<TObject> update);
        
        /// <summary>
        /// Changes object for another object.
        /// </summary>
        /// <param name="newObject">New object instance to replace.</param>
        void Update(TObject newObject);
    }
}