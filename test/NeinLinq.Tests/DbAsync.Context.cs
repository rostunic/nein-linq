﻿#if EF6

using System;
using System.Data.Entity;

namespace NeinLinq.Tests.DbAsync
{
    [DbConfigurationType(typeof(Config))]
    public class Context : DbContext
    {
        public DbSet<Dummy> Dummies { get; set; }
    }
}

#elif EF7

using System;
using Microsoft.Data.Entity;

namespace NeinLinq.Tests.DbAsync
{
    public class Context : DbContext
    {
        public DbSet<Dummy> Dummies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}

#endif
