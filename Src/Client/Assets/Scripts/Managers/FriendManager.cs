using SkillBridge.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    class FriendManager:Singleton<FriendManager>
    {
        public List<NFriendInfo> allFriends;
        public Dictionary<int, NFriendInfo> Friends=new Dictionary<int, NFriendInfo>();

        public void Init(List<NFriendInfo> friends)
        {
            this.allFriends = friends;
            Friends.Clear();
            foreach(var friend in friends)
            {
                Friends[friend.friendInfo.Id] = friend;
            }
        }
    }
}
