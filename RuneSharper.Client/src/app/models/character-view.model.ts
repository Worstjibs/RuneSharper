import { Activities } from "./activities.model";
import { Stats } from "./stats.model";

export interface CharacterView {
    userName: string;
    firstTracked: Date;
    stats: Stats;
    activities: Activities;
}