using JornalerosApp.Shared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IDbServices<T> : IDisposable where T: class
    {
        Task<EntityEntry<T>> AddItem(T item);
        Task<List<T>> AllItem();
        Task<EntityEntry<T>> DeleteItem(string id);
        Task<T> GetItemById(string id);
        void Save();
        EntityEntry<T> UpdateItem(string id, T item);
    }
}