using System;
using System.Collections.Generic;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IClientManager
    {
        IEnumerable<Client> GetAll();

        bool Create(Client client);

        bool Update(Client client);

        Client Get(Guid clientId);

        Client GetByProjectId(Guid projectId);
    }
}