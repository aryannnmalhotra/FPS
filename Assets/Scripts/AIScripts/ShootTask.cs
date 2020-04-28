﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootTask : Task
{
    private Animator anim;
    private NavMeshAgent navAgent;
    private Vector3 bulletOrigin;
    private Vector3 target;
    private ParticleSystem flash;
    private ParticleSystem smoke1;
    private ParticleSystem smoke2;
    private float shotDamage;
    private float aimTime;
    public ShootTask(TaskManager taskManager, Animator anim, NavMeshAgent navAgent, Vector3 bulletOrigin, Vector3 target, ParticleSystem flash, ParticleSystem smoke1, ParticleSystem smoke2, float shotDamage) : base(taskManager)
    {
        this.anim = anim;
        this.navAgent = navAgent;
        this.bulletOrigin = bulletOrigin;
        this.target = target;
        this.flash = flash;
        this.smoke1 = smoke1;
        this.smoke2 = smoke2;
        this.shotDamage = shotDamage;
        aimTime = Random.Range(0.5f, 1.5f);
    }
    public override bool Start()
    {
        navAgent.ResetPath();
        navAgent.isStopped = true;
        var direction = (TaskManager.gameObject.transform.position - target).normalized;
        Quaternion directionalRotaion = Quaternion.LookRotation(-(new Vector3(direction.x, 0, direction.z)));
        TaskManager.gameObject.transform.rotation = Quaternion.Slerp(TaskManager.gameObject.transform.rotation, directionalRotaion, 1);
        anim.SetBool("Aim", true);
        TaskManager.StartCoroutine(StartShooting());
        return true;
    }
    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(aimTime);
            RaycastHit shot;
            if(!TaskManager.gameObject.GetComponent<Enemy>().IsWeakerEnem)
               anim.SetBool("Fire", true);
            flash.Play();
            smoke1.Play();
            smoke2.Play();
            if (Physics.Raycast(bulletOrigin, -((bulletOrigin - target).normalized), out shot, 15))
            {
                if (shot.transform.GetComponent<FpsAttributes>() != null)
                {
                    TaskManager.gameObject.GetComponent<Enemy>().FireRange = Mathf.Clamp(TaskManager.gameObject.GetComponent<Enemy>().FireRange + 3, 6, TaskManager.gameObject.GetComponent<Enemy>().EscapeRange - 1);
                    int armourFactor = shot.transform.GetComponent<ArmourSystem>().GetArmourLevel();
                    shot.transform.GetComponent<ArmourSystem>().TakeHit();
                    shot.transform.GetComponent<HealthSystem>().DecreaseHealth(shotDamage - (armourFactor * ((5 * shotDamage) / 100)));
                }
            }
        yield return new WaitForSeconds(0.5f);
            if (!TaskManager.gameObject.GetComponent<Enemy>().IsWeakerEnem)
                anim.SetBool("Fire", false);
            flash.Stop();
            smoke1.Stop();
            smoke2.Stop();
            IsTaskComplete = true;
    }
    public override bool End()
    {
        return IsTaskComplete;
    }
}
