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
    /// ���[�U�[���C���^�t�F�[�X
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// ���[�U�h�c
        /// </summary>
        string UserID { get; set;}

        /// <summary>
        /// ���[�U��
        /// </summary>
        string UserName { get; set;}

        /// <summary>
        /// ���[�U�������
        /// </summary>
        string UserDescription { get; set;}

        /// <summary>
        /// ����
        /// </summary>
        string Department { get; set;}

        /// <summary>
        /// ���[��
        /// </summary>
        string Role { get; set;}
    }
}
