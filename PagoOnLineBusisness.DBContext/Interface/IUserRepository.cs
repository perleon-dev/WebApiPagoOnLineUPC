﻿
using PagoOnLineBusisness.DBEntity.Base;
using PagoOnLineBusisness.DBEntity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagoOnLineBusisness.DBContext.Interface
{
    public interface IUserRepository
    {
        ResponseBase Insert(EntityUser user);
        ResponseBase Login(EntityLogin login);
    }
}
