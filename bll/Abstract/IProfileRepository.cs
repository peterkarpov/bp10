using ESN.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ESN.Domain.Abstract
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> Profiles { get; }
        bool SaveProfile(Profile Profile);
        Profile DeleteProfile(Guid? ProfileId);

    }
}
