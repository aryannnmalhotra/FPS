﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScroll : MonoBehaviour
{
    private int currentWeapon;
    public GameObject Crosshair;
    public GameObject ScopeImage;
    public Camera FpsCam;
    void Start()
    {
        currentWeapon = 0;
        SelectWeapon();
    }
    void SelectWeapon()
    {
        int i = 0; 
        foreach(Transform weapon in transform)
        {
            if (currentWeapon == i)
            {
                FpsCam.fieldOfView = 60;
                Crosshair.SetActive(true);
                ScopeImage.SetActive(false);
                gameObject.GetComponent<Camera>().enabled = true;
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeapon < 3)
                currentWeapon++;
            else
                currentWeapon = 0;
            SelectWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentWeapon > 0)
                currentWeapon--;
            else
                currentWeapon = 3;
            SelectWeapon();
        }
    }
}
