using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Pause : PlayerAction
{
    [SerializeField] private PauseMenu pauseMenu;
    protected override void OnActionStarted(CallbackContext context)
    {
        pauseMenu.Open();
    }
}
