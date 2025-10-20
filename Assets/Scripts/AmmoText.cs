using TMPro;
using UnityEditor;
using UnityEngine;
public class AmmoText : MonoBehaviour
{
    public TMP_Text ammoText;
    public Player playerScript;

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
    }

    void DisplayAmmo()
    {
        int current = playerScript.currentBoneAmmo;
        int max = playerScript.maxBoneAmmo;

        ammoText.text = current.ToString() + " / " + max.ToString();

    }


}
