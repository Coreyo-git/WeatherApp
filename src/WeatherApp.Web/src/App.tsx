import { useState } from "react";
import { getForecast } from "./api/forecast";
import { Header } from "./components/Header/Header";
import { ForecastSummary } from "./components/ForecastSummary";
import type { WeatherForecast } from "./types/weather";

function App() {
	const [forecast, setForecast] = useState<WeatherForecast | null>(null);
	const [error, setError] = useState<string | null>(null);

	async function handleSelectedSearchResult(cityId: number) {
		try {
			setError(null);
			setForecast(await getForecast(cityId));
		} catch (err) {
			setError(err instanceof Error ? err.message : "Something went wrong.");
		}
	}

  return (
      <main>
          <Header
              location={forecast?.location.country ?? ""}
              onSelectedSearchResult={handleSelectedSearchResult}
          />
          {error && <p>{error}</p>}

          {forecast && (
              <section>
                  <h2>
                      {forecast.location.name}, {forecast.location.region},{" "}
                      {forecast.location.country}
                  </h2>

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
