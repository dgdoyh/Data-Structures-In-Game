using System.Collections;
using UnityEngine;

public class DialogueQueue : MonoBehaviour
{
    [SerializeField] string[] dialogueText;

    public Queue queue { get; private set; }


    private void Start()
    {
        queue = new Queue();

        foreach (string dialogue in dialogueText)
        {
            queue.Enqueue(dialogue);
        }
    }
}
