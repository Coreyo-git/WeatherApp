import { useState, useEffect } from "react";
import { getForecast } from "./api/forecast";
import { Header } from "./components/Header/Header";
import { ForecastSummary } from "./components/ForecastSummary";
import { DaySelection } from "./components/DaySelection";
import type {
	WeatherForecast,
	ForecastDay,
	ForecastQuery,
} from "./types/weather";
import { createForecastQuery } from "./types/weather"

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
			setError(err instanceof Error ? err.message : "Something went wrong.");
			setForecast(null);
			setSelectedDate(null);
		} finally {
			setIsLoading(false);
		}
	}

	useEffect(() => {
		setIsLoading(true);
		navigator.geolocation.getCurrentPosition((pos) => {
			const coords = createForecastQuery([pos.coords.latitude, pos.coords.longitude])

			getForecast(coords).then((result) => {
				setForecast(result);
				setSelectedDate(result.today.date);
			}).finally(() => setIsLoading(false));
		}, () => setIsLoading(false));
	},[])

	const selectedDay: ForecastDay | null = forecast?.days.find(
		(d) => d.date === selectedDate
	) ?? forecast?.today ?? null;

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
          {error && <p>{error}</p>}
          {forecast && selectedDate && (
              <DaySelection
                  days={forecast.days}
                  selectedDate={selectedDate}
                  onSelect={setSelectedDate}
              />
          )}
          {/* TODO: Create forecast Component */}
          {forecast && selectedDay && (
              <section>
                  <div>
                      <img
                          src={`https:${forecast.current.condition.iconUrl}`}
                          alt={forecast.current.condition.text}
                      />
                      <p>{forecast.current.condition.text}</p>
                      <p>{forecast.current.temperatureCelsius}°C</p>
                      <p>Feels like {forecast.current.feelsLikeCelsius}°C</p>
                      <p>
                          Wind {forecast.current.windSpeedKph} kph{" "}
                          {forecast.current.windDirection}
                      </p>
                      <p>Humidity {forecast.current.humidity}%</p>
                      <p>UV {forecast.current.uvIndex}</p>
                  </div>

                  <ForecastSummary today={selectedDay} />
              </section>
          )}
      </main>
  );
}

export default App;
