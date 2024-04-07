using UnityEngine;

public class ShowHideJoiner : MonoBehaviour
{
    /*
     * USED IN UNITY!
     * This Script is used to enable and disable objects at the same time.
     */

    [SerializeField] private GameObject[] joinedObjects = new GameObject[0];

    private void OnEnable()
    {
        foreach (GameObject gameObject in joinedObjects)
            gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (GameObject gameObject in joinedObjects)
            gameObject.SetActive(false);
    }
}