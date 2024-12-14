using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtilities.Connection.IOServices;

namespace CommonUtilities.Connection.Databases
{
    public class JsonRepository<TEntity> : IRepository<TEntity>
    {
        private readonly JsonIoService<List<TEntity>> _connection;

        private readonly List<TEntity> _source;

        public JsonRepository(string directoryPath, string fileName)
        {
            _connection = new JsonIoService<List<TEntity>>(directoryPath, fileName);
            _source = _connection.TryRead(out var source) ? source : new List<TEntity>();
        }
        
        public void Add(TEntity element)
        {
            _source.Add(element);
        }

        public void AddAndSave(TEntity element)
        {
            Add(element);
            Save();
        }

        public void Add(IEnumerable<TEntity> elements)
        {
            _source.AddRange(elements);
        }

        public void AddAndSave(IEnumerable<TEntity> elements)
        {
            Add(elements);
            Save();
        }

        public TEntity Select(Func<TEntity, bool> selector) 
            => _source.FirstOrDefault(selector);

        public IEnumerable<TEntity> SelectAll(Func<TEntity, bool> selector = null) 
            => _source.FindAll(item => selector?.Invoke(item) ?? true);

        public TEntity SelectOrDefault(Func<TEntity, bool> selector, TEntity defaultValue = default)
        {
            var result = _source.FirstOrDefault(selector);
            return result is null ? defaultValue : result;
        }

        public bool Remove(TEntity element) 
            => _source.Remove(element);

        public bool RemoveAndSave(TEntity element)
        {
            var result = Remove(element);
            
            if (result) Save();
            
            return result;
        }

        public bool Remove(Func<TEntity, bool> selector)
        {
            var toDelete = _source.Where(selector).ToArray();
            
            if (toDelete.Length == 0) return false;
            
            foreach (var entity in toDelete)
            {
                _source.Remove(entity);
            }

            return true;
        }

        public bool RemoveAndSave(Func<TEntity, bool> selector)
        {
            var result = Remove(selector);
            
            if (result) Save();

            return result;
        }

        public bool Remove(IEnumerable<TEntity> elements = null)
        {
            if (elements is null) return false;

            var countRemoved = 0;
            
            foreach (var element in elements)
            {
                _source.Remove(element);
                countRemoved++;
            }

            return countRemoved != 0;
        }

        public bool RemoveAndSave(IEnumerable<TEntity> elements = null)
        {
            var result = Remove(elements);
            
            if (result) Save();

            return result;
        }

        public bool Update(TEntity element, TEntity newElement)
        {
            var indexToRemove = _source.IndexOf(element);
            if (indexToRemove < 0) return false;
            
            _source.RemoveAt(indexToRemove);
            _source.Insert(indexToRemove, newElement);

            return true;
        }

        public bool UpdateAndSave(TEntity element, TEntity newElement)
        {
            var result = Update(element, newElement);
            
            if (result) Save();
            
            return result;
        }

        public bool Update(Func<TEntity, bool> selector, TEntity element)
        {
            var countUpdated = 0;
            foreach (var toRemove in _source.Where(selector))
            {
                Update(toRemove, element);
                countUpdated++;
            }

            return countUpdated != 0;
        }

        public bool UpdateAndSave(Func<TEntity, bool> selector, TEntity element)
        {
            var result = Update(selector, element);
            
            if (result) Save();
            
            return result;
        }

        public bool Update(TEntity element, Func<TEntity, TEntity> modification)
        {
            var indexToRemove = _source.IndexOf(element);
            if (indexToRemove < 0) return false;
            
            var elementToReplace = modification(_source[indexToRemove]);
            
            _source.RemoveAt(indexToRemove);
            _source.Insert(indexToRemove, elementToReplace);
            return true;
        }

        public bool UpdateAndSave(TEntity element, Func<TEntity, TEntity> modification)
        {
            var result = Update(element, modification);
            
            if (result) Save();
            
            return result;
        }

        public bool Update(Func<TEntity, bool> selector, Func<TEntity, TEntity> modification)
        {
            var countUpdated = 0;
            
            foreach (var entity in _source.Where(selector))
            {
                Update(entity, modification);
                countUpdated++;
            }
            
            return countUpdated != 0;
        }

        public bool UpdateAndSave(Func<TEntity, bool> selector, Func<TEntity, TEntity> modification)
        {
            var result = Update(selector, modification);
            
            if (result) Save();
            
            return result;
        }

        public void Save() 
            => _connection.Save(_source);
    }
}