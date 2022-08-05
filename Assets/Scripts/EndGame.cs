using UnityEngine;

public class EndGame : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("AudioMusic").GetComponent<AudioMusic>().EndGame();
    }
}
