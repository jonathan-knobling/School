namespace RandomShit.Queues;

public static class QueueTest
{
    private static void Queue()
    {
        IQueue<Dog> dogQueue = new Queue<Dog>();

        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
    }

    private static void ArrayQueue()
    {
        IQueue<Dog> dogQueue = new ArrayQueue<Dog>();
        
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());

        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());
        dogQueue.Add(new Dog());

        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
        Console.WriteLine(dogQueue.Get());
    }
}

internal class Dog
{
    public string Name { get; set; } = "Dog";
    public int Age { get; set; } = 4;

    public override string ToString()
    {
        return $"The dogs name is {Name} and hes {Age} years old!";
    }
}