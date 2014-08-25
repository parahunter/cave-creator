using UnityEngine;
using System.Collections;

public class SeedOptions : MonoBehaviour {

    private bool displayGUI;

    private string userSeed;

    private int curSeed;
    private int CurrentSeed
    {
        get { return WorldGenerator.currentSeed; }
    }

    private bool genNewCave;
    private bool GenNewCave
    {
        get { return genNewCave; }
        set
        {
            genNewCave = value;
            if (value)
                GenerateNewCave();
        }
    }

    void Start()
    {
        userSeed = "";
    }

	bool firstFrame = false;

    void OnGUI()
    {
        if (displayGUI)
        {
			if(firstFrame)
			{
				firstFrame = false;
				GUI.FocusControl("seedField");
			}
        
            GUI.Label(new Rect(100, 100, 100, 20), "Current Seed");
            GUI.TextArea(new Rect(100, 120, 100, 20), CurrentSeed.ToString());

            GUI.Label(new Rect(100, 160, 100, 20), "Enter Seed");
           
			GUI.SetNextControlName("seedField");
            userSeed = GUI.TextField(new Rect(100, 180, 100, 20), userSeed);

            GenNewCave = GUI.Button(new Rect(100, 205, 80, 20), "Generate");
            
			if(Event.current.isKey && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
				GenNewCave = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            displayGUI = !displayGUI;
			firstFrame = true;
            Screen.lockCursor = !displayGUI;
        }
    }


    private void GenerateNewCave()
    {
        try
        {
            int seed = int.Parse(userSeed);

            GameObject messageObject = new GameObject("PortalMessage");
            PortalMessage message = messageObject.AddComponent<PortalMessage>();
            PortalMessage.message = message;
            message.oldSeed = WorldGenerator.currentSeed;
            message.newSeed = seed;

            DontDestroyOnLoad(messageObject);

            Application.LoadLevel(Application.loadedLevel);
        }
        catch
        {
            return;
        }
    }
}
