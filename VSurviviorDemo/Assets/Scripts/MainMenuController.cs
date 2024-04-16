using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public ScreenFader screenFader; // Assign in the inspector

    public void StartGame()
    {
        // Call the FadeOut function and then load the CharacterMenu scene.
        StartCoroutine(screenFader.Fade(ScreenFader.FadeDirection.In));
        // Wait for the fade to finish before loading the next scene.
        Invoke("LoadCharacterMenu", screenFader.fadeSpeed);
    }

    void LoadCharacterMenu()
    {
        SceneManager.LoadScene("CharacterMenu");
    }

    public void QuitGame()
    {
        // Log a message to the console (useful for checking functionality in the editor)
        Debug.Log("Quit game request");
        Application.Quit();
    }
}
