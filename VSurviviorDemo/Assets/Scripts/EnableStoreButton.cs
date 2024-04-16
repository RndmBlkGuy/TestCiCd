using UnityEngine;

public class EnableStoreButton : MonoBehaviour
{
    public GameEvent enableStoreEvent;

    public void OnStoreButtonPressed()
    {
        enableStoreEvent.Raise();
    }
}
