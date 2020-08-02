using JornalerosApp.Shared.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JornalerosApp.Shared.Services
{
    public interface IDbServices<T> : IGetDbServices<T> where T: class
    {
        Task<bool> AddItem(T item);
        Task<T> GetItemByname(string name);
        Task<bool> DeleteItem(string id);
        Task<T> GetItemById(string id);
        Task<bool> UpdateItem(T item);
        Task Save();        
    }
}