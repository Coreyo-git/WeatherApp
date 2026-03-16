import type { WeatherForecast } from "../types/weather";

export async function getForecast(query: string): Promise<WeatherForecast> {
  const response = await fetch(`/api/forecast?query=${encodeURIComponent(query)}`);

  if (!response.ok) {
    throw new Error(`Failed to fetch forecast (${response.status})`);
  }

  return response.json() as Promise<WeatherForecast>;
}
