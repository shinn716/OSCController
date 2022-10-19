using System;
using System.Collections;

public interface IConnect
{
    string RemoteIP { get; set; }

    IEnumerator Init(string _ip, Action _action = null);

    void SendMsg(string _address, string _state);

    void Connect(string _result, Action _action = null);
}
