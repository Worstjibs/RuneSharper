import { Frequency } from "@app/models/frequency";

export interface Stats {
    overall: Skill;
    attack: Skill;
    defence: Skill;
    strength: Skill;
    hitpoints: Skill;
    ranged: Skill;
    prayer: Skill;
    magic: Skill;
    cooking: Skill;
    woodcutting: Skill;
    fletching: Skill;
    fishing: Skill;
    firemaking: Skill;
    crafting: Skill;
    smithing: Skill;
    mining: Skill;
    herblore: Skill;
    agility: Skill;
    thieving: Skill;
    slayer: Skill;
    farming: Skill;
    runecrafting: Skill;
    hunter: Skill;
    construction: Skill;
}

export interface Skill {
    name: string,
    level: number;
    experience: number;
    rank: number;
}

export interface StatsChange {
    frequency: Frequency,
    dateRange: { DateFrom: Date, DateTo: Date },
    model: Stats;
}