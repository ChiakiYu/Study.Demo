﻿using System.ComponentModel.DataAnnotations;

namespace EF.Model.Users
{
    /// <summary>
    ///     用户
    /// </summary>
    public class User
    {
        public User()
        {
            Password = "123123";
            PasswordFormat = UserPasswordFormat.Clear;
            IsActive = true;
        }

        #region 需持久化属性

        public int Id { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        /// <summary>
        ///     0=Clear（明文）1=标准MD5
        /// </summary>
        public UserPasswordFormat PasswordFormat { get; set; }

       /// <summary>
        ///     个人姓名 或 企业名称
        /// </summary>
        [StringLength(128)]
        public string TrueName { get; set; }

        /// <summary>
        ///     昵称
        /// </summary>
        [Required]
        [StringLength(64)]
        public string NickName { get; set; }

        /// <summary>
        ///     是否激活
        /// </summary>
        public bool IsActive { get; set; }

        #endregion
    }
}