
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Coder
{
    [RequireComponent(typeof(Button))]
    public class ConfirmCodeBtn : MonoBehaviour
    {
        [SerializeField] TMP_Text textField;
        private void Awake() =>
                GetComponent<Button>().onClick.AddListener(() =>
                {
                    TextConverter.ConvertText.Invoke(textField.text);
                    textField.text = string.Empty;
                    PlayerScripts.EnableMovement.Invoke();
                });
    }
}
