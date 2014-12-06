
using BusniessEntities.Models;
using Message.WebAPI.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Message.WebAPI.Services.IRepository
{
    public interface IMessageRepository
    {
         bool SendMessage(PushMessageModel messagemodel);
    }
}