using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{

    private SpriteRenderer sr;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = .2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage VFX")]

    [SerializeField] private Color hitVFXColor = Color.white;
    [SerializeField] private GameObject hitVFX;



    private void Awake() // Lưu sprite renderer và material gốc
    { 
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material; 
    }


    public void CreateOnHitVFX(Transform target)
    {
        GameObject vfx =  Instantiate(hitVFX, target.position, Quaternion.identity);
        vfx.GetComponent<SpriteRenderer>().color = hitVFXColor;
    }
    public void PlayOnDamageVFX() // Hiện hiệu ứng khi nhận sát thương
    {
        if (onDamageVfxCoroutine != null)   // nếu coroutine đang chạy thì dừng nó lại
            StopCoroutine(onDamageVfxCoroutine);
        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo()); // nếu coroutine chưa chạy thì bắt đầu chạy nó
    }

    private IEnumerator OnDamageVfxCo() // Coroutine để thay đổi material trong một khoảng thời gian
    {
        sr.material = onDamageMaterial; // thay đổi material thành material hiệu ứng

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }

}
