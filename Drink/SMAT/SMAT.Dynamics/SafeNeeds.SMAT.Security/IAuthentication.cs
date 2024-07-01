using System;
using System.Collections.Generic;
using System.Text;

namespace SafeNeeds.DySmat.Core
{

    public enum ActionType
    {
        View,
        Add,
        Update,
        Delete,
        Other
    }

    /// <summary>
    /// ユーザー情報インタフェース
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// ユーザＩＤ
        /// </summary>
        string UserID { get; set;}

        /// <summary>
        /// ユーザ名
        /// </summary>
        string UserName { get; set;}

        /// <summary>
        /// ユーザ名他情報
        /// </summary>
        string UserDescription { get; set;}

        /// <summary>
        /// 部署
        /// </summary>
        string Department { get; set;}

        /// <summary>
        /// ロール
        /// </summary>
        string Role { get; set;}
    }
}
