using System;
using BlogEngineApp.core.enums;
using MediatR;

namespace BlogEngineApp.services
{
    public class CreationPostFlowCommand : INotification
    {
        public PostStatus Status { get; set; }
        public object Payload { get; set; }
    }

}

