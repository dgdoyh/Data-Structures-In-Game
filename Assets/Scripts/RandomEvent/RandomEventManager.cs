using System.Collections;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    private PriorityQueue eventQueue;
    private float eventCoolTime = 5f;

    private void Start()
    {
        eventQueue = new PriorityQueue();

        eventQueue.Enqueue(new GameEvent("Event p1", 10, 1));
        eventQueue.Enqueue(new GameEvent("Event p3", 10, 3));
        eventQueue.Enqueue(new GameEvent("Event p2", 10, 2));

        ProcessEvents();
    }

    public void ProcessEvents()
    {
        if (eventQueue.Count > 0)
        {
            GameEvent gameEvent = eventQueue.Dequeue();

            StartCoroutine(GameEventProgression(gameEvent));
        }
    }

    IEnumerator GameEventProgression(GameEvent gameEvent)
    {
        yield return new WaitForSeconds(eventCoolTime);

        Debug.Log(gameEvent.EventName + " Time !! Yay !!");
        yield return new WaitForSeconds(gameEvent.EventTime);

        Debug.Log("Event ends.");
        ProcessEvents();
    }
}
