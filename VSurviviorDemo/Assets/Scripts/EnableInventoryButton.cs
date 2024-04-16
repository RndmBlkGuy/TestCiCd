using UnityEngine;

public class EnableInventoryButton : MonoBehaviour
{
    public GameEvent enableInventoryButton;

    public void OnInventoryButtonPressed()
    {
        enableInventoryButton.Raise();
    }
}
