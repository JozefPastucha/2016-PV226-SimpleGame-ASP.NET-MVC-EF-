using System;
using System.Collections.Generic;
using System.Linq;
using BL.AppInfrastructure;
using BL.DTOs.Players;
using BL.DTOs.UserAccount;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Queries
{
    public class PlayerAccordingToUsernameQuery : AppQuery<PlayerDTO>
    {
        public PlayerAccordingToUsernameQuery(IUnitOfWorkProvider provider) : base(provider) { }

        public string Username { get; set; }

        protected override IQueryable<PlayerDTO> GetQueryable()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new InvalidOperationException("PlayerAccordingToUserIdQuery - Userame must be valid.");
            }

            // Single result is expected so client side execution is not a problem
            var player = Context.Players.Include(nameof(Player.Account))
                .FirstOrDefault(c => c.Account.Username.Equals(Username));

            if (player == null)
            {
                return new EnumerableQuery<PlayerDTO>(new List<PlayerDTO>());
            }

            var playerDto = AutoMapper.Mapper.Map<PlayerDTO>(player);

            return new EnumerableQuery<PlayerDTO>(new List<PlayerDTO> { playerDto });
        }
    }
}
