import { Frequency } from "./frequency";

export interface Activities {
    clues: Activity[];
    bosses: Activity[];
    other: Activity[];
}

export interface Activity {
    name: string;
    score: number;
    rank: number;
}

export interface ActivitiesChange {
    frequency: Frequency;
    dateRange: { DateFrom: Date, DateTo: Date };
    model: Activities;
}
