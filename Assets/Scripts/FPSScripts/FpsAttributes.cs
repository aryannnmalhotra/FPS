﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FpsAttributes : MonoBehaviour
{
    private HealthSystem healthSystem;
    private ArmourSystem armourSystem;
    private MoneySystem moneySystem;
    private FpsMovement translation;
    private InventorySystem inventory;
    public Animator Anim;
    public Text EnemyCountUI;
    public FpsController Rotation;
    public GameObject WeaponsCam;
    public AudioSource Cinematic;
    public static int EnemyCount = 100;
    public static bool IsAlive;
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        armourSystem = GetComponent<ArmourSystem>();
        moneySystem = GetComponent<MoneySystem>();
        translation = GetComponent<FpsMovement>();
        inventory = GetComponent<InventorySystem>();
        IsAlive = true;
    }
    void Update()
    {
        if (healthSystem.GetHealth() <= 0 && IsAlive)
        {
            IsAlive = false;
            healthSystem.enabled = false;
            armourSystem.enabled = false;
            moneySystem.enabled = false;
            translation.enabled = false;
            inventory.enabled = false;
            Rotation.enabled = false;
            WeaponsCam.SetActive(false);
            Anim.enabled = true;
        }
        if (IsAlive)
        {
            EnemyCountUI.text = "ENEMIES REMAINING : " + EnemyCount.ToString();
        }
        if(EnemyCount == 0)
        {
            Cinematic.Stop();
        }
    }
}
