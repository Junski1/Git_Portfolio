using UnityEngine;

[CreateAssetMenu(fileName = "new BG", menuName = "BG")]
public class BGsObj : ScriptableObject
{
    public Sprite sprite;
    public string objName;
    public int unlock;
}
