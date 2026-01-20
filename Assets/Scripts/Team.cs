using System.Collections;
using System.Collections.Generic;

public class Team
{
    public Person teamLead;
    public List<TeamMember> teamMembers;
}

public class TeamMember
{
    public Person person;
    public AdvisorType advisorType;
}