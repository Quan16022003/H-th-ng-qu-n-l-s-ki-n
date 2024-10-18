using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    /// <summary>
    /// Unit of Work Interface.
    /// </summary>
    public interface IUnitOfWork : IAsyncDisposable
    {
        #region Properties

        //ITodoRepository TodoRepository { get; }

        #endregion

        #region Methods

        Task CompleteAsync();

        #endregion
    }
}
