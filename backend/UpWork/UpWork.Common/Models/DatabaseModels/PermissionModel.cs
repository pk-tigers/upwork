﻿using System.Text.Json.Serialization;
using UpWork.Common.Enums;

namespace UpWork.Common.Models.DatabaseModels
{
    public class PermissionModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual UserModel User { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime GrantDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }


        public bool IsActive()
        {
            return GrantDate < DateTime.UtcNow && ExpirationDate.GetValueOrDefault(DateTime.MaxValue) > DateTime.UtcNow;
        }
    }
}
