using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPopup : MonoBehaviour
{

    //Popup Pour afficher des infos

    public static InfoPopup Create(Vector3 position, string message)
    {
        Transform infoPopupTransform = Instantiate(GameAssets.i.pfInfoPopup, position, Quaternion.identity);
        InfoPopup infoPopup = infoPopupTransform.GetComponent<InfoPopup>();


        infoPopup.Setup(message);
        return infoPopup;
    }

    private float timerDestroy = 2f;
    private TextMeshPro textMesh;

    public void Setup(string message)
    {
        textMesh = transform.Find("text").GetComponent<TextMeshPro>();
        textMesh.SetText(message);
    }

    public void Update()
    {
        timerDestroy -= Time.deltaTime;

        if (timerDestroy < 0)
        {
            Destroy(gameObject);
        }
    }

}
