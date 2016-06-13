using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public Text gameName, gamePause;
    public Image  waitingPanel, pausePanel;

    private Player player;
    private bool muteToggle = false;
    private float nativeWidth = 1280; //1920
    private float nativeHeight = 720; //1080

    void Awake()
    {
        if (AudioListener.volume == 1)
        {
            muteToggle = false;
        }
        else
        {
            muteToggle = true;
        }
    }

	// Use this for initialization
	void Start () {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;

        player = GameObject.FindObjectOfType<Player>();

        gamePause.enabled = false;
        pausePanel.enabled = false;
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            GameEventManager.TriggerGameStart();
            player.dir = Vector3.forward;
            player.isPlaying = true;
        }            
	}

    void OnGUI()
    {
        float rx = Screen.width / nativeWidth;
        float ry = Screen.height / nativeHeight;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));

        muteToggle = GUI.Toggle(new Rect(1150, 25, 500, 500), muteToggle, "Mute");
        ToggleMute();
    }

    private void ToggleMute()
    {
        if (muteToggle == false)
        {
            AudioListener.volume = 1;
        }
        else if (muteToggle == true)
        {
            AudioListener.volume = 0;
        }
    }

    private void GameStart()
    {        
        //UI Canvas disapearing after game start trigger
        gameName.enabled = false;        
        waitingPanel.enabled = false;
    }

    private void GameOver()
    {      
        player.GameOver();
        enabled = true;
    }

    void OnDestroy()
    {
        GameEventManager.GameStart -= GameStart;
        GameEventManager.GameOver -= GameOver;
    }
}
