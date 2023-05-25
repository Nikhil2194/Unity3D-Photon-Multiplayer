using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageScript : MonoBehaviour
{
    public TMP_Text myMessage;
    void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }

}
