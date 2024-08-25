using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.GenerativeAI.LLMClient.SemanticKernel
{
    /// <summary>
    /// Implement this interface to automatically register Kernel plugins.
    /// </summary>
    public interface IKernelPlugin
    {
        public string PluginName { get; }
    }
}
