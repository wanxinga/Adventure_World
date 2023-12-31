﻿using Managers;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopCharMenu : UIWindow,IDeselectHandler {

	public int targetId;

	public string targetName;

    public Text friendButtonText;

    public bool isAdd=true;

    public void OnDeselect(BaseEventData eventData)
    {
		var ed = eventData as PointerEventData;
		if (ed.hovered.Contains(this.gameObject))
			return;
		this.Close(WindowResult.None);
    }

    public void OnEnable()
    {
        this.GetComponent<Selectable>().Select();
        this.Root.transform.position = Input.mousePosition + new Vector3(80, 0, 0);
    }

    public void Start()
    {
        if (FriendManager.Instance.Friends.ContainsKey(targetId))
        {
            friendButtonText.text = "删除好友";
            isAdd = false;
        }
    }

    public void OnChat()
    {
        ChatManager.Instance.StartPrivateChat(targetId, targetName);
        this.Close(WindowResult.No);
    }

    public void OnAddFriend()
    {
        if (isAdd)
            FriendService.Instance.SendFriendAddRequest(targetId, targetName);
        else
            FriendService.Instance.SendFriendRemoveRequest(FriendManager.Instance.Friends[targetId].Id, targetId);
        this.Close(WindowResult.No);
    }

    public void OnInviteTeam()
    {
        TeamService.Instance.SendTeamInviteRequest(targetId, targetName);
        this.Close(WindowResult.No);
    }
}
