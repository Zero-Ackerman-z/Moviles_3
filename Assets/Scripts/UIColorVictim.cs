using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ColorVictimType { MainType, ContrastType, TextType}
public class UIColorVictim : MonoBehaviour
{
    public UIColorDataSO UIColorData;

    public ColorVictimType ColorVictimType;
    void Start()
    {
        switch (ColorVictimType)
        {
            case ColorVictimType.MainType:

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().color = UIColorData.MainColor;
                }
                if(GetComponent<TextMeshProUGUI>() != null)
                {
                    GetComponent<TextMeshProUGUI>().color = UIColorData.MainColor;
                }

                break;

            case ColorVictimType.ContrastType:

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().color = UIColorData.ConstrastColor;
                }
                if (GetComponent<TextMeshProUGUI>() != null)
                {
                    GetComponent<TextMeshProUGUI>().color = UIColorData.ConstrastColor;
                }

                break;

            case ColorVictimType.TextType:

                if (GetComponent<Image>() != null)
                {
                    GetComponent<Image>().color = UIColorData.TextColor;
                }
                if (GetComponent<TextMeshProUGUI>() != null)
                {
                    GetComponent<TextMeshProUGUI>().color = UIColorData.TextColor;
                }

                break;
        }
    }
}