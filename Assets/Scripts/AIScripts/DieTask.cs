﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTask : Task
{
    Animator anim;
    private GameObject dieEffect;
    private GameObject cashDrop;
    private AudioSource soundPlayer;
    private AudioClip enemyDown;
    public DieTask(TaskManager taskManager, Animator anim, GameObject dieEffect, GameObject cashDrop, AudioSource soundPlayer, AudioClip enemyDown) : base(taskManager)
    {
        this.anim = anim;
        this.dieEffect = dieEffect;
        this.cashDrop = cashDrop;
        this.soundPlayer = soundPlayer;
        this.enemyDown = enemyDown;
    }
    public override bool Start()
    {
        anim.SetBool("Die", true);
        TaskManager.StartCoroutine(SpawnCollectibles());
        return true;
    }
    IEnumerator SpawnCollectibles()
    {
        yield return new WaitForSeconds(3);
        soundPlayer.PlayOneShot(enemyDown);
        FpsAttributes.EnemyCount = Mathf.Clamp(FpsAttributes.EnemyCount - 1, 0, 100);
        yield return new WaitForSeconds(1);
        TaskManager.Instantiate(dieEffect, TaskManager.gameObject.transform.position, Quaternion.LookRotation(Vector3.up));
        yield return new WaitForSeconds(0.4f);
        var go = TaskManager.Instantiate(cashDrop) as GameObject;
        go.transform.position = new Vector3(TaskManager.gameObject.transform.position.x, TaskManager.gameObject.transform.position.y + 1, TaskManager.gameObject.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        TaskManager.Destroy(TaskManager.gameObject);
    }

    public override bool End()
    {
        return false;
    }
}
