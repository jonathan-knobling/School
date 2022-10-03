namespace School.Queues;

/// <summary>
/// Queue implemented using an array
/// </summary>
/// <typeparam name="T">The Type of data stored and returned by the Queue</typeparam>
public class ArrayQueue<T> : IQueue<T>
{
    private T[] _items;

    private int _head, _tail;

    public ArrayQueue()
    {
        _items = new T[8];
    }

    /// <summary>
    /// Enqueues an item to the Queue
    /// </summary>
    /// <param name="item">The item to Enqueue</param>
    public void Add(T item)
    {
        CheckIsFull();

        _items[_head] = item;
        if (_head != _items.Length - 1)
        {
            _head++;
        }
        else
        {
            _head = 0;
        }
    }

    /// <summary>
    /// Gets the first item in the Queue and dequeues it
    /// </summary>
    /// <returns>The first item in the Queue</returns>
    public T Get()
    {
        var item = _items[_tail];
        _items[_tail] = default!;

        if (_tail != _items.Length - 1)
        {
            _tail++;
        }
        else
        {
            _tail = 0;
        }
        
        return item ?? default!;
    }

    /// <summary>
    /// Returns the First item in Queue without Dequeueing it
    /// </summary>
    /// <returns>The first Item in the Queue</returns>
    public T Peek()
    {
        return _items[_tail] ?? default!;
    }

    /// <summary>
    /// Checks if the Queue is Full and Calls the DoubleSize Method if it is
    /// </summary>
    private void CheckIsFull()
    {
        if (_head < _tail && _tail - _head > 1) return;
        if (_tail - _head == 1) DoubleSize();
        
        if (_head > _tail && _head - _tail > 1) return;
        if(_head - _tail == 1) DoubleSize();
    }

    /// <summary>
    /// Doubles the Size of the array which the Queue is implemented with
    /// </summary>
    private void DoubleSize()
    {
        var newItems = new T[_items.Length * 2];
        _items.CopyTo(newItems.AsSpan());
        _items = newItems;
    }
}