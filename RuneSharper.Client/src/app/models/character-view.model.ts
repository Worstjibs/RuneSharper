import { Activities, ActivitiesChange } from "./activities.model";
import { Stats, StatsChange } from "./stats.model";

export interface CharacterView {
    userName: string;
    firstTracked: Date;
    stats: Stats;
    activities: Activities;
    activitiesChange: ActivitiesChange[];
    statsChange: StatsChange[];
}