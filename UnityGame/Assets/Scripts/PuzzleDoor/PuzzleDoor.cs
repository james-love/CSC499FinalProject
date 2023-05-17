using UnityEngine;
using System.Collections.Generic;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private PuzzleDoorButton[] buttonOrder;
    private List<PuzzleDoorButton> pressedButtons;

    private void Awake()
    {
        pressedButtons = new();
        foreach (var item in buttonOrder)
        {
            item.OnPuzzleButtonPressed += ButtonPressed;
        }
    }

    private void ButtonPressed(PuzzleDoorButton button)
    {
        pressedButtons.Add(button);
        if (pressedButtons.Count == buttonOrder.Length)
            CheckResult();
    }

    private void CheckResult()
    {
        if (ButtonsMatch())
        {
            Destroy(gameObject); // TODO put the animation here
        }
        else
        {
            ResetButtons();
        }
    }

    private void ResetButtons()
    {
        pressedButtons.ForEach(b => b.Reset());
        pressedButtons.Clear();
    }

    private bool ButtonsMatch()
    {
        for (int i = 0; i < pressedButtons.Count; i++)
        {
            if (pressedButtons[i] != buttonOrder[i])
                return false;
        }
        return true;
    }
}
