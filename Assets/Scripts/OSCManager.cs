using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour, IConnect
{
    public static OSCManager Instance;

    [SerializeField] private GameObject OSCPrefab;

    private OSC oscReference;
    private GameObject oscObj = null;
    private string remoteIP = "127.0.0.1";


    private void Awake()
    {
        Instance = this;
        var tip = PlayerPrefs.GetString("remoteIP");
        if (tip != string.Empty)
            remoteIP = tip;
    }


    // interface
    public string RemoteIP { get => remoteIP; set => remoteIP = value; }
    public void SendMsg(string _address, string _state)
    {
        OscMessage message = new OscMessage();
        message.address = _address;
        message.values.Add(_state);
        oscReference.Send(message);
    }
    public void Connect(string _result, Action _action = null)
    {
        StartCoroutine(Init(_result, _action));
        PlayerPrefs.SetString("remoteIP", _result);
    }
    public IEnumerator Init(string _ip, Action _action = null)
    {
        if (oscObj != null)
        {
            oscReference = null;
            Destroy(oscObj);
        }
        yield return null;
        OSCPrefab.GetComponent<OSC>().outIP = _ip;
        oscObj = Instantiate(OSCPrefab);
        oscReference = oscObj.GetComponent<OSC>();

        if (_action != null)
            _action.Invoke();
    }
}
