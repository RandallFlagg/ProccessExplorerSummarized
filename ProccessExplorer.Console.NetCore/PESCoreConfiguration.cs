using ProccessExplorer.Core.NetCore;

namespace ProccessExplorerSummarized.Interface
{
    internal class PESCoreConfiguration : IPESCoreConfiguration
    {
        public PESCoreConfiguration(long minimumSize)
        {
            this.MinimumSize = minimumSize;
        }
    }
}