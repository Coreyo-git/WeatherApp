import type { WeatherForecast, ForecastQuery } from "../types/weather";

export async function getForecast(
    query: ForecastQuery,
): Promise<WeatherForecast> {
    let queryString: string;

    if (query.type === "id") {
        queryString = `id=${encodeURIComponent(query.id)}`;
    } else {
        queryString = `lat=${encodeURIComponent(query.lat)}&lon=${encodeURIComponent(query.lon)}`;
    }

    const response = await fetch(`/api/forecast?${queryString}`);

    if (!response.ok) {
        throw new Error(`Failed to fetch forecast (${response.status})`);
    }

    return response.json() as Promise<WeatherForecast>;
}
