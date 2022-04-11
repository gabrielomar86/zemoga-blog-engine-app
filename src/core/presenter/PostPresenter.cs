using System;

namespace BlogEngineApp.core.presenter
{
    public class PostPresenter
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public DateTime SubmitDate { get; set; }

    }
}