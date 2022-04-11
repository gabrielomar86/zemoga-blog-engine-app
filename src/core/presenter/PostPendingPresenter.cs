using System;

namespace BlogEngineApp.core.presenter
{
    public class PostPendingPresenter
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime SubmitDate { get; set; }

    }
}