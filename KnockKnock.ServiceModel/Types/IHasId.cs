using System;

namespace KnockKnock.ServiceModel.Types
{
    public interface IHasId
    {
        Guid Id { get; set; }
    }
}