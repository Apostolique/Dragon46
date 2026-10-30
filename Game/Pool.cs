using System.Collections.Generic;

namespace GameProject {
    /// <summary>
    /// Holds on to objects that are done so they can be handed out again instead of allocating
    /// new ones. Whoever gets an object resets it.
    /// </summary>
    public class Pool<T> where T : class, new() {
        public T Get() => _free.Count > 0 ? _free.Pop() : new T();

        public void Free(T item) => _free.Push(item);

        readonly Stack<T> _free = new Stack<T>();
    }
}
