using System;
using System.Collections.Generic;

namespace EntityModels
{
#if DB_CLASS
    [Serializable]
#endif
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}