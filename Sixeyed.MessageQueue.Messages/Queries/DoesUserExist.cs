namespace Sixeyed.MessageQueue.Messages.Queries
{
    public class DoesUserExistRequest
    {
        public string EmailAddress { get; set; }
    }

    public class DoesUserExistResponse
    {
        public bool Exists { get; set; }
    }
}
