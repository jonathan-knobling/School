namespace RandomShit.Queues;

public interface IQueue<T>
{
    /// <summary>
    /// Enqueues an item to the Queue
    /// </summary>
    /// <param name="item">The item to Enqueue</param>
    void Add(T item);
    
    /// <summary>
    /// Gets the first item in the Queue and dequeues it
    /// </summary>
    /// <returns>The first item in the Queue</returns>
    T? Get();
    
    /// <summary>
    /// Returns the First item in Queue without Dequeueing it
    /// </summary>
    /// <returns>The first Item in the Queue</returns>
    T? Peek();
}