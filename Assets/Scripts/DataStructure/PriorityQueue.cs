using System;

public class PriorityQueue
{
    private GameEvent head;
    public int Count { get; set; }

    public PriorityQueue()
    {
        head = null;
        Count = 0;
    }

    // Add a new GameEvent at a proper space based on its priority (high - low)
    public void Enqueue(GameEvent newEvent)
    {
        // If there's no head OR head's priority is smaller than the newEvent's priority,
        if (IsEmpty() || head.Priority <= newEvent.Priority)
        {
            // Put the newEvent at the front (head)
            newEvent.Next = head;
            head = newEvent;
        }
        // If there's already a head AND its priority is bigger than the newEvent's priority,
        else
        {
            GameEvent currEvent = head;

            // Check GameEvents until it finds a proper place to put a newEvent (based on its priority)
            while (currEvent.Next != null && currEvent.Next.Priority >= newEvent.Priority)
            {
                currEvent = currEvent.Next;
            }

            newEvent.Next = currEvent.Next;
            currEvent.Next = newEvent;
        }

        Count++;
    }

    public GameEvent Peek()
    {
        return head;
    }

    // Remove the first Event (head, the highest priority)
    public GameEvent Dequeue()
    {
        if (IsEmpty())
        {
            Console.WriteLine("There's no data to remove in the queue.");
            return null;
        }

        GameEvent prevHead = head;
        head = head.Next;

        Count--;

        return prevHead;
    }

    // Check if this queue is empty
    private bool IsEmpty()
    {
        return head == null;
    }
}
