﻿namespace HRMData.WEB.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPasswordRepeat { get; set; }
    }
}
