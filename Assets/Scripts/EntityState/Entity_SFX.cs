using UnityEngine;

public class Entity_SFX : MonoBehaviour
{
    private AudioSource audioSource;
    [Header(" SFX Names")]
    [SerializeField] private string attackHit;
    [SerializeField] private string attackMiss;



    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    public void playAttackHit()
    {
        AudioManager.instance.PlaySFX(attackHit, audioSource);
    }
    public void playAttackMiss()
    {
        AudioManager.instance.PlaySFX(attackMiss, audioSource);
    }
}
