namespace ModerationService.Models;

public class Report
{
    private IEnumerable<ChatMessage> _messages;
    private User _reportedUser;
    private DateTime _reportTime;
    private User _reporingUser;
    private ReportStatus _status;
    private ReportCategory _category;
    private string _reportMessage;

    public Report(IEnumerable<ChatMessage> messages, User reportedUser, DateTime reportTime, User reporingUser, ReportStatus status, ReportCategory category, string reportMessage)
    {
        _messages = messages;
        _reportedUser = reportedUser;
        _reportTime = reportTime;
        _reporingUser = reporingUser;
        _status = status;
        _category = category;
        _reportMessage = reportMessage;
    }
}

public enum ReportCategory
{
    ExplicitLanguage,
    Racism,
    Spam,
}

public enum ReportStatus
//TODO Expand to include information such as who handled the report
{
    Unhandled,
    Processing,
    Handled,
}