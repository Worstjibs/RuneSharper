import { Activities } from "./activities.model";
import { Frequency } from "./frequency";
import { Stats } from "./stats.model";

export interface CharacterView {
    userName: string;
    firstTracked: Date;
    stats: Stats;
    activities: Activities;
    activitiesChange: { key: Frequency, value: Activities }[]
}