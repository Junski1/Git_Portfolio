using UnityEngine;
using static Coder.GameManager;
using CodeType = Coder.GameManager.CodeType;

namespace Coder
{
    public class CodableObject : MonoBehaviour
    {
        private enum checkType
        {
            Sphere,
            Box
        }
        [SerializeField] private LayerMask playerMask;

        [SerializeField] private checkType type = checkType.Sphere;
        [SerializeField] private Vector3 checkSize = Vector3.one;

       


        [SerializeField] private CodeType codeType = CodeType.All;
        public CodeType CodeType => codeType;


        void Update()
        {
            InitiateCoding();
        }

        private void InitiateCoding()
        {
            if (!CheckForPlayer(type))
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                CanvasManager.ShowCanvas.Invoke("CodeTextCanvas");
                TextConverter.GetCodableObj.Invoke(gameObject);
                PlayerScripts.DisableMovement.Invoke();
            }
        }

        private bool CheckForPlayer(checkType _type)
        {
            if (_type == checkType.Sphere)
                return Physics.CheckSphere(transform.position, checkSize.x, playerMask);

            return Physics.CheckBox(transform.position, checkSize, Quaternion.identity, playerMask);
        }

        private void OnDrawGizmos()
        {
            if (type == checkType.Sphere)
                Gizmos.DrawWireSphere(transform.position, checkSize.x);
            else
                Gizmos.DrawWireCube(transform.position, checkSize);
        }
    }
}

