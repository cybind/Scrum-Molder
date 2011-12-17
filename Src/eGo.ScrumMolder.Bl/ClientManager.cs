using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Data;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl
{
    public class ClientManager : IClientManager
    {
        private readonly ScrumMolderContext _context = new ScrumMolderContext();

        public IEnumerable<Client> GetAll()
        {
            try
            {
                return _context.Clients;
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public bool Update(Client client)
        {
            try
            {
                _context.Clients.Attach(client);
                
                foreach (var project in client.Projects)
                {
                    if (_context.Projects.Any(p => p.Id == project.Id))
                    {
                        _context.Projects.Attach(project);
                        _context.Entry(project).State = System.Data.EntityState.Modified;
                        //_context.SaveChanges();
                    }
                    else
                    {
                        _context.Projects.Add(project);
                        _context.Entry(project).State = System.Data.EntityState.Added;
                    }
                }

                _context.Entry(client).State = System.Data.EntityState.Modified;

                if (_context.SaveChanges() == 0)
                    return false;
                
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return false;
            }

            return true;
        }

        public Client Get(Guid clientId)
        {
            try
            {
                return _context.Clients.SingleOrDefault(c=> c.Id == clientId);
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public Client GetByProjectId(Guid projectId)
        {
            try
            {
                return _context.Clients.SingleOrDefault(c => c.Projects.FirstOrDefault(p => p.Id == projectId) != null);
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public bool Create(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                if (_context.SaveChanges() == 0)
                    return false;
            }
            catch (Exception ex)
            {
                //logger.ErrorFormat("Exception: {0}", ex.ToString());
                return false;
            }

            return true;
        }
    }
}