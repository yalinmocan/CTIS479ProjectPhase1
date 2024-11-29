using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRoleService
    {
        public IQueryable<RoleModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }
    public class RoleService : ServiceBase, IRoleService
    {
        public RoleService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Error!!");
            record.Name = record.Name.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Created!!");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(s => s.Users).SingleOrDefault(s => s.Id == id);
            if (entity == null)
                return Error("error!!");
            if (entity.Users.Any())
                return Error("has a relation!!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("deleted!!");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(s => s.Name).Select(s => new RoleModel() { Record = s });
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(s => s.Id != record.Id && s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Error!!");
            var entity = _db.Roles.SingleOrDefault(s => s.Id == record.Id);
            if (entity == null)
                return Error("not found!!");
            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("nice!!");
        }
    }
}
