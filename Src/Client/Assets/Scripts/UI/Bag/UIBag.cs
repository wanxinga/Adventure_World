﻿using Common.Data;
using Managers;
using Models;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIWindow {

	public Text money;

	public Transform[] pages;

	public GameObject bagItem;

	List<Image> slots;

	// Use this for initialization
	void Start () {
        if (slots == null)
        {
			slots = new List<Image>();
			for(int page = 0; page < this.pages.Length; page++)
            {
				slots.AddRange(this.pages[page].GetComponentsInChildren<Image>(true));
            }
        }
		StartCoroutine(InitBags());
	}

    IEnumerator InitBags()
    {
        this.money.text = User.Instance.CurrentCharacter.Gold.ToString();
        int j = 0;
        for (int i = 0; i < BagManager.Instance.Items.Length; i++)
        {
            var item = BagManager.Instance.Items[i];
            if (item.ItemId > 0 && ItemManager.Instance.Items[item.ItemId].Define.Type != ItemType.Ride )
            {
                GameObject go = Instantiate(bagItem, slots[j].transform);
                var ui = go.GetComponent<UIIconItem>();
                var def = ItemManager.Instance.Items[item.ItemId].Define;
                ui.SetMainIcon(def.Icon, item.Count.ToString());
                j++;
            }
        }
        for(int i = BagManager.Instance.Items.Length; i < slots.Count; i++)
        {
            slots[i].color = Color.gray;
        }
        yield return null;
    }

    void Clear()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void SetTitle(string title)
    {
        this.money.text = User.Instance.CurrentCharacter.Id.ToString();
    }

    public void OnReset()
    {
        BagManager.Instance.Reset();
        this.Clear();
        StartCoroutine(InitBags());
    }
}
