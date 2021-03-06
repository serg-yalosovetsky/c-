﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webTemplate.Models.ViewModels;
using webTemplate.Models.ViewModels.User;


namespace webTemplate.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UserEmailValidationAttribute : UniqueValidationAttribute
    {
        protected override bool CheckProperty(object obj)
        {
            if (!(obj is BaseUserView))
                return true;
            var userView = obj as BaseUserView;
            return repository.Users.Count(p => string.Compare(p.Email, userView.Email, true) == 0 && p.ID != userView.ID) == 0;
        }
    }
}