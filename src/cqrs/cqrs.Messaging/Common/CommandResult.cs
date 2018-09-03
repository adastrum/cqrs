namespace cqrs.Messaging.Common
{
    public class CommandResult
    {
        public bool Succeeded { get; }
        public string Details { get; }

        private CommandResult(bool succeeded, string details)
        {
            Succeeded = succeeded;
            Details = details;
        }

        public static CommandResult Successfull()
        {
            return new CommandResult(true, string.Empty);
        }

        public static CommandResult Failed(string details)
        {
            return new CommandResult(false, details);
        }
    }
}
