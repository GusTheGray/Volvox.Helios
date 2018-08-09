using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volvox.Helios.Service.Clients;

namespace Tests.Integration.Infrastructure
{
    class TestDiscordAPIClient : IDiscordAPIClient
    {

        public Task<string> GetGuildChannels(ulong guildId)
        {
            //TODO: return json blob from an actual call
            throw new NotImplementedException();
        }

        public Task<string> GetUserGuilds()
        {
            //TODO: return json blob from an actual call
            throw new NotImplementedException();
        }
    }
}
