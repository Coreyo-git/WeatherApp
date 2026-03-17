import type { CitySearchResult } from "../types/weather";

export async function searchCities(query: string): Promise<CitySearchResult[]> {
    const response = await fetch(
        `/api/cities?query=${encodeURIComponent(query)}`,
    );

    if (!response.ok) {
        throw new Error(`Failed to fetch cities (${response.status})`);
    }

    return response.json() as Promise<CitySearchResult[]>;
}
