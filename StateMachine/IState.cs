using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface IState 
{
    void OnEnter(Enemy enemy);

    void OnExcute(Enemy enemy);

    void OnExit(Enemy enemy);

}
