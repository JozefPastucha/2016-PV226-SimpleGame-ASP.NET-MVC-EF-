using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Entities;
using BL.DTOs.Players;
using BL.Repositories;
using BL.Repositories.UserAccount;
using BL.Queries;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Players
{
    public class PlayerService : GameService, IPlayerService
    {
        public int PlayerPageSize => 9;

        private readonly PlayerRepository playerRepository;
        private readonly PlayerListQuery playerListQuery;
        private readonly PlayerAccordingToUsernameQuery playerAccordingToUsernameQuery;
        private readonly UserAccountRepository userRepository;

        public PlayerService(PlayerRepository playerRepository, PlayerListQuery playerListQuery, PlayerAccordingToUsernameQuery playerAccordingToUsernameQuery, UserAccountRepository userRepository)
        {
            this.playerRepository = playerRepository;
            this.playerListQuery = playerListQuery;
            this.playerAccordingToUsernameQuery = playerAccordingToUsernameQuery;
            this.userRepository = userRepository;
        }

        
        public int CreatePlayer(Guid userAccountId)
        {
            Player player;
            using (var uow = UnitOfWorkProvider.Create())
            {
                var playerAccount = userRepository.GetById(userAccountId);

                player = new Player { Account = playerAccount };

                playerRepository.Insert(player);

                uow.Commit();
            }
            return player.ID;
            
        }


        public PlayerDTO GetPlayerAccordingToUserName(string UserName)
        {
            using (UnitOfWorkProvider.Create())
            {
                playerAccordingToUsernameQuery.Username = UserName;
                return playerAccordingToUsernameQuery.Execute().SingleOrDefault();
            }
        }

        public PlayerDTO GetPlayer(int playerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var player = playerRepository.GetById(playerId);
                if (player == null)
                {
                    throw new NullReferenceException("Player Service - GetPlayer(...) player cant be null");
                }
                return player != null ? Mapper.Map<PlayerDTO>(player) : null;
            }
        }


        public Guid DeletePlayer(int id) 
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                Player player = playerRepository.GetById(id, p => p.Account);
                Guid accountID = player.Account.ID;

                playerRepository.Delete(id);
                uow.Commit();
                return accountID;
            }
            
        
        }

        public PlayerListQueryResultDTO ListPlayersPaging(string sortingCriteria, int requiredPage = 1)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = playerListQuery;
                query.Skip = Math.Max(0, requiredPage - 1) * PlayerPageSize;
                query.Take = PlayerPageSize;

                var sortOrder = SortDirection.Ascending;
                query.AddSortCriteria(sortingCriteria, sortOrder);

                return new PlayerListQueryResultDTO
                {
                    RequestedPage = requiredPage,
                    TotalResultCount = query.GetTotalRowCount(),
                    ResultsPage = query.Execute(),
                };
            }
        }

        public IEnumerable<PlayerDTO> ListPlayers()
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = playerListQuery;
                return query.Execute();
            }
        }
    }     
}