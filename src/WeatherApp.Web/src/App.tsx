import { useState, useEffect } from "react";
import { getForecast } from "./api/forecast";
import { Header } from "./components/Header/Header";
import { ForecastSummary } from "./components/ForecastSummary";
import { ErrorMessage } from "./components/ErrorMessage";
import { DaySelection } from "./components/DaySelection";
import type {
    WeatherForecast,
    ForecastDay,
    ForecastQuery,
} from "./types/weather";
import { createForecastQuery } from "./types/weather";
import { HourlyTempChart } from "./components/HourlyTempChart";

function App() {
    const [forecast, setForecast] = useState<WeatherForecast | null>(null);
    const [selectedDate, setSelectedDate] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    async function handleSelectedSearchResult(query: ForecastQuery) {
        try {
            setError(null);
            setIsLoading(true);
            const result = await getForecast(query);
            setForecast(result);
            setSelectedDate(result.today.date);
        } catch (err) {
            setError(
                err instanceof Error ? err.message : "Something went wrong.",
            );
            setForecast(null);
            setSelectedDate(null);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        setIsLoading(true);
        navigator.geolocation.getCurrentPosition(
            (pos) => {
                const coords = createForecastQuery([
                    pos.coords.latitude,
                    pos.coords.longitude,
                ]);

                getForecast(coords)
                    .then((result) => {
                        setForecast(result);
                        setSelectedDate(result.today.date);
                    })
                    .catch(() => setError("Failed to load forecast. Please try searching for a location."))
                    .finally(() => setIsLoading(false));
            },
            () => {
                setIsLoading(false);
                setError("Location access was denied. Search for a city to get started.");
            },
        );
    }, []);

    const selectedDay: ForecastDay | null =
        forecast?.days.find((d) => d.date === selectedDate) ??
        forecast?.today ??
        null;

    return (
        <main>
            <Header
                location={
                    forecast
                        ? `${forecast.location.name}, ${forecast.location.region}, ${forecast.location.country}`
                        : ""
                }
                isLoading={isLoading}
                onSelectedSearchResult={handleSelectedSearchResult}
            />
            {error && <ErrorMessage message={error} />}
            {forecast && selectedDate && (
                <DaySelection
                    days={forecast.days}
                    selectedDate={selectedDate}
                    onSelect={setSelectedDate}
                />
            )}
            {/* TODO: Create forecast Component */}
            {forecast && selectedDay && (
                <div className="flex gap-3 mt-3 items-start">
                    <ForecastSummary today={selectedDay} />
                    <HourlyTempChart hours={selectedDay.hours} />
                </div>
            )}
        </main>
    );
}

export default App;
