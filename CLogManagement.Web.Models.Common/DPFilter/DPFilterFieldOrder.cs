namespace CLogManagement.Web.Models.Common.DPFilter
{
    public class DPFilterFieldOrder
    {
        public DPFilterFieldOrder() { }

        public DPFilterFieldOrder(string name, DPFilterSortOrder order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; set; }
        public DPFilterSortOrder Order { get; set; }
    }

    public enum DPFilterSortOrder
    {
        ASC,
        DESC
    }
}