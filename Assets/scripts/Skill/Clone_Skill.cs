using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject ClonePrefab;
    [SerializeField] private float CloneDuration;
    [SerializeField] private bool CanAttack;

    [SerializeField] private bool CanCreateCloneOnDashEnter;
    [SerializeField] private bool CanCreateCloneOnDashExit;
    [SerializeField] private bool CanCreateCloneOnCounterAttack;
    public void CreateClone(Transform playerTransform, Vector3 xOffset)
    {
        GameObject clone = Instantiate(ClonePrefab);
        clone.GetComponent<Clone_Skill_Controler>().SetTransform(playerTransform, CloneDuration, CanAttack, xOffset);
    }
    public void CreateCloneOnDashEnter()
    {
        if(CanCreateCloneOnDashEnter)
            CreateClone(player.transform,Vector3.zero);
    }
    public void CreateCloneOnDashExit()
    {
        if(CanCreateCloneOnDashExit)
            CreateClone(player.transform, Vector3.zero);
    }
    public void CreateCloneOnCounterAttack(Transform _transform)
    {
        if (CanCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneDelay(_transform, new Vector3(2 * player.facingDirection, 0)));
    }
    private IEnumerator CreateCloneDelay(Transform _transform,Vector3 offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform, offset);
    }
}