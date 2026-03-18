import { useState, useEffect } from "react";
import { getForecast } from "./api/forecast";
import { Header } from "./components/Header/Header";
import { ForecastSummary } from "./components/ForecastSummary";
import type {
	WeatherForecast,
	ForecastQuery,
} from "./types/weather";
import { createForecastQuery } from "./types/weather"

function App() {
	const [forecast, setForecast] = useState<WeatherForecast | null>(null);
	const [error, setError] = useState<string | null>(null);

	async function handleSelectedSearchResult(query: ForecastQuery) {
		try {
			setError(null);
			setForecast(await getForecast(query));
		} catch (err) {
			setError(err instanceof Error ? err.message : "Something went wrong.");
		}
	}

	useEffect(() => {
		navigator.geolocation.getCurrentPosition((pos) => {
			const coords = createForecastQuery([pos.coords.latitude, pos.coords.longitude])

			getForecast(coords).then((forecast) =>
				setForecast(forecast),
			)
		}); 
	},[])

  return (
      <main>
          <Header
              location={
                  forecast
                      ? `${forecast.location.name}, ${forecast.location.region}, ${forecast.location.country}`
                      : ""
              }
              onSelectedSearchResult={handleSelectedSearchResult}
          />
          {error && <p>{error}</p>}
          {/* TODO: Create Day Selection Button Scroller */}
          {/* TODO: Create forecast Component */}
          {forecast && (
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

                  <ForecastSummary today={forecast.today} />
              </section>
          )}
      </main>
  );
}

export default App;
