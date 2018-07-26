using System.Collections;
using System.Collections.Generic;
using Eppy;
namespace Corruptor
{
    public class Team
    {
        enum AdvisorType { MediaAdvisor, Intelligence, CampaignManager, SpeechWriter, PrincipalClerk }
        Tuple<Person, AdvisorType, int>[] TeamMembers;
    }
}