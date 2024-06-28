using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelManager : ScriptableObject
{
    int lastIndex = 0;    
    public event Action OnResetBlocks;
    public event Action OnCompleteLevel;
    public event Action OnAcceptBlock;

    public void ResetLevel()
    {
        lastIndex = 0;

        OnResetBlocks = null;
        OnCompleteLevel = null;
        OnAcceptBlock = null;

        Debug.Log("Resetting Level");
    }

    public void ActivateBlock(int index)
    {
        Debug.Log("Activating block " + index);
        if(index == lastIndex + 1)
        {
            lastIndex = index;
            if(index >= 4)
            {
                OnCompleteLevel?.Invoke();
                Debug.Log("Level Complete");
            }
            else
            {
                OnAcceptBlock?.Invoke();
            }
        }
        else
        {
            OnResetBlocks?.Invoke();
            lastIndex = 0;
        }
    }
}
