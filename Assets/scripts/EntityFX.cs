using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float FlashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;
    void Start()
    {
        sr=GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(FlashDuration);

        sr.material = originalMat;
    }
    private void RedColorblink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
