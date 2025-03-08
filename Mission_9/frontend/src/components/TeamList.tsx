import React from "react";
import TeamCard from "./TeamCard";
import teamsData from "../data/CollegeBasketballTeams.json";
import { TeamsData } from "../types";

const TeamList: React.FC = () => {
  const { teams } = teamsData as TeamsData;

  return (
    <div className="team-list">
      {teams.map((team) => (
        <TeamCard key={team.tid} team={team} />
      ))}
    </div>
  );
};

export default TeamList;