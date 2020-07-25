using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace App.Examples
{
    public class TrashChannel<T> : ITrashReader<T>, ITrashWriter<T>
    {
        private bool _isCompleted;
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        public void Write(T item)
        {
            _queue.Enqueue(item);
            _semaphore.Release();
        }

        public async Task<(bool, T)> TryReadAsync()
        {
            await _semaphore.WaitAsync();
            var found = _queue.TryDequeue(out var item);
            return (found, item);
        }

        public async Task<T> ReadAsync()
        {
            await _semaphore.WaitAsync();
            _queue.TryDequeue(out var item);
            return item;
        }

        public void Complete()
        {
            _isCompleted = true;
            _semaphore.Release();
        }

        public bool IsComplete() => _isCompleted && _queue.IsEmpty;
    }

    public interface ITrashReader<T>
    {
        Task<(bool, T)> TryReadAsync();
        Task<T> ReadAsync();
        bool IsComplete();
    }

    public interface ITrashWriter<T>
    {
        void Write(T msg);
        void Complete();
    }
}