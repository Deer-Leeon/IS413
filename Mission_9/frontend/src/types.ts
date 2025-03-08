export interface Team {
    tid: number;
    cid: number;
    did: number;
    school: string;
    name: string;
    abbrev: string;
    pop: number;
    city: string;
    state: string;
    latitude: number;
    longitude: number;
  }
  
  export interface TeamsData {
    teams: Team[];
  }