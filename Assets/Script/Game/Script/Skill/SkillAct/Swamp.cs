﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : SkillBase {

    public float slowPercentage = 3.3f;
    float Iconrotation = 0;
    public List<PlayerControlThree> colliderList;

    public override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }


    protected override IEnumerator ActivityDuringDurationTime()
    {
        SkillSoundEffect("SkillEffect_Swamp", durationTime);
        yield return base.ActivityDuringDurationTime();
        this.gameObject.GetComponent<Collider>().enabled = false;
        if (colliderList.Count != 0)
        {
            foreach (PlayerControlThree i in colliderList)
            {
                i.GetPMI().Speed *= slowPercentage;
            }
        }
    }

    void TargetIconRotate()
    {
        Iconrotation += 10f * Time.deltaTime;
        
        this.transform.localRotation = Quaternion.Euler(90, Iconrotation, 0);
    }

    Quaternion targetRotation(Vector3 target)
    {
        Quaternion Q = Quaternion.FromToRotation(this.transform.position, target) * SkillParent.transform.rotation;
        return Quaternion.Euler(Q.eulerAngles);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Head")
        {
            colliderList.Add(other.GetComponent<PlayerControlThree>());
            other.gameObject.GetComponent<PlayerControlThree>().GetPMI().Speed /= slowPercentage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Head")
        {
            other.gameObject.GetComponent<PlayerControlThree>().GetPMI().Speed *= slowPercentage;
            colliderList.Remove(other.GetComponent<PlayerControlThree>());
        }
    }

    public override void SetPivot(Transform pivot, Transform pivotRotation, float angle, Vector3 skillVector)
    {
        base.SetPivot(pivot, pivotRotation, angle, skillVector);
        SkillParent.transform.rotation = targetRotation(skillVector);
    }

    public override float ShowPreCooltime()
    {
        return base.ShowPreCooltime();
    }

    public override bool GetIsSkillNeedGuideLine()
    {
        return base.GetIsSkillNeedGuideLine();
    }

    private void Update()
    {
        TargetIconRotate();
    }
}
