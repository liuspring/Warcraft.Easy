﻿using System.Web;

namespace TaskManager.Commands.Dto
{
    public class CommandListInput:DataTablesRequest
    {
        public CommandListInput(HttpRequestBase request) : base(request)
        {
        }

        public CommandListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
