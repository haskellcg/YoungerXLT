using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleDB
{
    class OnLineList 
    {
        private Hashtable hashTB;

        public OnLineList()
        {
            hashTB = new Hashtable();
        }


        //用户在线时将临时信息填入哈希表
        public void OnLined(SmallUser user)
        {
            if(!hashTB.ContainsKey(user.UserID))
               hashTB.Add(user.UserID, user);
            else if (!user.IPAddress.Equals(((SmallUser)hashTB[user.UserID]).IPAddress) || user.IPPort != ((SmallUser)hashTB[user.UserID]).IPPort)
            {
                hashTB.Remove(user.UserID);
                hashTB.Add(user.UserID, user);
            }

                  
        }
        //用户下线时将临时信息从哈希表中删除
        public void OffLined(string userID)
        {
            if (IsOnLine(userID))
            {
                hashTB.Remove(userID);
            }
        }
        //根据用户ID获得用户的临时信息
        public SmallUser GetUserByID(string userID)
        {
            return (SmallUser)hashTB[userID];
        }
        //根据用户ID确认用户是否在线
        public bool IsOnLine(string userID)
        {
            return hashTB.ContainsKey(userID);
        }
        //获得哈希表长度
        public int Length()
        {
            return hashTB.Count;
        }

        public List<string> GetAllOLUsersID()
        {
            List<string> list = new List<string>();
            foreach (DictionaryEntry de in hashTB)
            {
                list.Add((string)de.Key);
            }
            return list;
        }

    }
}
