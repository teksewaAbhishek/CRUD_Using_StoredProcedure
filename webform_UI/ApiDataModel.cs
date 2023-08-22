using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webform_UI
{
    public class ApiDataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}