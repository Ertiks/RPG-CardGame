using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class DamagePopup : MonoBehaviour
{
    //Script pour les degats affiches a l'ecran

    public static DamagePopup Create(Vector3 position, int damageAmount, string strColor)
        //Demande une couleur en hexa
        //Static, utilisable partout. Renvoie l'instance damagePopup créé
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity); //Load via "GameAssets"

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, strColor);



        return damagePopup;
    }
    //---------------------------------------------------------------------------//

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private const float DISAPPEAR_TIMER_MAX = .5f;
    private Vector3 moveVector;


    public void Awake()
        //Init
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, string strColor)
        //SetUp avec les parametres de creation.
    {
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = DISAPPEAR_TIMER_MAX;

        textColor = UtilsClass.GetColorFromString(strColor);
        textMesh.color = textColor;

        moveVector = new Vector3(1, 1) * 3f;
    }

    private void Update()
    {
        //Deplacement du texte :
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        //Effets visuels :

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = .3f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        //Disparition du texte :
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 10f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject); 
            }
        }
    }

}
