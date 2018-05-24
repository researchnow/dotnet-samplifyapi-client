using System.Collections.Generic;
using System.Web;

namespace ResearchNow.SamplifyAPIClient
{
    public enum SortDirection
    {
        Asc = 0,
        Desc = 1
    }
    // Sort by top level fields only. Nested fields are not supported for sorting.
    public class Sort
    {
        public string Field { get; set; }
        public SortDirection Direction { get; set; }

        public Sort() { }
        public Sort(string field, SortDirection direction)
        {
            this.Field = field;
            this.Direction = direction;
        }
    }

    // Filter by top level fields only. Nested fields are not supported for filtering.
    public class Filter
    {
        public string Field { get; set; }
        public object Value { get; set; }

        public Filter() { }
        public Filter(string field, object value)
        {
            this.Field = field;
            this.Value = value;
        }
    }

    // Filtering/Sorting for GET endpoints that return an array object
    public class QueryOptions
    {
        public List<Filter> FilterBy { get; set; }
        public List<Sort> SortBy { get; set; }

        public QueryOptions()
        {
            this.FilterBy = new List<Filter>();
            this.SortBy = new List<Sort>();
        }

        public void AddFilter(string field, object value)
        {
            this.FilterBy.Add(new Filter(field, value));
        }

        public void AddSort(string field, SortDirection direction)
        {
            this.SortBy.Add(new Sort(field, direction));
        }

        public override string ToString()
        {
            string query = "?", sep = "";
            if (this.FilterBy.Count > 0)
            {
                foreach (var f in this.FilterBy)
                {
                    query = string.Format("{0}{1}{2}={3}", query, sep, f.Field, HttpUtility.UrlEncode(f.Value.ToString()));
                    sep = "&amp;";
                }
            }
            if (this.SortBy.Count > 0)
            {
                query = string.Format("{0}{1}sort=", query, sep);
                sep = "";

                foreach (var s in this.SortBy)
                {
                    query = string.Format("{0}{1}{2}:{3}", query, sep, s.Field, s.Direction);
                    sep = ",";
                }
            }
            return query;
        }
    }
}
