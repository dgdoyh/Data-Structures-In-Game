using System;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public Stack<Quest> questStack;

    public event Action OnQuestListUpdated;


    public QuestManager()
    {
        questStack = new Stack<Quest>();
    }

    public void AddQuest(Quest newQuest)
    {
        questStack.Push(newQuest);

        Debug.Log("NEW QUEST ADDED");

        OnQuestListUpdated?.Invoke();
    }

    public Quest GetCurrentQuest()
    {
        if (questStack.Count > 0)
        {
            return questStack.Peek();
        }
        else
        {
            return null;
        }
    }

    public void CompleteQuest()
    {
        Debug.Log("QUEST COMPLETED!");

        questStack.Pop();

        OnQuestListUpdated?.Invoke();
    }
}
