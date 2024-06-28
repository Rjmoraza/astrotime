using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationBlock : MonoBehaviour, Interactable
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] Sprite baseSprite, activeSprite, highlightSprite;
    [SerializeField] int activationIndex;
    [SerializeField] DialogManager dialogManager;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        levelManager.OnResetBlocks += Reset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        sr.sprite = highlightSprite;
        dialogManager.ActivateDialog(activationIndex, 
            accept: () => {
                sr.sprite = activeSprite;
                levelManager.ActivateBlock(activationIndex);
            }, 
            cancel: () => {
                sr.sprite = baseSprite;
            });
    }

    public void Reset()
    {
        sr.sprite = baseSprite;
    }

    
}
