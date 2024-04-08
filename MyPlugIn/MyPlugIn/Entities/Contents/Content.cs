using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace MyPlugIn.Contents
{
    public abstract class ContentBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Value { get; set; }

        protected ContentBase()
        {

        }

        public ContentBase(Guid id, string? name = null, string? value = null)
        {

            Id = id;
            Name = name;
            Value = value;
        }

    }
}