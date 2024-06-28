using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    [SerializeField] TMP_Text text;
    [SerializeField] LevelManager manager;

    Action OnAccept;
    Action OnCancel;

    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDialog(int index, Action accept, Action cancel)
    {
        text.text = $"ACTIVATE BLOCK No: {index} ?";
        dialog.SetActive(true);
        this.OnAccept = accept;
        this.OnCancel = cancel;
    }

    public void AcceptDialog()
    {
        dialog.SetActive(false);
        OnAccept();
    }

    public void CancelDialog()
    {
        dialog.SetActive(false);
        OnCancel();
    }

    public bool IsActive()
    {
        return dialog.activeSelf;
    }



}
