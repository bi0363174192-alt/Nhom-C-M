using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] private string musicGroupName;
    private void Start()
    {
        AudioManager.instance.StartBGM(musicGroupName);
    }
}
