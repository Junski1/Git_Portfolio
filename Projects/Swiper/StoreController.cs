using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoreController : MonoBehaviour
{
    public class UnityEventColor : UnityEvent<Color> { }


    public static readonly UnityEventInt EquipBgPatternEvent = new UnityEventInt();
    public static readonly UnityEventColor SetBGEvent = new UnityEventColor();


    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject patternObjHolder = null;

    [SerializeField] private SpriteRenderer BGSprite = null;
    [SerializeField] private SpriteRenderer arrowSprite = null;

    private int storeState = 0;




    private void Awake()
    {
        //sRenderer.sprite = sprites[SaveManager.LoadData(BG)[0]];
        //sRenderer.color = sprites[SaveManager.LoadData(BG)[1]];

        EquipBgPatternEvent.AddListener((_int) => {
            ConfirmBGPatternSprite(_int);
        });

        SetBGEvent.AddListener((_color) =>
        {
            if(storeState == 0)
            {
                foreach (SpriteRenderer _elem in patternObjHolder.GetComponentsInChildren<SpriteRenderer>())
                {
                    _elem.color = _color;
                }
            }
            else if (storeState == 1)
                BGSprite.color = _color;

            else if(storeState == 2)
                arrowSprite.color = _color;
        });
    }

    private void ConfirmBGPatternSprite(int _patternI)
    {
        foreach(SpriteRenderer _elem in patternObjHolder.GetComponentsInChildren<SpriteRenderer>())
        {
            _elem.sprite = sprites[_patternI];
        }
    }

    public void SetStoreState(int _i)
    {
        storeState = _i;
    }
}
