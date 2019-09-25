using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainServices.OnlineGaming.WithDomainServices.Model
{
    // Domain Service interface - part of ubiquitous language
    public interface IGamingScorePolicy
    {
        void Apply(IGame game);
    }
}
