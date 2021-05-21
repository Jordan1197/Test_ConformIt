﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Authentication
{
    public class ApiKey
    {
        public int Id { get; }
        public string Owner { get; }
        public string Key { get; }
        public DateTime Created { get; }
        public IReadOnlyCollection<string> Roles { get; }

        //constructeur
        public ApiKey(int id,string owner, string key,DateTime created, IReadOnlyCollection<string> roles)
        {
            Id = id;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key ?? throw new ArgumentNullException(nameof(owner));
            Created = created;
            Roles = roles ?? throw new ArgumentNullException(nameof(roles));
        }
    }
}
