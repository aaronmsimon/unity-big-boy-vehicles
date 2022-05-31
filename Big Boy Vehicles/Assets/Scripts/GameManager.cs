using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }
    public PlayerManager PlayerManager { get; private set; }

    private void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
        PlayerManager = GetComponentInChildren<PlayerManager>();
    }

}
