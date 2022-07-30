﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IApplicationWriteDbConnection : IApplicationReadDbConnection
    {
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
