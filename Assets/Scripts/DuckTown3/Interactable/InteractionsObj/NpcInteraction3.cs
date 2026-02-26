using UnityEngine;
using DuckTown3.Quest;
using DuckTown3.Core;
using DuckTown3.Dialogue;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    private bool isTalking = false;
    public InteractMode InteractMode => InteractMode.State;

    //Quest: EveryNpc have some Quest
    [SerializeField]private QuestData questData;
    [SerializeField]private DIalogueData dialogueDataDefault;

    //tmp Data
    [SerializeField] private DIalogueData diaDataNotAccept;
    [SerializeField] private DIalogueData diaDataInProgress;
    [SerializeField] private DIalogueData diaDataSummit;
    [SerializeField] private DIalogueData diaDataCompleteOrDefault;

    private QuestInstance quest;

    private void Start()
    {
        //DuckTown3.Dialogue.DialogueManager.Instance.OnDialogueEnded += Quest;
        //DuckTown3.Dialogue.DialogueManager.Instance.OnNotAccepntDialogueEnded

        //先注册任务
        quest = GetQuest();
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractPrompt()
    {
        return "这是一个可以说话的方块，按E交流";
    }

    public void Interact()
    {
        Debug.Log("一切皆善");
        //这个代码应该没人在用
        //InteractionEvents.TriggerInteractionStarted();
        //UI_DialogueManager3.instance.ShowDialoguePanel();
        isTalking = true;
        var lookDir = Duck3.instance.transform.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);


        DIalogueData dIalogue = GetDIalogueData();
        //正式对话系统
        DuckTown3.Dialogue.DialogueManager.Instance.StartTalk(dIalogue);

        //Quest();
    }

    private void Update()
    {
        if (isTalking && Vector3.Distance(transform.position, Duck3.instance.transform.position) > 5.5)
        {
            //UI_DialogueManager3.instance.CloseDialoguePanel();
            DuckTown3.Dialogue.DialogueManager.Instance.EndTalk();
            isTalking = false;
        }
    }

    private void Quest()
    {
        if (questData == null)
        {
            Debug.LogError("has no questdata");
            return;
        }

        QuestInstance quest = QuestManager.Instance.GetQuests.Find(q => q.data.questID == questData.questID);

        if (quest == null)
        {
            QuestManager.Instance.AddQuest(questData);
            //GameEvents.QuestListUpdated();
            Debug.Log($"Accepet Quest {questData.questTitle}");
        }
        else if (quest.state == QuestState.Completed)
        {
            Debug.Log("Submmit Quest, Reward Coin");
            quest.state = QuestState.Submitted;
            GameManager.Instance.GoldSystem.AddGold(questData.rewardGold);
            GameEvents.QuestSubmitted();
        }
        else if (quest.state == QuestState.Submitted)
        {
            Debug.Log($"Quest {questData.questTitle} is Submitted");
        }
        else
        {
            Debug.Log("Quest is Uncompleted");
        }
    }

    private QuestInstance GetQuest()
    {
        if (questData == null)
        {
            Debug.Log("has no questdata");
            return null;
        }

        

        var quest = QuestManager.Instance.AddQuest(questData);
        //GameEvents.QuestListUpdated();
        Debug.Log($"Add or Register Quest {questData.questTitle}");


        //quest = QuestManager.Instance.GetQuests.Find(q => q.data.questID == questData.questID);
        return quest;
    }


    private DIalogueData GetDIalogueData()
    {
        if(quest == null)
        {
            Debug.Log("has no questdata, use default Dialogue Data");
            return dialogueDataDefault;
        }

        if (quest.state == QuestState.NotAccepted)
        {
            return diaDataNotAccept;
        }
        else if (quest.state == QuestState.InProgress)
        {
            return diaDataInProgress;
        }
        else if (quest.state == QuestState.Completed)
        {
            return diaDataSummit;
        }
        else if (quest.state == QuestState.Submitted)
        {
            return diaDataCompleteOrDefault;
        }
        else
        { 
            return null;
        }
    }

}
