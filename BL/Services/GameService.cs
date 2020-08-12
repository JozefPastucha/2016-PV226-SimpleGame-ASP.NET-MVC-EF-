using Riganti.Utils.Infrastructure.Core;

namespace BL.Services
{
    /// <summary>
    /// Base class for all game services
    /// </summary>
    public abstract class GameService
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
    }
}
