using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshetTorTest
{
    public class DataObject
    {
        public Item[] items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }


    }

    public class owner
    {
        public int account_id { get; set; }
        public int reputation { get; set; }

        public int user_id { get; set; }

        public string user_type { get; set; }

        public string profile_image { get; set; }

        public string display_name { get; set; }

        public string link { get; set; }

    }

    public class Item
    {
        public owner owner { get; set; }
        public bool is_accepted { get; set; }
        public int score { get; set; }

        public int last_activity_date { get; set; }

        public int creation_date { get; set; }

        public int answer_id { get; set; }

        public int question_id { get; set; }

        public string content_license { get; set; }
    }
}
