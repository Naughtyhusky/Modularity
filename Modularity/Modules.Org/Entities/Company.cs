using Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.Entities
{
    public class Company : EntityBase
    {
        public Company(string name, string description, long createUserId, string createUserName) : base(createUserId, createUserName)
        {
            Name = name;
            Description = description;
        }

        protected Company() { }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
