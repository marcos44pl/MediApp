using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Entities
{
    [Serializable]
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}