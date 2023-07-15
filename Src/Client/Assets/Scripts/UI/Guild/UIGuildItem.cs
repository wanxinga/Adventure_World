using SkillBridge.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGuildItem : ListView.ListViewItem
{

	public Text guildName;

	public Text guildID;

	public Text leader;

	public Text memberNumber;

	public Image background;
	public Sprite normalBg;
	public Sprite selectedBg;


	public override void onSelected(bool selected)
	{
		this.background.overrideSprite = selected ? selectedBg : normalBg;
	}

	public NGuildInfo Info;



	public void SetGuildInfo(NGuildInfo item)
	{
		this.Info = item;
		if (this.guildName != null) this.guildName.text = this.Info.GuildName;
		if (this.guildID != null) this.guildID.text = this.Info.Id.ToString();
		if (this.leader != null) this.leader.text = this.Info.leaderName;
		if (this.memberNumber != null) this.memberNumber.text = this.Info.memberCount.ToString();
	}
}
