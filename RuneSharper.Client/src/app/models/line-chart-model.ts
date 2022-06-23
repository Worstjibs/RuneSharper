export interface LineChartModel {
    name: string;
    series: LineChartSeriesData[]
}

interface LineChartSeriesData {
    name: string;
    value: string;
}