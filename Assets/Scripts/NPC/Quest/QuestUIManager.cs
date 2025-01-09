using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currQuestText;
    [SerializeField] TextMeshProUGUI questListText;

    #region Event Subscription
    private void Start()
    {
        QuestManager.Singleton.OnQuestListUpdated += UpdateQuestUI;
    }

    private void OnDisable()
    {
        QuestManager.Singleton.OnQuestListUpdated -= UpdateQuestUI;
    }
    #endregion

    public void UpdateQuestUI()
    {
        string quests = "";

        Quest currQuest = QuestManager.Singleton.GetCurrentQuest();

        if (currQuest != null)
        {
            currQuestText.text = currQuest.questName;
        }
        else
        {
            currQuestText.text = quests;
            questListText.text = string.Empty;
        }

        foreach (Quest quest in QuestManager.Singleton.questStack)
        {
            quests += (quest.questName + "\n");

            questListText.text = quests;
        }
    }
}
