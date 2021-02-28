using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCreator
{
    protected GameObject gameObject;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    protected void _Instantiate(string name)
    {
        gameObject = new GameObject(name);
    }
}
