using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OSCReceiver : MonoBehaviour
{
    public static OSCReceiver Instance;

    [SerializeField] private OSC oscReference;

    public int Minutes { get => minutes; set => minutes = value; }

    private string msg = string.Empty;
    private int minutes = 0;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        oscReference.SetAllMessageHandler(OnReceive);
    }

    private void OnReceive(OscMessage _message)
    {
        print("OnReceive: " + _message.address + " Value: " + _message.values[0].ToString());
        switch (_message.address)
        {
            case "/scene":
                msg = _message.values[0].ToString();
                if (msg.Equals("ready"))
                    LoadScene(0);
                else if (msg.Equals("scene1"))
                    LoadScene(1);
                else if (msg.Equals("scene2"))
                    LoadScene(2);
                break;
            case "/app":
                msg = _message.values[0].ToString();
                if (msg.Equals("close"))
                    Application.Quit();
                break;
            case "/countdiwn":
                msg = _message.values[0].ToString();
                int.TryParse(msg, out minutes);
                break;
        }
    }

    private void LoadScene(int _index)
    {
        print("LoadScene: " + _index);
        // TODO ...
    }
}
