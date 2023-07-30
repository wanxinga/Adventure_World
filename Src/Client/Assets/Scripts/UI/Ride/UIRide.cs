using Managers;
using Models;
using SkillBridge.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRide : UIWindow {

	public Text descript;
	public GameObject itemPrefab;
	public ListView listMain;
	public UIRideItem selectedItem;
	public Text buttonText;

	// Use this for initialization
	void Start()
	{
		RefreshUI();
		this.listMain.onItemSelected += this.OnItemSelected;
	}

	void OnDestroy()
	{
	}

	public void OnItemSelected(ListView.ListViewItem item)
	{
		this.selectedItem = item as UIRideItem;
		this.descript.text = this.selectedItem.item.Define.Description;
        if (this.selectedItem.item.Id == User.Instance.CurrentRide)
        {
			this.buttonText.text = "离开坐骑";
        }
        else
        {
			this.buttonText.text = "召唤坐骑";
		}
	}

	void RefreshUI()
	{
		ClearItems();
		InitItems();
	}

    private void InitItems()
    {
		foreach (var kv in ItemManager.Instance.Items)
		{
			if (kv.Value.Define.Type == ItemType.Ride && (kv.Value.Define.LimitClass ==CharacterClass.None|| kv.Value.Define.LimitClass == User.Instance.CurrentCharacter.Class))
			{
				if (EquipManager.Instance.Contains(kv.Key))
					continue;
				GameObject go = Instantiate(itemPrefab, this.listMain.transform);
				UIRideItem ui = go.GetComponent<UIRideItem>();
				ui.SetRideItem(kv.Value, this, false);
				this.listMain.AddItem(ui);
			}
		}
	}

    private void ClearItems()
    {
		this.listMain.RemoveAll();
    }

	public void DoRide()
	{
		if (selectedItem == null)
		{
			MessageBox.Show("请选择要召唤的坐骑","提示");
			return;
		}
		int currentRideId = User.Instance.CurrentRide;
		if (currentRideId == 0)
        {
			buttonText.text = "离开坐骑";
        }
		else if (currentRideId == this.selectedItem.item.Id)
        {
			buttonText.text = "召唤坐骑";
		}
        else
        {
			buttonText.text = "离开坐骑";
		}
		User.Instance.Ride(this.selectedItem.item.Id);
	}
}
