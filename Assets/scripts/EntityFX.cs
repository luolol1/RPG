using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("Flash info")]
    [SerializeField] private float flashDuration; //闪烁持续时间
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    [SerializeField] private Color[] IgnitedColor;
    [SerializeField] private Color[] ChilledColor;
    [SerializeField] private Color[] ShockedColor;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMaterial;
    }

    public void IgnitedFXfor(float _second)
    {
        InvokeRepeating("IgnitedFX", 0, 0.3f);
        Invoke("CancelColorChange", _second);
    }

    public void ChilledFXfor(float _second)
    {
        InvokeRepeating("ChilledFX", 0, 0.3f);
        Invoke("CancelColorChange", _second);
    }
    public void ShockedFXfor(float _second)
    {
        InvokeRepeating("ShockedFX", 0, 0.3f);
        Invoke("CancelColorChange", _second);
    }
    private void IgnitedFX()
    {
        if (sr.color != IgnitedColor[0])
            sr.color = IgnitedColor[0];
        else
            sr.color = IgnitedColor[1];
    }

    private void ChilledFX()
    {
        if (sr.color != ChilledColor[0])
            sr.color = ChilledColor[0];
        else
            sr.color = ChilledColor[1];
    }

    private void ShockedFX()
    {
        if (sr.color != ShockedColor[0])
            sr.color = ShockedColor[0];
        else
            sr.color = ShockedColor[1];
    }
    private void RedColorblink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
