using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input;
	//InputField.SubmitEvent se;
	//InputField.OnChangeEvent ce;
	public Text locationName;
	public Text locationStory;

	// Use this for initialization
	void Start () {
        locationStory.text = "Welcome, Type 'help' to see avaliable inputs";

        input = this.GetComponent<InputField>();

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
		string currentText = locationStory.text;

        //  DO THIS LATER 
         CommandProcessor aCmd = new CommandProcessor();
         output.text = aCmd.Parse(arg0);

        //output.text = arg0;

		input.text = "";
		input.ActivateInputField();



	}

	private void ChangeInput( string arg0)
	{
		Debug.Log(arg0);
	}
}
