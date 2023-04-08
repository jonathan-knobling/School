using System.Net.NetworkInformation;

namespace RandomShit.Stacks;

public class MyStack
{

    public Element First;

    public void Push(Kiste kiste)
    {
        First.Push(kiste);
    }

    public Kiste Pop()
    {
        return First.Pop();
    }

}

public abstract class Element {
   
    public abstract Element Push(Kiste kiste);
    public abstract Kiste Pop();

}

public class Node : Element
{

    public Element Next;
    public Kiste Inhalt;

    public Node(Kiste kiste)
    {
        Inhalt = kiste;
    }

    public override Element Push(Kiste kiste)
    {
        return Next.Push(kiste);
    }

    public override Kiste Pop()
    {
        return Next.Pop();
    }
}

public class Kiste {
   
    public String Sorte;
    public Kiste(String s) {
        Sorte = s;
    }

}

public class EndNode : Element
{

    public Node? Previous;
    
    public override Element Push(Kiste kiste)
    {
        var newNode = new Node(kiste)
        {
            Next = this
        };

        if (Previous is not null)
        {
            Previous.Next = newNode;
        }

        Previous = newNode;

        return Previous;
    }

    public override Kiste Pop()
    {
        throw new NotImplementedException();
    }
}