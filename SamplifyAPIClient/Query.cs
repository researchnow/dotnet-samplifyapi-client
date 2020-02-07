using System.Collections.Generic;
using System.Web;

namespace Dynata.SamplifyAPIClient
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
        public SortDirection Direction
        {
            set
            {
                switch (value)
                {
                    case SortDirection.Asc:
                        this.DirectionToken = "asc";
                        break;
                    case SortDirection.Desc:
                        this.DirectionToken = "desc";
                        break;
                    default:
                        this.DirectionToken = "asc";
                        break;
                }
            }
        }
        internal string DirectionToken { get; set; }

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

        public Filter(string field, object value)
        {
            this.Field = field;
            this.Value = value;
        }
    }

    // Filtering/Sorting and pagination params for GET endpoints that return an array object
    public class QueryOptions
    {
        private const uint _maxLimit = 50;

        public List<Filter> FilterBy { get; set; }
        public List<Sort> SortBy { get; set; }
        public uint Offset { get; set; }
        public uint Limit { get; set; }

        public QueryOptions()
        {
            this.FilterBy = new List<Filter>();
            this.SortBy = new List<Sort>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dynata.SamplifyAPIClient.QueryOptions"/> class with
        /// Offset and Limit values. Maximum `Limit` value is 50.
        /// </summary>
        /// <param name="offset">Offset.</param>
        /// <param name="limit">Max `Limit` value is 50</param>
        public QueryOptions(uint offset, uint limit)
        {
            this.FilterBy = new List<Filter>();
            this.SortBy = new List<Sort>();
            this.Offset = offset;
            this.Limit = limit;
        }

        public QueryOptions AddFilter(string field, object value)
        {
            this.FilterBy.Add(new Filter(field, value));
            return this;
        }

        public QueryOptions AddSort(string field, SortDirection direction)
        {
            this.SortBy.Add(new Sort(field, direction));
            return this;
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
                    query = string.Format("{0}{1}{2}:{3}", query, sep, s.Field, s.DirectionToken);
                    sep = ",";
                }
            }
            if (sep != "") { sep = "&amp;"; }
            if (this.Offset > 0)
            {
                query = string.Format("{0}{1}offset={2}", query, sep, this.Offset);
                sep = "&amp;";
            }
            if (this.Limit > 0)
            {
                if (this.Limit > _maxLimit) { this.Limit = _maxLimit; }
                query = string.Format("{0}{1}limit={2}", query, sep, this.Limit);
            }
            return query;
        }
    }
}
