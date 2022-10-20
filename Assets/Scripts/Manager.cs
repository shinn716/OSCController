using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class Manager : OSCManager
{
    [Space]
    [SerializeField] private GameObject groupScene1;
    [SerializeField] private Dropdown dpScene;
    [SerializeField] private InputField ifTime;
    [SerializeField] private InputField ifRemoteip;
    [SerializeField] private Text txtLog;
    [SerializeField] private Toggle togSetting;
    [SerializeField] private Button btnSendTIme;
    [SerializeField] private Button btnConnection;
    [SerializeField] private Button btnSend;
    [SerializeField] private Button btnCloseapp;

    private StringBuilder sb;
    private string scene = "ready";
    private string time = "10";
    private IConnect connect;

    private IEnumerator Start()
    {
        sb = new StringBuilder();
        connect = OSCManager.Instance;
        yield return connect.Init(connect.RemoteIP);

        ifTime.text = time;
        ifTime.onEndEdit.AddListener((string v) => { time = v; });

        ifRemoteip.text = connect.RemoteIP;
        ifRemoteip.onEndEdit.AddListener((string v) => connect.RemoteIP = v);

        btnSendTIme.onClick.AddListener(() =>
        {
            connect.SendMsg("/countdown", time);
            AddStringToLog($"{DateTime.Now} address: /countdown value: {time}");
        });
        btnConnection.onClick.AddListener(() =>
        {
            btnConnection.interactable = false;
            connect.Connect(connect.RemoteIP, () => { btnConnection.interactable = true; togSetting.isOn = false; });
        });
        btnCloseapp.onClick.AddListener(() =>
        {
            connect.SendMsg("/app", "close");
            AddStringToLog($"{DateTime.Now} address: /app value: close");
        });
        btnSend.onClick.AddListener(() =>
        {
            connect.SendMsg("/scene", scene);
            AddStringToLog($"{DateTime.Now} address: /scene value: {scene}");
        });
        dpScene.onValueChanged.AddListener((int v) =>
        {
            if (v == 0) { scene = "ready"; groupScene1.SetActive(false); }
            else if (v == 1) { scene = "scene1"; groupScene1.SetActive(true); }
            else if (v == 2) { scene = "scene2"; groupScene1.SetActive(false); }
        });
    }

    private void AddStringToLog(string _str)
    {
        sb.AppendLine(_str);
        txtLog.text = sb.ToString();
    }
}
