using UnityEngine;

public class PuzzleDoorButton : Interactable
{
    private bool active = false;
    public delegate void PuzzleButtonPressed(PuzzleDoorButton button);
    public event PuzzleButtonPressed OnPuzzleButtonPressed;
    private ParticleSystem activeEffect;

    public override void Interact()
    {
        if (!active)
        {
            active = true;
            activeEffect.Play();
            OnPuzzleButtonPressed.Invoke(this);
        }
    }

    public void Reset()
    {
        active = false;
        activeEffect.Stop();
    }

    private void Awake()
    {
        activeEffect = GetComponentInChildren<ParticleSystem>();
        activeEffect.Stop();
    }
}
