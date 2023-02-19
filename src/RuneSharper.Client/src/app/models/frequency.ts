export interface Frequency {
    type: FrequencyType;
    name: string;
}

export enum FrequencyType {
    Day,
    Week,
    Fortnight,
    Month,
    Quarter,
    Year
}