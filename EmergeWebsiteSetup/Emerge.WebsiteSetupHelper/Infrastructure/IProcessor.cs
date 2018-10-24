using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.WebsiteSetupHelper.Delegates;

namespace Emerge.WebsiteSetupHelper.Infrastructure
{
    public interface IProcessor
    {
        event LogProgressHandler OnLogProgress;

        void Process();

    }
}
