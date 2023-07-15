using Managers;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuild : UIWindow {

	public GameObject itemPrefab;
	public ListView listMain;
	public Transform itemRoot;
	public UIGuildInfo uiInfo;
	public UIGuildItem selectedItem;
	// Use this for initialization
	void Start()
	{
		this.listMain.onItemSelected += this.OnGuildMemberSelected;
		GuildService.Instance.OnGuildUpdate = UpdateUI;
		this.UpdateUI();

	}


	void OnDestroy()
	{
		GuildService.Instance.OnGuildUpdate = null;
	}


	void UpdateUI()
	{
		this.uiInfo.Info = GuildManager.Instance.guildInfo;
		ClearList();
		InitItems();
	}


	public void OnGuildMemberSelected(ListView.ListViewItem item)
	{
		this.selectedItem = item as UIGuildItem;
	}


	private void InitItems()
	{
		foreach (var item in GuildManager.Instance.guildInfo.Members)
		{
			GameObject go = Instantiate(itemPrefab, this.listMain.transform);
			UIGuildMemberItem ui = go.GetComponent<UIGuildMemberItem>();
			ui.SetGuildMemberInfo(item);
			this.listMain.AddItem(ui);
		}
	}

	private void ClearList()
	{
		this.listMain.RemoveAll();
	}

	public void OnClickAppliesList()
	{
		
	}

	public void OnClickLeave()
	{

	}

	public void OnClickChat()
	{

	}

	public void OnClickKickout()
	{

	}

	public void OnClickPromote()
	{

	}

	public void OnClickDepose()
	{

	}

	public void OnClickTransfer()
	{

	}

	public void OnClickSetNotice()
	{

	}
}
