using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFunctions : MonoBehaviour
{
    public void Resume()
    {
        gameManager.instance.UnPauseGame();
    }
}
