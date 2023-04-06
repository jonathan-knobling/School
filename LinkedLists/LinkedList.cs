using System.Collections;

namespace School.LinkedLists;

public class LinkedList<T> : IList<T>
{
    private ListNode<T>? _head;
    private ListNode<T>? _tail;
    private ListNode<T>? _cur;
    
    public int Count { get; private set; }
    public bool IsReadOnly { get; private set; } = false;

    public T this[int index]
    {
        get
        {
            CheckIndexInBounds(index);
            
            var i = 0;
            while (index > i)
            {
                _cur = _cur?.Next;
                i++;
            }

            if (_cur is null) return default!;
            
            var temp = _cur.Data;
            _cur = _head;
            return temp;
        }
        set
        {
            CheckIndexInBounds(index);
            
            var i = 0;
            while (index > i)
            {
                _cur = _cur?.Next;
                i++;
            }

            _cur!.Data = value;
            _cur = _head;
        }
    }

    private void CheckIndexInBounds(int index)
    {
        if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index), "0 to " + Count, "The Index was bigger then the number of elements in the List");
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "0 to " + Count, "The Index was a negative number");
    }

    public IEnumerator<T> GetEnumerator()
    {
        while (_cur is not null)
        {
            yield return _cur.Data;
            _cur = _cur.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (_tail is not null)
        {
            _tail.Next = new ListNode<T>(item); 
            _tail = _tail.Next;
            Count++;
            return;
        }
        _tail = new ListNode<T>(item);
        _head = _tail;
        _cur = _head;
        Count++;
    }

    public void Clear()
    {
        _head = null;
        _tail = null;
        Count = 0;
    }

    public bool Contains(T item)
    {
        if (item is null) return false;
        
        while (_cur is not null)
        {
            if (_cur.Data!.Equals(item)) return true;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), nameof(arrayIndex) + " is less than 0");
        if (array is null) throw new ArgumentNullException(nameof(array), nameof(array) + " is null");
        var i = 0;
        while (_cur is not null)
        {
            if (i + arrayIndex > array.Length) throw new ArgumentException("The number of elements in the source ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
            array[arrayIndex + i] = _cur.Data;
            _cur = _cur.Next;
            i++;
        }
    }

    public bool Remove(T item)
    {
        while (_cur.Next is not null)
        {
            if (_cur.Next.Data.Equals(item))
            {
                _cur.Next = _cur.Next.Next;
                Count--;
                return true;
            }
            _cur = _cur.Next;
        }

        return false;
    }

    public int IndexOf(T item)
    {
        var i = 0;
        while (_cur is not null)
        {
            if (_cur.Data.Equals(item)) return i;
            _cur = _cur.Next;
            i++;
        }

        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index >= Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            _head = new ListNode<T>(item)
            {
                Next = _cur
            };
            _cur = _head;
            Count++;
            return;
        }
        
        var i = 0;
        while (i + 1 < index)
        {
            _cur = _cur.Next;
            i++;
        }

        var temp = new ListNode<T>(item)
        {
            Next = _cur.Next
        };
        _cur.Next = temp;
        _cur = _head;
        Count++;
    }

    public void RemoveAt(int index)
    {
        if (index >= Count || index < 0) throw new ArgumentOutOfRangeException(nameof(index));

        if (index == 0)
        {
            _head = _head.Next;
            _cur = _head;
            Count--;
            return;
        }
        
        var i = 0;
        while (i + 1 < index)
        {
            _cur = _cur.Next;
            i++;
        }

        _cur.Next = _cur.Next.Next;
        Count--;
    }

    private class ListNode<TNode>
    {
        public TNode Data { get; set; }
        public ListNode<TNode>? Next { get; set; }

        public ListNode(TNode data)
        {
            Data = data;
        }
    }
}