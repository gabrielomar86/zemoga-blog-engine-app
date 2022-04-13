using System;

namespace BlogEngineApp.core.presenter
{
    public class CommentPresenter
    {

        public Guid Id { get; set; }
        public string Detail { get; set; }
        public Guid PostId { get; set; }
        public string UserId { get; set; }

    }
}