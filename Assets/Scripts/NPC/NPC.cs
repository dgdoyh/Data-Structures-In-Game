using UnityEngine;

public class NPC : MonoBehaviour
{
    private DialogueQueue dialogueQueue;
    private Quest quest;

    private bool questHandled = false;


    private void Start()
    {
        dialogueQueue = GetComponent<DialogueQueue>();
        quest = GetComponent<Quest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartConversation();
        } 
    }

    public void StartConversation()
    {
        Debug.Log($"{this.name}: Hey!");

        while (dialogueQueue.queue.Count > 0)
        {
            string message = dialogueQueue.queue.Dequeue().ToString();
            Debug.Log($"{this.name}: " + message);
        }

        if (!questHandled)
        {
            GiveQuest();
        }
    }

    public void GiveQuest()
    {
        QuestManager.Singleton.AddQuest(quest);
        questHandled = true;
    }
}
