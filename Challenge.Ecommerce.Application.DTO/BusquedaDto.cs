using System.Collections.Generic;

namespace Challenge.Ecommerce.Application.DTO
{
    public class BusquedaDto
    {
        public string site_id { get; set; }
        public string country_default_time_zone { get; set; }
        public string query { get; set; }
        public Paging paging { get; set; }
        public List<Result> results { get; set; }
        public Sort sort { get; set; }
        public List<Available_Sort> available_sorts { get; set; }
        public List<Filter> filters { get; set; }
        public List<Available_Filter> available_filters { get; set; }

        public class Result
        {
            public string id { get; set; }
            public string site_id { get; set; }
            public string title { get; set; }
            public long price { get; set; }
            public Seller seller { get; set; }
        }

        public class Seller
        {
            public int id { get; set; }
            public string permalink { get; set; }
        }
        public class Paging
        {
            public int total { get; set; }
            public int primary_results { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
        }

        public class Sort
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Available_Sort
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Filter
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public List<Value> values { get; set; }
        }

        public class Value
        {
            public string id { get; set; }
            public string name { get; set; }
            public List<Path_From_Root> path_from_root { get; set; }
        }

        public class Path_From_Root
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Available_Filter
        {
            public string id { get; set; }
            public string name { get; set; }
        }

    }
}
