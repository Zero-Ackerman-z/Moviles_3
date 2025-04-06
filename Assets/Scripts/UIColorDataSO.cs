using UnityEngine;

[CreateAssetMenu(fileName = "UIColorData", menuName = "ScriptableObjects/UIColorData")]
public class UIColorDataSO : ScriptableObject
{
    public Color MainColor;
    public Color ConstrastColor;
    public Color TextColor;
}