using System.Diagnostics.CodeAnalysis;

namespace Tests;

public class LinkedListTests
{
    [Theory]
    [InlineData(5, 5, 0)]
    [InlineData(12, 35, 15151515)]
    [InlineData(24, 40, 03030)]
    [InlineData(48, 69, 12409)]
    [InlineData(128, 196, -1)]
    public void ArrayAccess_ShouldGetTheItemAtSpecifiedIndex(int listSize, int checks, int seed)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        var reference = new int[listSize];
        
        var rnd = new Random(seed);

        for (var i = 0; i < listSize; i++)
        {
            int item = rnd.Next(-9999, 9999);
            list.Add(item);
            reference[i] = item;
        }
        
        // Act and Assert
        for (int i = 0; i < checks; i++)
        {
            int index = rnd.Next(listSize);
            list[index].Should().Be(reference[index]);
        }
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(12, 15151515)]
    [InlineData(24, 03030)]
    [InlineData(48, 12409)]
    [InlineData(128, -1)]
    [InlineData(1024, 2048)]
    public void ArrayAccesSet_ShouldReplaceItemAtSpecifiedIndex(int listSize, int seed)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        int[] reference = new int[listSize];
        
        for (int i = 0; i < listSize; i++)
        {
            list.Add(int.MinValue);
            reference[i] = int.MinValue;
        }

        var rnd = new Random(seed);
        
        // Act
        for (int i = 0; i < listSize; i++)
        {
            int item = rnd.Next(-9999, 9999);
            list[i] = item;
            reference[i] = item;
        }
        
        // Assert
        for (int i = 0; i < listSize; i++)
        {
            list[i].Should().Be(reference[i]);
        }
    }

    [Theory]
    [InlineData(32, 33)]
    [InlineData(1, 1)]
    [InlineData(48, int.MaxValue)]
    public void ArrayAccessGet_ShouldThrow_WhenIndexTooBig(int listSize, int index)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        for (int i = 0; i < listSize; i++)
        {
            list.Add(i);
        }
        
        // Act and Assert
        list.Invoking(l =>
        {
            int i = l[index];
        }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(32, 33)]
    [InlineData(1, 1)]
    [InlineData(48, int.MaxValue)]
    public void ArrayAccessSet_ShouldThrow_WhenIndexTooBig(int listSize, int index)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        for (int i = 0; i < listSize; i++)
        {
            list.Add(i);
        }
        
        // Act and Assert
        list.Invoking(l =>
        {
            return l[index] = 32;
        }).Should().Throw<ArgumentOutOfRangeException>();
    }
    
    [Theory]
    [InlineData(32, -1)]
    [InlineData(1, -9999)]
    [InlineData(48, int.MinValue)]
    public void ArrayAccessGet_ShouldThrow_WhenIndexLessThanZero(int listSize, int index)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        for (int i = 0; i < listSize; i++)
        {
            list.Add(i);
        }
        
        // Act and Assert
        list.Invoking(l =>
        {
            int i = l[index];
        }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(32, -1)]
    [InlineData(1, -9999)]
    [InlineData(48, int.MinValue)]
    public void ArrayAccessSet_ShouldThrow_WhenIndexLessThanZero(int listSize, int index)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        for (int i = 0; i < listSize; i++)
        {
            list.Add(i);
        }
        
        // Act and Assert
        list.Invoking(l =>
        {
            l[index] = 32;
        }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData()]
    [InlineData(1)]
    [InlineData(1,2,3)]
    [InlineData(-1,0,1)]
    [InlineData(1,1,1,1,1,1,1)]
    [InlineData(-9999999, 0, -1234, 9999999)]
    [InlineData(1,2,5,6,1,2,4,4,1,2,4,1,4,35,124,6,7,2,543,3,4,2,2,3,3,4,5,6,9388,12,4,1,2,2,1,0)]
    public void Item_ShouldBeAddedToEndOfList_WhenAddIsCalled(params int[] items)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act
        foreach (int item in items)
        {
            list.Add(item);
        }

        // Assert
        for (var i = 0; i < items.Length; i++)
        {
            list[i].Should().Be(items[i]);
        }
    }
    
    [Fact]
    public void AddingNull_ShouldNotThrow()
    {
        // Arrange
        IList<object> list = new RandomShit.DataStructures.LinkedList<object>();
        list.Add(-1);
        list.Add(-1);
        
        // Act and Assert
        list.Invoking(l =>
        {
            l.Add(null!);
        }).Should().NotThrow();
    }

    [Theory]
    [InlineData()]
    [InlineData(1)]
    [InlineData(1,2,3)]
    [InlineData(-1,0,1)]
    [InlineData(1,1,1,1,1,1,1)]
    [InlineData(-9999999, 0, -1234, 9999999)]
    [InlineData(1,2,5,6,1,2,4,4,1,2,4,1,4,35,124,6,7,2,543,3,4,2,2,3,3,4,5,6,9388,12,4,1,2,2,1,0)]
    public void Count_ShouldBeUpdated_WhenItemIsAdded(params int[] items)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        
        // Act
        foreach (int item in items)
        {
            list.Add(item);
        }
        
        // Assert
        list.Count.Should().Be(items.Length);
    }
    
    [Theory]
    [InlineData()]
    [InlineData(1)]
    [InlineData(1,2,3)]
    [InlineData(-1,0,1)]
    [InlineData(1,1,1,1,1,1,1)]
    [InlineData(-9999999, 0, -1234, 9999999)]
    [InlineData(1,2,5,6,1,2,4,4,1,2,4,1,4,35,124,6,7,2,543,3,4,2,2,3,3,4,5,6,9388,12,4,1,2,2,1,0)]
    public void Count_ShouldBeUpdated_WhenItemIsInserted(params int[] items)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();
        list.Add(1);
        list.Add(2);

        // Act
        foreach (int item in items)
        {
            list.Insert(1, item);
        }
        
        // Assert
        list.Count.Should().Be(items.Length + 2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1123123123)]
    [InlineData(-1565656)]
    [InlineData(-474)]
    [InlineData(5656)]
    [InlineData(0)]
    [InlineData(32)]
    public void Insert_InsertsElementAtSpecifiedIndex(int seed)
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        var rnd = new Random(seed);
        int numberOfInsertions = rnd.Next(128);

        var items = new int[numberOfInsertions];
        var indices = new int[numberOfInsertions];

        for (var i = 0; i < numberOfInsertions; i++)
        {
            items[i] = rnd.Next(int.MinValue, int.MaxValue);
            indices[i] = rnd.Next(1, i + 1);
        }
        
        // Act and Assert
        list.Add(0);
        list.Add(1);

        for (var i = 0; i < numberOfInsertions; i++)
        {
            list.Insert(indices[i], items[i]);
            list[indices[i]].Should().Be(items[i]);
        }
    }

    //TODO rewrite
    [Fact]
    public void Insert_ThrowsExceptionWhenIndexIsOutOfRange()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act & Assert
        list.Invoking(l => l.Insert(-1, 1)).Should().Throw<ArgumentOutOfRangeException>();
        list.Invoking(l => l.Insert(2, 1)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void InsertingNull_ShouldNotThrow()
    {
        // Arrange
        IList<object> list = new RandomShit.DataStructures.LinkedList<object>();
        list.Add(-1);
        list.Add(-1);
        
        // Act and Assert
        list.Invoking(l =>
        {
            l.Insert(1, null!);
        }).Should().NotThrow();
    }

    // TODO rewrite
    [Fact]
    public void RemoveAt_RemovesElementAtSpecifiedIndex()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.RemoveAt(1);

        // Assert
        list.Should().HaveCount(2);
        list[0].Should().Be(1);
        list[1].Should().Be(3);
    }

    //TODO rewrite
    [Fact]
    public void RemoveAt_ThrowsExceptionWhenIndexIsOutOfRange()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act & Assert
        list.Invoking(l => l.RemoveAt(-1)).Should().Throw<ArgumentOutOfRangeException>();
        list.Invoking(l => l.RemoveAt(1)).Should().Throw<ArgumentOutOfRangeException>();
    }
    
    // TODO Remove() updates count

    // todo rewrite
    [Fact]
    public void IndexOf_ReturnsIndexOfElement()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act
        list.Add(1);
        list.Add(2);
        list.Add(3);
        int index = list.IndexOf(2);

        // Assert
        index.Should().Be(1);
    }

    // todo rewrite
    [Fact]
    public void IndexOf_ReturnsMinusOneWhenElementNotFound()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        // Act
        list.Add(1);
        list.Add(2);
        list.Add(3);
        int index = list.IndexOf(4);

        // Assert
        index.Should().Be(-1);
    }

    // todo rewrite
    [Fact]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public void GetEnumerator_EnumeratesListCorrectly()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        // Act
        var values = new List<int>();
        foreach (int item in list)
        {
            values.Add(item);
        }

        // Assert
        values.Should().HaveCount(3);
        values[0].Should().Be(1);
        values[1].Should().Be(2);
        values[2].Should().Be(3);
    }

    // todo rewrite
    [Fact]
    public void Clear_ClearsList()
    {
        // Arrange
        IList<int> list = new RandomShit.DataStructures.LinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        // Act
        list.Clear();

        // Assert
        list.Should().HaveCount(0);
    }
    
    // todo check other generic types than integers
}