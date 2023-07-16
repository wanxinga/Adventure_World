﻿using Managers;
using Services;
using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuild : UIWindow {

	public GameObject itemPrefab;
	public ListView listMain;
	public Transform itemRoot;
	public UIGuildInfo uiInfo;
	public UIGuildMemberItem selectedItem;

	public GameObject panelAdmin;
	public GameObject panelLeader;
	// Use this for initialization
	void Start()
	{
		this.listMain.onItemSelected += this.OnGuildMemberSelected;
		GuildService.Instance.OnGuildUpdate += UpdateUI;
		this.UpdateUI();

	}


	void OnDestroy()
	{
		GuildService.Instance.OnGuildUpdate -= UpdateUI;
	}


	void UpdateUI()
	{
		this.uiInfo.Info = GuildManager.Instance.guildInfo;
		ClearList();
		InitItems();

		this.panelAdmin.SetActive(GuildManager.Instance.myMemberInfo.Title > GuildTitle.None);
		this.panelLeader.SetActive(GuildManager.Instance.myMemberInfo.Title == GuildTitle.President);
	}


	public void OnGuildMemberSelected(ListView.ListViewItem item)
	{
		this.selectedItem = item as UIGuildMemberItem;
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
		UIManager.Instance.Show<UIGuildApplyList>();
	}

	public void OnClickLeave()
	{
		MessageBox.Show("功能开发中");
	}

	public void OnClickChat()
	{
		MessageBox.Show("功能开发中");
	}

	public void OnClickKickout()
	{
		if (selectedItem == null)
		{
			MessageBox.Show("请选择要踢出的成员");
			return;
		}
		MessageBox.Show(string.Format("确定要踢【{0}】出公会吗？", selectedItem.Info.Info.Name), "踢出公会", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
		{
			GuildService.Instance.SendAdminCommand(GuildAdminCommand.Kickout, this.selectedItem.Info.Info.Id);

		};
	}

	public void OnClickPromote()
	{
		if (selectedItem == null)
		{
			MessageBox.Show("请选择要晋升的成员");
			return;
		}
		if (selectedItem.Info.Title!=GuildTitle.None)
		{
			MessageBox.Show("对方身份已经为可晋升的最高级别");
			return;
		}
		MessageBox.Show(string.Format("确定要晋升【{0}】为副会长吗？", selectedItem.Info.Info.Name), "晋升", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
		{
			GuildService.Instance.SendAdminCommand(GuildAdminCommand.Promote, this.selectedItem.Info.Info.Id);

		};
	}

	public void OnClickDepose()
	{
		if (selectedItem == null)
		{
			MessageBox.Show("请选择要罢免的成员");
			return;
		}
		if (selectedItem.Info.Title == GuildTitle.None)
		{
			MessageBox.Show("对方无职位，无需罢免");
			return;
		}
		if (selectedItem.Info.Title == GuildTitle.President)
		{
			MessageBox.Show("会长不是你能动的");
			return;
		}
		MessageBox.Show(string.Format("确定要罢免【{0}】的公会职务吗？", selectedItem.Info.Info.Name), "罢免职务", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
		{
			GuildService.Instance.SendAdminCommand(GuildAdminCommand.Depost, this.selectedItem.Info.Info.Id);

		};
	}

	public void OnClickTransfer()
	{
		if (selectedItem == null)
		{
			MessageBox.Show("请选择一个成员");
			return;
		}
		MessageBox.Show(string.Format("确定要把会长转让给【{0}】吗？", selectedItem.Info.Info.Name), "转让会长", MessageBoxType.Confirm, "确定", "取消").OnYes = () =>
		{
			GuildService.Instance.SendAdminCommand(GuildAdminCommand.Transfer, this.selectedItem.Info.Info.Id);

		};
	}

	public void OnClickSetNotice()
	{
		MessageBox.Show("功能开发中");
	}
}
