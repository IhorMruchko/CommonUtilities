using System;
using System.Collections.Generic;

namespace CommonUtilities.Connection.Databases
{
    /// <summary>
    /// Defines connection between database (system files, relative and non-relative DB).
    /// </summary>
    /// <typeparam name="TElement">Type that is retrieved. </typeparam>
    public interface IRepository<TElement>
    {
        /// <summary>
        /// Adds <paramref name="element"/> to the sequence.
        /// </summary>
        /// <param name="element">Element to add to the sequence.</param>
        void Add(TElement element);
        
        /// <summary>
        /// Adds <paramref name="element"/> to the sequence and saves updates.
        /// </summary>
        /// <param name="element">Element to add to the sequence.</param>
        void AddAndSave(TElement element);
        
        /// <summary>
        /// Add all <paramref name="elements"/> to the sequence.
        /// </summary>
        /// <param name="elements">Elements to add to the sequence.</param>
        void Add(IEnumerable<TElement> elements);
        
        /// <summary>
        /// Add all <paramref name="elements"/> to the sequence and saves updates.
        /// </summary>
        /// <param name="elements">Elements to add to the sequence.</param>
        void AddAndSave(IEnumerable<TElement> elements);
        
        /// <summary>
        /// Searches for specified element.
        /// </summary>
        /// <param name="selector">Defines which element to return.</param>
        /// <returns>Object that satisfies <paramref name="selector"/> restriction.</returns>
        TElement Select(Func<TElement, bool> selector);
        
        /// <summary>
        /// Searches for all elements.
        /// </summary>
        /// <param name="selector">Defines which elements to return. If empty - returns all elements</param>
        /// <returns>List of objects that satisfy <paramref name="selector"/> restriction.</returns>
        IEnumerable<TElement> SelectAll(Func<TElement, bool> selector = null);
        
        /// <summary>
        /// Searches for element. If not found - returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="selector">Defines which element to return.</param>
        /// <param name="defaultValue">Default value in case no elements found.</param>
        /// <returns>Element from the sequence - if element matches <paramref name="selector"/>
        /// <para/><paramref name="defaultValue"/> - otherwise.</returns>
        TElement SelectOrDefault(Func<TElement, bool> selector, TElement defaultValue = default);
        
        /// <summary>
        /// Deletes element from the source.
        /// </summary>
        /// <param name="element">Element to remove</param>
        /// <returns>True - if element is removed;<para/>False - if element does not exist.</returns>
        bool Remove(TElement element);

        /// <summary>
        /// Deletes element from the source and saves the updates.
        /// </summary>
        /// <param name="element">Element to remove</param>
        /// <returns>True - if element is removed;<para/>False - if element does not exist.</returns>
        bool RemoveAndSave(TElement element);
        
        /// <summary>
        /// Deletes all elements from the source by specified selector. 
        /// </summary>
        /// <param name="selector">Defines which elements to remove.</param>
        /// <returns>True - if at least one element was removed;<para/>False - if no elements were removed.</returns>
        bool Remove(Func<TElement, bool> selector);
        
        /// <summary>
        /// Deletes all elements from the source by specified selector and saves the updates.
        /// </summary>
        /// <param name="selector">Defines which elements to remove.</param>
        /// <returns>True - if at least one element was removed;<para/>False - if no elements were removed.</returns>
        bool RemoveAndSave(Func<TElement, bool> selector);
        
        /// <summary>
        /// Deletes all elements from the source.
        /// </summary>
        /// <param name="elements">List of elements to remove. If empty - removes all data.</param>
        /// <returns>True - if at least one element was removed;<para/>False - if no elements were removed. </returns>
        bool Remove(IEnumerable<TElement> elements = null);
        
        /// <summary>
        /// Deletes all elements from the source and saves changes.
        /// </summary>
        /// <param name="elements">List of elements to remove. If empty - removes all data.</param>
        /// <returns>True - if at least one element was removed;<para/>False - if no elements were removed. </returns>
        bool RemoveAndSave(IEnumerable<TElement> elements = null);

        /// <summary>
        /// Updates <paramref name="element"/> with <paramref name="newElement"/> values.
        /// </summary>
        /// <param name="element">Element to update.</param>
        /// <param name="newElement">Element update from.</param>
        /// <returns>True - if element was updated;<para/>False - if element was not updated. </returns>
        bool Update(TElement element, TElement newElement);
        
        /// <summary>
        /// Updates <paramref name="element"/> with <paramref name="newElement"/> values and saves changes.
        /// </summary>
        /// <param name="element">Element to update.</param>
        /// <param name="newElement">Element update from.</param>
        /// <returns>True - if element was updated;<para/>False - if element was not updated. </returns>
        bool UpdateAndSave(TElement element, TElement newElement);
        
        /// <summary>
        /// Updates all elements that match <paramref name="selector"/> with <paramref name="element"/> values.
        /// </summary>
        /// <param name="selector">Selects which elements to update.</param>
        /// <param name="element">Element update from.</param>
        /// <returns>True - if at least one element was updated;<para/>False - if no elements were updated.</returns>
        bool Update(Func<TElement, bool> selector, TElement element);
        
        /// <summary>
        /// Updates all elements that match <paramref name="selector"/> with <paramref name="element"/> values
        /// and saves changes.
        /// </summary>
        /// <param name="selector">Selects which elements to update.</param>
        /// <param name="element">Element update from.</param>
        /// <returns>True - if at least one element was updated;<para/>False - if no elements were updated.</returns>
        bool UpdateAndSave(Func<TElement, bool> selector, TElement element);
        
        /// <summary>
        /// Modifies element basing on the <paramref name="modification"/> instructions.
        /// </summary>
        /// <param name="element">Element update to.</param>
        /// <param name="modification">Instructions on how to update element.</param>
        /// <returns>True - if element was modified;<para/>False - otherwise.</returns>
        bool Update(TElement element, Func<TElement, TElement> modification);
        
        /// <summary>
        /// Modifies element basing on the <paramref name="modification"/> instructions and saves changes.
        /// </summary>
        /// <param name="element">Element update to.</param>
        /// <param name="modification">Instructions on how to update element.</param>
        /// <returns>True - if element was modified;<para/>False - otherwise.</returns>
        bool UpdateAndSave(TElement element, Func<TElement, TElement> modification);
        
        /// <summary>
        /// Updates all selected elements via <paramref name="selector"/>
        /// with instruction provided via <paramref name="modification"/>.
        /// </summary>
        /// <param name="selector">Selects all elements to modify.</param>
        /// <param name="modification">Instructions on how to modify all elements.</param>
        /// <returns>True - if at least one element was updated;<para/>False - if no elements were updated.</returns>
        bool Update(Func<TElement, bool> selector, Func<TElement, TElement> modification);
        
        /// <summary>
        /// Updates all selected elements via <paramref name="selector"/>
        /// with instruction provided via <paramref name="modification"/> and saves changes.
        /// </summary>
        /// <param name="selector">Selects all elements to modify.</param>
        /// <param name="modification">Instructions on how to modify all elements.</param>
        /// <returns>True - if at least one element was updated;<para/>False - if no elements were updated.</returns>
        bool UpdateAndSave(Func<TElement, bool> selector, Func<TElement, TElement> modification);

        /// <summary>
        /// Saves the updates.
        /// </summary>
        void Save();
    }
}