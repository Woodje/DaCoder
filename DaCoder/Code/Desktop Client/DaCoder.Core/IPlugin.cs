using System.ComponentModel.Composition;

namespace DaCoder.Core
{
    /// <summary>
    /// Export the Interface and all the classes which inherits this Interface
    /// </summary>
    [InheritedExport(typeof(IPlugin))]
    public interface IPlugin
    {
    }
}