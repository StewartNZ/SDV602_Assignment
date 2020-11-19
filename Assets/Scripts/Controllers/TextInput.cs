using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input;
	//InputField.SubmitEvent se;
	//InputField.OnChangeEvent ce;
	public Text locationName;
	public Text locationStory;

	// Use this for initialization
	void Awake () {
		locationName.text = "Welcome";
        locationStory.text = "Welcome, Type 'help' to see avaliable inputs";

        input = this.GetComponent<InputField>();

    }
	
	public void UpdateLocation()
    {
		locationName.text = GameModel.currentLocale.Name;
		locationStory.text = GameModel.currentLocale.Story + "\n\nYour sensors tell you there are " + GameModel.currentLocale.NumOfPlayers + " ships in orbit";
	}

	// Update is called once per frame
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
			SubmitInput(input.text);
        }
	}
	

	private void SubmitInput(string arg0)
	{
        CommandProcessor aCmd = new CommandProcessor();
        Dictionary<string, string> output = aCmd.Parse(arg0);

		locationName.text = output["name"];
		locationStory.text = output["story"];

		input.text = "";
		input.ActivateInputField();

        Canvas.ForceUpdateCanvases();

        locationStory.transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false; // **
		locationStory.transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
	}
}
