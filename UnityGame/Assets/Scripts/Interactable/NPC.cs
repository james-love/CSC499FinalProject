using UnityEngine;

public class NPC : Interactable
{
    [TextArea(3, 10)]
    [SerializeField] private string npcText = string.Empty;
    [Header("Dialogue Style Overides")]
    [SerializeField] private int scrollWidth = 880;
    [SerializeField] private int scrollHeight = 320;
    [SerializeField] private int fontSize = 45;

    private DialoguePopup popup;

    public override void Interact()
    {
        popup.Display();
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.UIOpen, transform.position);
    }

    private void Awake()
    {
        popup = Instantiate(Resources.Load<DialoguePopup>("NPCDialogue"));
    }

    private void Start()
    {
        popup.SetValues(npcText, scrollWidth, scrollHeight, fontSize);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            popup.Display(false);
    }
}
