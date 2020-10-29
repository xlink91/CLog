namespace CLogManagement.Web.Models.Common
{
    public class FilterOrder
    {
        public int Column { get; set; }

        public SortOrder Dir { get; set; }
    }

    public enum SortOrder
    {
        ASC,
        DESC
    }
}