using System;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Web.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public Enums.UserType UserType { get; set; }

    }
}