using JornalerosApp.Shared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IGetDbServices<T> : IDisposable where T: class
    {
        Task<IReadOnlyList<T>> GetAllAsync();        
    }
}