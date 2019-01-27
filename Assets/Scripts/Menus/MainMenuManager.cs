using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Manages the main menu screen
public class MainMenuManager : MonoBehaviour 
{
    [SerializeField]
    private Button viewLoreButton;  // Button that sends the player to the scene where they can view the lore of the game

	// Use this for initialization
	void Start () 
	{
        // Only allow lore to be viewed if there is lore to view
        viewLoreButton.interactable = DataManager.Data.LoreViewed[0];
	}

	// Method called when the play button is pressed
	public void Play ()
	{
		if (!(DataManager.Data.CompletedLevels.Contains (true))) {
			SceneManager.LoadScene ("Level1");
		} else {
			SceneManager.LoadScene ("StageSelect");
		}
	}
    // Method called when the view lore button is pressed
    public void ViewLore ()
    {
        SceneManager.LoadScene("LoreSelect");
    }
}
