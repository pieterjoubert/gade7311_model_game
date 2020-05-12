using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Owner CurrentPlayer;
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
        set { }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.TURN_CHANGED, OnTurnChanged);
        CurrentPlayer = Owner.RED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTurnChanged(EVENT_TYPE Event_Type, Component sender, object param = null)
    {
        if (CurrentPlayer == Owner.RED)
        {
            CurrentPlayer = Owner.BLUE;
        }
        else
        {
            CurrentPlayer = Owner.RED;
        }
    }
}
